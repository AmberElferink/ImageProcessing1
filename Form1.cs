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
            if (InputImage == null)
            {
                textBox2.Text = "Please Load an image first.";
                return;
            }


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
            if (InputImage == null)
            {
                textBox2.Text = "Please Load an image first.";
                return;
            }

            resetForApply();

            if (radioButton1.Checked == true)
                ApplyLinearFilter();

            else if (gaussianButton.Checked == true)
                ApplyGaussianFilter();
            else if (radioButton3.Checked == true)
                ApplyMedianFilter();
            else if (radioButton4.Checked == true)
                ApplyMaxFilter();
            else if (thresholdButton.Checked == true)
                ApplyThresholdFilter();
        }

        private void ApplyLinearFilter()
        {
            int[,] matrix = ParseMatrix();
            newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            if (matrix != null)
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
                        newImage[x, y] = updatedColor;
                        progressBar.PerformStep();
                    }
                }
            }
            Image = newImage;
            toOutputBitmap();
        }

        private void ApplyGaussianFilter()
        {
            // input lezen: eerst een double voor de sigma, dan een int voor de kernel size
            double sigma;
            int boxsize;
            try
            {
                string[] input = gaussianInput.Text.Split();
                sigma = double.Parse(input[0]);
                boxsize = int.Parse(input[1]);
                if (sigma <= 0 || boxsize <= 0)
                {
                    throw new Exception("Please use positive numbers only");
                }
                if (boxsize % 2 == 0)
                {
                    throw new Exception("Please make sure that the kernel size is an odd number");
                }
            }
            catch (Exception e)
            {
                this.textBox2.Text = e.Message;
                return ;
            }

            int filterBorder = (boxsize - 1) / 2;
            float[] kernel = new float[boxsize];

            // berekenen 1D gaussian filter, geplukt uit boek pagina 115
            double sigma2 = sigma * sigma;
            float countingKernel = 0;
            for (int j = 0; j < kernel.Length; j++)
            {
                double r = filterBorder - j;
                kernel[j] = (float)Math.Exp(-0.5 * (r * r) / sigma2);
                countingKernel += kernel[j];
            }
            for (int j = 0; j < kernel.Length; j++)
            {
                kernel[j] = kernel[j] / countingKernel;
                Console.WriteLine("Kernel " + (j) + ": " + kernel[j]);
            }
            
            // maak arrays om tijdelijke variabelen in op te slaan
            float[,] gaussian1DColor = new float[boxsize, boxsize];
            float[,] gaussian2DColor = new float[boxsize, boxsize];
            float[,] weight = new float[boxsize, boxsize];

            // verzamel- en telvariabelen
            float gaussianTotal = 0;
            float weightTotal = 0;
            int i = 0;

            newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];

            for (int x = filterBorder; x < InputImage.Size.Width - filterBorder; x++)
            {
                for (int y = filterBorder; y < InputImage.Size.Height - filterBorder; y++)
                {
                    gaussianTotal = 0;
                    weightTotal = 0;
                    i = 0;

                    // 1D-gaussian wordt eerst horizontaal toegepast. Nieuwe grijswaarden en weights worden in bijbehorende arrays opgeslagen.
                    for (int a = (filterBorder * -1); a <= filterBorder; a++)
                    {
                        i = 0;
                        for (int b = (filterBorder * -1); b <= filterBorder; b++)
                        {
                            gaussian1DColor[a + filterBorder,b + filterBorder] = (Image[x + a, y + b].R * kernel[i]);
                            weight[a + filterBorder, b + filterBorder] = kernel[i];
                            i++;
                        }
                    }

                    // 1D-gaussian wordt opnieuw toegepast, nu verticaal.
                    // Grijswaarden uit de eerste keer worden gebruikt om de nieuwe grijswaarden te maken, en deze worden bij elkaar opgeteld.
                    // Weights worden ook herberekend en bij elkaar opgeteld.
                    i = 0;
                    for (int a = (filterBorder * -1); a <= filterBorder; a++)
                    {
                        for (int b = (filterBorder * -1); b <= filterBorder; b++)
                        {
                            gaussian2DColor[a + filterBorder, b + filterBorder] = (gaussian1DColor[a + filterBorder, b + filterBorder] * kernel[i]);
                            gaussianTotal = gaussianTotal + gaussian2DColor[a + filterBorder, b + filterBorder];
                            weight[a + filterBorder, b + filterBorder] = weight[a + filterBorder, b + filterBorder] * kernel[i];
                            weightTotal = weightTotal + weight[a + filterBorder, b + filterBorder];
                        }
                        i++;
                    }
                    Console.WriteLine(weightTotal);


                    // Totale som grijswaarden delen door totale weights om uiteindelijke grijswaarde te krijgen.
                    int gaussianColor = (int)(gaussianTotal / weightTotal);


                    Color updatedColor = Color.FromArgb(gaussianColor, gaussianColor, gaussianColor);
                    newImage[x, y] = updatedColor;
                    progressBar.PerformStep();
                }
            }
            string gaussianOutput = "";
            for (int k = 0; k < boxsize; k++)
            {
                for (int j = 0; j < boxsize; j++)
                {
                    gaussianOutput += ((weight[k, j] / weightTotal) + " ");
                }
                gaussianOutput += "\r\n";
            }
            textBox1.Text = gaussianOutput;

            Image = newImage;
            toOutputBitmap();
        }

        private void ApplyThresholdFilter()
        {
            int thresholdColor = 0;
            int thresholdLimit = thresholdTrackbar.Value;       // threshold wordt afgelezen van trackbar
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    if (Image[x, y].R < thresholdLimit)
                    {
                        thresholdColor = 0;
                    }
                    else
                    {
                        thresholdColor = 255;
                    }
                    Color updatedColor = Color.FromArgb(thresholdColor, thresholdColor, thresholdColor);
                    Image[x, y] = updatedColor;
                    progressBar.PerformStep();
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
                    throw new Exception("Give an odd number");
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
            int halfboxsize;
            try
            {
                boxsize = int.Parse(maxFilterValue.Text);
                if (boxsize <= 0)
                    throw new Exception("Give a positive number");
                else if (boxsize % 2 == 0)
                    throw new Exception("Give an odd number");

            }
            catch (Exception e)
            {
                textBox2.Text = e.Message;
                return;
            }

            halfboxsize = boxsize / 2;
            Color[,] newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            for (int x = halfboxsize; x < InputImage.Size.Width - halfboxsize; x++)
            {
                for (int y = halfboxsize; y < InputImage.Size.Height - halfboxsize; y++)
                {
                    int maxValue = 0;
                    for (int a = (halfboxsize * -1); a <= halfboxsize; a++)
                    {
                        for (int b = (halfboxsize * -1); b <= halfboxsize; b++)
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
            if (gaussianButton.Checked)
            {
                gaussianInput.ReadOnly = false;
                textBox2.Text = "Please first enter a double and then an integer in the adjacent text box";
            }
            else
            {
                gaussianInput.ReadOnly = true;
            }
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
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            thresholdValue.Text = thresholdTrackbar.Value.ToString();
        }

        private void thresholdButton_CheckedChanged(object sender, EventArgs e)
        {
            if (thresholdButton.Checked)
                thresholdTrackbar.Enabled = true;
            else
                thresholdTrackbar.Enabled = false;
        }

        private void thresholdValue_Click(object sender, EventArgs e)
        {

        }
    }
}
