namespace INFOIBV
{
    partial class INFOIBV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.gaussianButton = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gaussianInput = new System.Windows.Forms.TextBox();
            this.medianSize = new System.Windows.Forms.Label();
            this.medianSizeValue = new System.Windows.Forms.TextBox();
            this.edgeDetection = new System.Windows.Forms.RadioButton();
            this.thresholdTrackbar = new System.Windows.Forms.TrackBar();
            this.thresholdValue = new System.Windows.Forms.Label();
            this.thresholdButton = new System.Windows.Forms.RadioButton();
            this.RightAsInput = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(12, 12);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(98, 23);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Location = new System.Drawing.Point(116, 14);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(316, 20);
            this.imageFileName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(434, 373);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(948, 14);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(210, 23);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "To grey-scale and auto contrast";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1232, 14);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(95, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(531, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(394, 373);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(531, 14);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(394, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(945, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Kernel input. Seperation by space and enter";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(948, 188);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(79, 17);
            this.radioButton1.TabIndex = 9;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Linear Filter";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gaussianButton
            // 
            this.gaussianButton.AutoSize = true;
            this.gaussianButton.Location = new System.Drawing.Point(948, 211);
            this.gaussianButton.Name = "gaussianButton";
            this.gaussianButton.Size = new System.Drawing.Size(90, 17);
            this.gaussianButton.TabIndex = 10;
            this.gaussianButton.TabStop = true;
            this.gaussianButton.Text = "Gaussian Blur";
            this.gaussianButton.UseVisualStyleBackColor = true;
            this.gaussianButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(948, 234);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(85, 17);
            this.radioButton3.TabIndex = 11;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Median Filter";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(952, 316);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Apply filter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(948, 84);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(246, 99);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(945, 397);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(382, 20);
            this.textBox2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(945, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Message";
            // 
            // gaussianInput
            // 
            this.gaussianInput.Location = new System.Drawing.Point(1044, 211);
            this.gaussianInput.Name = "gaussianInput";
            this.gaussianInput.ReadOnly = true;
            this.gaussianInput.Size = new System.Drawing.Size(100, 20);
            this.gaussianInput.TabIndex = 16;
            // 
            // medianSize
            // 
            this.medianSize.AutoSize = true;
            this.medianSize.Location = new System.Drawing.Point(1042, 236);
            this.medianSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.medianSize.Name = "medianSize";
            this.medianSize.Size = new System.Drawing.Size(25, 13);
            this.medianSize.TabIndex = 19;
            this.medianSize.Text = "size";
            // 
            // medianSizeValue
            // 
            this.medianSizeValue.Location = new System.Drawing.Point(1071, 233);
            this.medianSizeValue.Margin = new System.Windows.Forms.Padding(2);
            this.medianSizeValue.Name = "medianSizeValue";
            this.medianSizeValue.ReadOnly = true;
            this.medianSizeValue.Size = new System.Drawing.Size(43, 20);
            this.medianSizeValue.TabIndex = 18;
            // 
            // edgeDetection
            // 
            this.edgeDetection.Location = new System.Drawing.Point(948, 279);
            this.edgeDetection.Name = "edgeDetection";
            this.edgeDetection.Size = new System.Drawing.Size(104, 24);
            this.edgeDetection.TabIndex = 26;
            this.edgeDetection.Text = "Edge Detection";
            // 
            // thresholdTrackbar
            // 
            this.thresholdTrackbar.Enabled = false;
            this.thresholdTrackbar.Location = new System.Drawing.Point(1049, 257);
            this.thresholdTrackbar.Margin = new System.Windows.Forms.Padding(2);
            this.thresholdTrackbar.Maximum = 255;
            this.thresholdTrackbar.Name = "thresholdTrackbar";
            this.thresholdTrackbar.Size = new System.Drawing.Size(94, 45);
            this.thresholdTrackbar.TabIndex = 23;
            this.thresholdTrackbar.Value = 127;
            this.thresholdTrackbar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // thresholdValue
            // 
            this.thresholdValue.AutoSize = true;
            this.thresholdValue.Location = new System.Drawing.Point(1148, 259);
            this.thresholdValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.thresholdValue.Name = "thresholdValue";
            this.thresholdValue.Size = new System.Drawing.Size(25, 13);
            this.thresholdValue.TabIndex = 24;
            this.thresholdValue.Text = "127";
            this.thresholdValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thresholdButton
            // 
            this.thresholdButton.AutoSize = true;
            this.thresholdButton.Location = new System.Drawing.Point(948, 257);
            this.thresholdButton.Margin = new System.Windows.Forms.Padding(2);
            this.thresholdButton.Name = "thresholdButton";
            this.thresholdButton.Size = new System.Drawing.Size(97, 17);
            this.thresholdButton.TabIndex = 25;
            this.thresholdButton.TabStop = true;
            this.thresholdButton.Text = "Threshold Filter";
            this.thresholdButton.UseVisualStyleBackColor = true;
            this.thresholdButton.CheckedChanged += new System.EventHandler(this.thresholdButton_CheckedChanged);
            // 
            // RightAsInput
            // 
            this.RightAsInput.AutoSize = true;
            this.RightAsInput.Location = new System.Drawing.Point(948, 45);
            this.RightAsInput.Name = "RightAsInput";
            this.RightAsInput.Size = new System.Drawing.Size(142, 17);
            this.RightAsInput.TabIndex = 27;
            this.RightAsInput.Text = "use Right image as input";
            this.RightAsInput.UseVisualStyleBackColor = true;
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 447);
            this.Controls.Add(this.RightAsInput);
            this.Controls.Add(this.thresholdButton);
            this.Controls.Add(this.thresholdValue);
            this.Controls.Add(this.thresholdTrackbar);
            this.Controls.Add(this.edgeDetection);
            this.Controls.Add(this.medianSize);
            this.Controls.Add(this.medianSizeValue);
            this.Controls.Add(this.gaussianInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.gaussianButton);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageFileName);
            this.Controls.Add(this.LoadImageButton);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton gaussianButton;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label medianSize;
        private System.Windows.Forms.TextBox medianSizeValue;
        private System.Windows.Forms.TextBox gaussianInput;
        private System.Windows.Forms.TrackBar thresholdTrackbar;
        private System.Windows.Forms.Label thresholdValue;
        private System.Windows.Forms.RadioButton thresholdButton;
        private System.Windows.Forms.RadioButton edgeDetection;
        private System.Windows.Forms.CheckBox RightAsInput;
    }
}

