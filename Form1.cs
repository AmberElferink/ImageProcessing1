﻿using System;
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
                    pictureBox1.Image = (Image) InputImage;                 // Display input image
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage == null) return;                                 // Get out if no input image
            if (OutputImage != null) OutputImage.Dispose();                 // Reset output image
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // Create new output image
            Color[,] Image = new Color[InputImage.Size.Width, InputImage.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)

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
            /*
            int[] newHistogram = new int[256];                              //nieuw histogram wordt aangemaakt (om te checken of het daadwerkelijk werkt)
            
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];
                    int newGrey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;         //newGrey wordt voor iedere pixel berekend, gebaseerd op formule uit boek
                    newGrey = (newGrey - alow) * 255 / (ahigh - alow);

                    Color updatedColor = Color.FromArgb(newGrey, newGrey, newGrey);

                    newHistogram[newGrey]++;

                    //Image[x, y] = updatedColor;
                    progressBar.PerformStep();
                }
            }


            Console.WriteLine("New histogram: ");
            for (int a = 0; a < 256; a++)
            {
                Console.WriteLine(a + ": " + newHistogram[a]);
            }
    

            int boxsize = 2;
            for (int x = boxsize; x < InputImage.Size.Width - boxsize; x++)
            {
                for (int y = boxsize; y < InputImage.Size.Height - boxsize; y++)
                {
                    int newestGrey = 0;
                    for (int a = (boxsize * -1); a <= boxsize; a++)
                    {
                        for (int b = (boxsize * -1); b <= boxsize; b++)
                        {
                            newestGrey = newestGrey + Image[x + a, y + b].R;
                        }
                    }
                    newestGrey = newestGrey / ((int)Math.Pow(((2 * boxsize) + 1), 2));

                    Color updatedColor = Color.FromArgb(newestGrey, newestGrey, newestGrey);
                    Image[x, y] = updatedColor;
                    progressBar.PerformStep();
                }
            }

    
            */
            int boxsize = 2;
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
                        progressBar.PerformStep();
                    }
                }
            }
                    //==========================================================================================

                    // Copy array to output Bitmap
        for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    //OutputImage.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                    OutputImage.SetPixel(x, y, newImage[x, y]);
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

    }
}
