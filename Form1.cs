using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;
        Color[,] Image;
        Color[,] newImage;

        public INFOIBV()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // Open File Dialog
            {
                string file = openImageDialog.FileName;                     // Get the file name
                imageFileName.Text = file;                                  // Show file name
                if (InputImage != null) InputImage.Dispose();               // Reset image
                InputImage = new Bitmap(file);                              // Create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // Dimension check
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                {
                    pictureBox1.Image = (Image)InputImage;                 // Display input image
                }


            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            resetForApply();

            //==========================================================================================
            // TODO: include here your own code
            // example: create a negative image
            int[] histogram = new int[256];     //histogram aanmaken, alow en ahigh initialiseren
            int alow = 255;
            int ahigh = 0;

            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];                         // Get the pixel color at coordinate (x,y)
                    //Color updatedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B); // Negative image
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;    //aanmaken grijswaarde op basis van RGB-values
                    Color updatedColor = Color.FromArgb(grey, grey, grey);          //toepassen grijswaarde

                    histogram[grey]++;      //histogram updaten

                    //kijken of er nieuwe alow/ahigh gevonden is
                    if (grey < alow)
                    {
                        alow = grey;
                    }
                    if (grey > ahigh)
                    {
                        ahigh = grey;
                    }

                    //Image[x, y] = updatedColor;                             // Set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // Increment progress bar
                }
            }

            Console.WriteLine("Histogram:");                                //histogram wordt geprint
            for(int a = 0; a < 256; a++)
            {
                Console.WriteLine(a + ": " + histogram[a]);
            }
            Console.WriteLine("A-low: " + alow);
            Console.WriteLine("A-high: " + ahigh);

            int[] newHistogram = new int[256];                              //nieuw histogram wordt aangemaakt (om te checken of het daadwerkelijk werkt)

            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];
                    int newGrey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;         //newGrey wordt voor iedere pixel berekend
                    newGrey = (newGrey - alow) * 255 / (ahigh - alow);

                    Color updatedColor = Color.FromArgb(newGrey, newGrey, newGrey);

                    newHistogram[newGrey]++;

                    Image[x, y] = updatedColor;
                   
                }
                progressBar.PerformStep();
            }

            Console.WriteLine("New histogram: ");
            for (int a = 0; a < 256; a++)
            {
                Console.WriteLine(a + ": " + newHistogram[a]);
            }
                    //==========================================================================================

                    // Copy array to output Bitmap
                    for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    OutputImage.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                }
            }
            
            pictureBox2.Image = (Image)OutputImage;                         // Display output image
            progressBar.Visible = false;                                    // Hide progress bar
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return;                                // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage.Save(saveImageDialog.FileName);                 // Save the output image
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }




        //Filter Radiobuttons



        private void button1_Click(object sender, EventArgs e)
        {
            resetForApply();

            if (radioButton1.Checked == true)
                ApplyLinearFilter();

            else if (radioButton3.Checked == true)
                ApplyMedianFilter();
            else if (radioButton4.Checked == true)
                ApplyMaxFilter();
        }

        private void ApplyLinearFilter()
        {
            int[,] matrix = ParseMatrix();
            if(matrix != null)
            {
                // linear boxfilter
                int boxsize = matrix.GetLength(0);        // lengte matrix wordt uitgelezen
                int filterBorder = (boxsize - 1) / 2;       // hulpvariabele voor berekeningen

                for (int x = filterBorder; x < InputImage.Size.Width - filterBorder; x++)
                {
                    for (int y = filterBorder; y < InputImage.Size.Height - filterBorder; y++)
                    {
                        int linearColor = 0;
                        int matrixTotal = 0;        // totale waarde van alle weights van de matrix bij elkaar opgeteld
                        for (int a = (filterBorder * -1); a <= filterBorder; a++)
                        {
                            for (int b = (filterBorder * -1); b <= filterBorder; b++)
                            {
                                linearColor = linearColor + (Image[x + a, y + b].R * matrix[a + filterBorder, b + filterBorder]);
                                // weight van filter wordt per kernel pixel toegepast op image pixel
                                matrixTotal = matrixTotal + matrix[a + filterBorder, b + filterBorder];
                                // weight wordt opgeteld bij totaalsom van weights
                            }
                        }
                        linearColor = linearColor / matrixTotal;

                        Color updatedColor = Color.FromArgb(linearColor, linearColor, linearColor);
                        Image[x, y] = updatedColor;
                        progressBar.PerformStep();
                    }
                }
            }

            toOutputBitmap();
        }

        void resetForApply()
        {
            if (InputImage == null) return;                                 // Get out if no input image
            if (OutputImage != null) OutputImage.Dispose();                 // Reset output image
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // Create new output image
            Image = new Color[InputImage.Size.Width, InputImage.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)


            // Setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = InputImage.Size.Width * InputImage.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;

            // Copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    Image[x, y] = InputImage.GetPixel(x, y);                // Set pixel color in array at (x,y)
                }
            }
        }

        void toOutputBitmap()
        {
            // Copy array to output Bitmap
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    OutputImage.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                    //OutputImage.SetPixel(x, y, newImage[x, y]);
                }
            }

            pictureBox2.Image = (Image)OutputImage;                         // Display output image
            progressBar.Visible = false;                                    // Hide progress bar
        }

        int[,] ParseMatrix()
        {
            try
            {
                // split the rows
                string input = textBox1.Text;
                string[] rows = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                // split the columns and add to a 2D array

                int[,] matrix = new int[rows.Length, rows.Length]; //creer M x M matrix afhankelijk van ingevoerde values

                for (int i = 0; i < rows.Length; i++)
                {
                    // alle 3 de rijen parsen
                    int[] column = Array.ConvertAll(rows[i].Split(' '), int.Parse);

                    if (column.Length != rows.Length)
                    {
                        throw new Exception("Provide a square matrix, with equal number of rows and columns");
                    }
                    if (column.Length %2 == 0)
                        throw new Exception("Provide a square matrix, with an odd number of columns and rows");


                    // deze kolom op de goede plek in de matrix zetten
                    for (int j = 0; j < rows.Length; j++)
                        matrix[i, j] = column[j];
                }

                return matrix;
                //de matrix is geparsed en de waardes zijn nu op te halen

            }
            catch (Exception e)
            {
                this.textBox2.Text = e.Message;
                return null;
            }

            

        }




        void ApplyMedianFilter()
        {
            int halfboxSize;
            int boxSize;
            try
            {
                boxSize = int.Parse(medianSizeValue.Text);
                if (boxSize <= 0)
                    throw new Exception("Give a positive number");
                else if (boxSize % 2 == 0)
                    throw new Exception("Give an uneven number");
                halfboxSize = boxSize / 2;
            }
            catch(Exception e)
            {
                textBox2.Text = e.Message;
                return;
            }

            newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];

            int[] values = new int[boxSize*boxSize];

            for (int x = halfboxSize; x < InputImage.Size.Width - halfboxSize; x++)
            {
                for (int y = halfboxSize; y < InputImage.Size.Height - halfboxSize; y++)
                {
                    int valueIndex= 0;
                    for (int a = -halfboxSize; a <= halfboxSize; a++)
                    {
                        for (int b = -halfboxSize; b <= halfboxSize; b++)
                        {
                            values[valueIndex] = Image[x + a, y + b].R;
                            valueIndex++;
                        }
                    }
                    Array.Sort(values);
                        
                    int newColor = values[values.Length / 2];
                    Color updatedColor = Color.FromArgb(newColor, newColor, newColor);

                    newImage[x, y] = updatedColor;
                    progressBar.PerformStep();
                }
            }
            Image = newImage;

            toOutputBitmap();
        }


        void ApplyMaxFilter()
        {
            int boxsize;
            try
            {
                boxsize = int.Parse(maxFilterValue.Text);
                if (boxsize <= 0)
                    throw new Exception("Give a positive number");
                else if (boxsize % 2 == 0)
                    throw new Exception("Give an uneven number");

            }
            catch (Exception e)
            {
                textBox2.Text = e.Message;
                return;
            }

            Color[,] newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            for (int x = boxsize; x < InputImage.Size.Width - boxsize; x++)
            {
                for (int y = boxsize; y < InputImage.Size.Height - boxsize; y++)
                {
                    int maxValue = 0;
                    for (int a = (boxsize * -1); a <= boxsize; a++)
                    {
                        for (int b = (boxsize * -1); b <= boxsize; b++)
                        {
                            if (Image[x + a, y + b].R > maxValue)
                            {
                                maxValue = Image[x + a, y + b].R;
                            }
                        }
                        Color updatedColor = Color.FromArgb(maxValue, maxValue, maxValue);
                        newImage[x, y] = updatedColor;

                    }
                    progressBar.PerformStep();
                }
            }
            Image = newImage;


            toOutputBitmap();
        }

        


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                textBox1.ReadOnly = false;
            else
                textBox1.ReadOnly = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                textBox3.ReadOnly = false;
            else
                textBox3.ReadOnly = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                medianSizeValue.ReadOnly = false;
            else
                medianSizeValue.ReadOnly = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                maxFilterValue.ReadOnly = false;
            else
                maxFilterValue.ReadOnly = true;
        }
    }
}
