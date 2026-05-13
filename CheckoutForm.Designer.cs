namespace PixMartt
{
    partial class CheckoutForm
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
            this.pictureBoxArtwork = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPostedBy = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnConfirmDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtwork)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxArtwork
            // 
            this.pictureBoxArtwork.Location = new System.Drawing.Point(32, 84);
            this.pictureBoxArtwork.Name = "pictureBoxArtwork";
            this.pictureBoxArtwork.Size = new System.Drawing.Size(285, 254);
            this.pictureBoxArtwork.TabIndex = 15;
            this.pictureBoxArtwork.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(459, 301);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel / Back";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(339, 195);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Description";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(339, 163);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 12;
            this.lblCategory.Text = "Category";
            // 
            // lblPostedBy
            // 
            this.lblPostedBy.AutoSize = true;
            this.lblPostedBy.Location = new System.Drawing.Point(339, 129);
            this.lblPostedBy.Name = "lblPostedBy";
            this.lblPostedBy.Size = new System.Drawing.Size(30, 13);
            this.lblPostedBy.TabIndex = 11;
            this.lblPostedBy.Text = "Artist";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(339, 97);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Title";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(339, 228);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(31, 13);
            this.lblPrice.TabIndex = 9;
            this.lblPrice.Text = "Price";
            // 
            // btnConfirmDownload
            // 
            this.btnConfirmDownload.Location = new System.Drawing.Point(339, 301);
            this.btnConfirmDownload.Name = "btnConfirmDownload";
            this.btnConfirmDownload.Size = new System.Drawing.Size(114, 23);
            this.btnConfirmDownload.TabIndex = 8;
            this.btnConfirmDownload.Text = "Confirm Download";
            this.btnConfirmDownload.UseVisualStyleBackColor = true;
            this.btnConfirmDownload.Click += new System.EventHandler(this.btnConfirmDownload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(360, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Do you want to download this artwork?";
            // 
            // CheckoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxArtwork);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblPostedBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnConfirmDownload);
            this.Name = "CheckoutForm";
            this.Text = "CheckoutForm";
            this.Load += new System.EventHandler(this.CheckoutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxArtwork;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPostedBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnConfirmDownload;
        private System.Windows.Forms.Label label1;
    }
}