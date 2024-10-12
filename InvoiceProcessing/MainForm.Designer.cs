namespace InvoiceProcessing
{
    partial class MainForm
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
            this.InvoicePictureBox = new System.Windows.Forms.PictureBox();
            this.OcrTextBox = new System.Windows.Forms.TextBox();
            this.OcrButton = new System.Windows.Forms.Button();
            this.ProcessedTextBox = new System.Windows.Forms.TextBox();
            this.ClassifyButton = new System.Windows.Forms.Button();
            this.ConfigurationTextBox = new System.Windows.Forms.TextBox();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.ConfigurationLabel = new System.Windows.Forms.Label();
            this.AccountingTextBox = new System.Windows.Forms.TextBox();
            this.AccountingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.InvoicePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(26, 25);
            this.LoadImageButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(150, 44);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // InvoicePictureBox
            // 
            this.InvoicePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InvoicePictureBox.Location = new System.Drawing.Point(26, 83);
            this.InvoicePictureBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.InvoicePictureBox.Name = "InvoicePictureBox";
            this.InvoicePictureBox.Size = new System.Drawing.Size(1336, 1308);
            this.InvoicePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InvoicePictureBox.TabIndex = 1;
            this.InvoicePictureBox.TabStop = false;
            // 
            // OcrTextBox
            // 
            this.OcrTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OcrTextBox.Location = new System.Drawing.Point(1374, 83);
            this.OcrTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OcrTextBox.Multiline = true;
            this.OcrTextBox.Name = "OcrTextBox";
            this.OcrTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OcrTextBox.Size = new System.Drawing.Size(752, 1304);
            this.OcrTextBox.TabIndex = 2;
            // 
            // OcrButton
            // 
            this.OcrButton.Location = new System.Drawing.Point(1374, 25);
            this.OcrButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OcrButton.Name = "OcrButton";
            this.OcrButton.Size = new System.Drawing.Size(150, 44);
            this.OcrButton.TabIndex = 3;
            this.OcrButton.Text = "OCR";
            this.OcrButton.UseVisualStyleBackColor = true;
            this.OcrButton.Click += new System.EventHandler(this.OcrButton_Click);
            // 
            // ProcessedTextBox
            // 
            this.ProcessedTextBox.Location = new System.Drawing.Point(2138, 299);
            this.ProcessedTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ProcessedTextBox.Multiline = true;
            this.ProcessedTextBox.Name = "ProcessedTextBox";
            this.ProcessedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ProcessedTextBox.Size = new System.Drawing.Size(487, 756);
            this.ProcessedTextBox.TabIndex = 4;
            // 
            // ClassifyButton
            // 
            this.ClassifyButton.Location = new System.Drawing.Point(2475, 27);
            this.ClassifyButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ClassifyButton.Name = "ClassifyButton";
            this.ClassifyButton.Size = new System.Drawing.Size(150, 44);
            this.ClassifyButton.TabIndex = 5;
            this.ClassifyButton.Text = "Classify";
            this.ClassifyButton.UseVisualStyleBackColor = true;
            this.ClassifyButton.Click += new System.EventHandler(this.ClassifyButton_Click);
            // 
            // ConfigurationTextBox
            // 
            this.ConfigurationTextBox.Location = new System.Drawing.Point(2138, 83);
            this.ConfigurationTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ConfigurationTextBox.Multiline = true;
            this.ConfigurationTextBox.Name = "ConfigurationTextBox";
            this.ConfigurationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigurationTextBox.Size = new System.Drawing.Size(487, 204);
            this.ConfigurationTextBox.TabIndex = 6;
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(2475, 1081);
            this.ProcessButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(150, 44);
            this.ProcessButton.TabIndex = 7;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // ConfigurationLabel
            // 
            this.ConfigurationLabel.AutoSize = true;
            this.ConfigurationLabel.Location = new System.Drawing.Point(2138, 35);
            this.ConfigurationLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ConfigurationLabel.Name = "ConfigurationLabel";
            this.ConfigurationLabel.Size = new System.Drawing.Size(146, 25);
            this.ConfigurationLabel.TabIndex = 8;
            this.ConfigurationLabel.Text = "Configuration:";
            // 
            // AccountingTextBox
            // 
            this.AccountingTextBox.Location = new System.Drawing.Point(2138, 1137);
            this.AccountingTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.AccountingTextBox.Multiline = true;
            this.AccountingTextBox.Name = "AccountingTextBox";
            this.AccountingTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AccountingTextBox.Size = new System.Drawing.Size(487, 254);
            this.AccountingTextBox.TabIndex = 9;
            // 
            // AccountingLabel
            // 
            this.AccountingLabel.AutoSize = true;
            this.AccountingLabel.Location = new System.Drawing.Point(2138, 1091);
            this.AccountingLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AccountingLabel.Name = "AccountingLabel";
            this.AccountingLabel.Size = new System.Drawing.Size(125, 25);
            this.AccountingLabel.TabIndex = 10;
            this.AccountingLabel.Text = "Accounting:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2740, 1413);
            this.Controls.Add(this.AccountingLabel);
            this.Controls.Add(this.AccountingTextBox);
            this.Controls.Add(this.ConfigurationLabel);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.ConfigurationTextBox);
            this.Controls.Add(this.ClassifyButton);
            this.Controls.Add(this.ProcessedTextBox);
            this.Controls.Add(this.OcrButton);
            this.Controls.Add(this.OcrTextBox);
            this.Controls.Add(this.InvoicePictureBox);
            this.Controls.Add(this.LoadImageButton);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "Invoice processing";
            ((System.ComponentModel.ISupportInitialize)(this.InvoicePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.PictureBox InvoicePictureBox;
        private System.Windows.Forms.TextBox OcrTextBox;
        private System.Windows.Forms.Button OcrButton;
        private System.Windows.Forms.TextBox ProcessedTextBox;
        private System.Windows.Forms.Button ClassifyButton;
        private System.Windows.Forms.TextBox ConfigurationTextBox;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.Label ConfigurationLabel;
        private System.Windows.Forms.TextBox AccountingTextBox;
        private System.Windows.Forms.Label AccountingLabel;
    }
}

