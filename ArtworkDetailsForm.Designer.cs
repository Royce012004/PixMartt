namespace PixMartt
{
    partial class ArtworkDetailsForm
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
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPostedBy = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pictureBoxArtwork = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtwork)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(365, 304);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(114, 23);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Buy / Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(365, 231);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(31, 13);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "Price";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(365, 100);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title";
            // 
            // lblPostedBy
            // 
            this.lblPostedBy.AutoSize = true;
            this.lblPostedBy.Location = new System.Drawing.Point(365, 132);
            this.lblPostedBy.Name = "lblPostedBy";
            this.lblPostedBy.Size = new System.Drawing.Size(30, 13);
            this.lblPostedBy.TabIndex = 3;
            this.lblPostedBy.Text = "Artist";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(365, 166);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(365, 198);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(485, 304);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(114, 23);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Cancel / Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pictureBoxArtwork
            // 
            this.pictureBoxArtwork.Location = new System.Drawing.Point(58, 87);
            this.pictureBoxArtwork.Name = "pictureBoxArtwork";
            this.pictureBoxArtwork.Size = new System.Drawing.Size(285, 254);
            this.pictureBoxArtwork.TabIndex = 7;
            this.pictureBoxArtwork.TabStop = false;
            // 
            // ArtworkDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 450);
            this.Controls.Add(this.pictureBoxArtwork);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblPostedBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnDownload);
            this.Name = "ArtworkDetailsForm";
            this.Text = "ArtworkDetailsForm";
            this.Load += new System.EventHandler(this.ArtworkDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPostedBy;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.PictureBox pictureBoxArtwork;
    }
}