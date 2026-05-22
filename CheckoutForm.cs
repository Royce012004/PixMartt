using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class CheckoutForm : Form
    {
        int currentUserID;
        int selectedArtworkID;

        public CheckoutForm(int userID, int artworkID)
        {
            InitializeComponent();
            currentUserID = userID;
            selectedArtworkID = artworkID;

            DesignCheckoutForm();
            LoadCheckoutDetails();
        }

        private void DesignCheckoutForm()
        {
            this.Text = "PIXMART Checkout";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1150, 720);
            this.BackColor = Color.FromArgb(235, 235, 235);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = AutoScaleMode.None;

            this.Controls.Clear();

            // MAIN SHADOW
            Panel mainShadow = new Panel();
            mainShadow.Size = new Size(945, 650);
            mainShadow.Location = new Point((this.ClientSize.Width - 945) / 2 + 8, 28);
            mainShadow.BackColor = Color.FromArgb(190, 190, 190);
            this.Controls.Add(mainShadow);

            // MAIN PANEL
            Panel mainPanel = new Panel();
            mainPanel.Size = new Size(945, 650);
            mainPanel.Location = new Point((this.ClientSize.Width - mainPanel.Width) / 2, 20);
            mainPanel.BackColor = Color.FromArgb(248, 248, 248);
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(mainPanel);
            mainPanel.BringToFront();

            // HEADER SHADOW
            Panel headerShadow = new Panel();
            headerShadow.Size = new Size(945, 135);
            headerShadow.Location = new Point(0, 8);
            headerShadow.BackColor = Color.FromArgb(190, 190, 190);
            mainPanel.Controls.Add(headerShadow);

            // HEADER PANEL
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(945, 135);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.FromArgb(248, 248, 248);
            headerPanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(headerPanel);
            headerPanel.BringToFront();

            // PIXMART LOGO
            PictureBox logoPicture = new PictureBox();
            logoPicture.Size = new Size(350, 95);
            logoPicture.Location = new Point(295, 18);
            logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
            logoPicture.BackColor = Color.Transparent;

            Image logoImage = LoadIcon("pixmart-logo.png", 350, 95);

            if (logoImage != null)
            {
                logoPicture.Image = logoImage;
                headerPanel.Controls.Add(logoPicture);
            }
            else
            {
                Label lblLogo = new Label();
                lblLogo.Text = "PIXMART";
                lblLogo.Font = new Font("Impact", 48, FontStyle.Regular);
                lblLogo.ForeColor = Color.FromArgb(35, 35, 35);
                lblLogo.TextAlign = ContentAlignment.MiddleCenter;
                lblLogo.Size = new Size(350, 95);
                lblLogo.Location = new Point(295, 18);
                headerPanel.Controls.Add(lblLogo);
            }

            // PROFILE ICON
            Button btnProfileIcon = new Button();
            btnProfileIcon.Size = new Size(45, 45);
            btnProfileIcon.Location = new Point(810, 25);
            btnProfileIcon.BackColor = Color.Transparent;
            btnProfileIcon.FlatStyle = FlatStyle.Flat;
            btnProfileIcon.FlatAppearance.BorderSize = 0;
            btnProfileIcon.Cursor = Cursors.Hand;
            btnProfileIcon.Click += (s, e) =>
            {
                ProfileForm profile = new ProfileForm(currentUserID);
                profile.Show();
                this.Hide();
            };

            Image profileIcon = LoadIcon("profile-icon.png", 40, 40);

            if (profileIcon != null)
            {
                btnProfileIcon.Image = profileIcon;
            }
            else
            {
                btnProfileIcon.Text = "◉";
                btnProfileIcon.Font = new Font("Segoe UI Symbol", 22, FontStyle.Bold);
            }

            headerPanel.Controls.Add(btnProfileIcon);

            // GALLERY ICON
            Button btnGalleryIcon = new Button();
            btnGalleryIcon.Size = new Size(45, 45);
            btnGalleryIcon.Location = new Point(865, 25);
            btnGalleryIcon.BackColor = Color.Transparent;
            btnGalleryIcon.FlatStyle = FlatStyle.Flat;
            btnGalleryIcon.FlatAppearance.BorderSize = 0;
            btnGalleryIcon.Cursor = Cursors.Hand;
            btnGalleryIcon.Click += (s, e) =>
            {
                DownloadedGalleryForm gallery = new DownloadedGalleryForm(currentUserID);
                gallery.Show();
                this.Hide();
            };

            Image galleryIcon = LoadIcon("gallery-icon.png", 40, 40);

            if (galleryIcon != null)
            {
                btnGalleryIcon.Image = galleryIcon;
            }
            else
            {
                btnGalleryIcon.Text = "▧";
                btnGalleryIcon.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            }

            headerPanel.Controls.Add(btnGalleryIcon);

            // LEFT TITLE
            Label lblConfirmTitle = new Label();
            lblConfirmTitle.Text = "CONFIRM DOWNLOAD";
            lblConfirmTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblConfirmTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblConfirmTitle.Size = new Size(420, 50);
            lblConfirmTitle.Location = new Point(55, 160);
            mainPanel.Controls.Add(lblConfirmTitle);

            // IMAGE SHADOW
            Panel imageShadow = new Panel();
            imageShadow.Size = new Size(380, 300);
            imageShadow.Location = new Point(67, 225);
            imageShadow.BackColor = Color.FromArgb(180, 180, 180);
            imageShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(imageShadow);

            // IMAGE PANEL
            Panel imagePanel = new Panel();
            imagePanel.Size = new Size(380, 300);
            imagePanel.Location = new Point(55, 215);
            imagePanel.BackColor = Color.FromArgb(248, 248, 248);
            imagePanel.Paint += ImagePanelPaint;
            mainPanel.Controls.Add(imagePanel);
            imagePanel.BringToFront();

            pictureBoxArtwork = new PictureBox();
            pictureBoxArtwork.Size = new Size(380, 300);
            pictureBoxArtwork.Location = new Point(0, 0);
            pictureBoxArtwork.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxArtwork.BackColor = Color.FromArgb(248, 248, 248);
            imagePanel.Controls.Add(pictureBoxArtwork);

            // DESCRIPTION TITLE
            Label lblDescriptionTitle = new Label();
            lblDescriptionTitle.Text = "DESCRIPTION";
            lblDescriptionTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblDescriptionTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblDescriptionTitle.Size = new Size(250, 30);
            lblDescriptionTitle.Location = new Point(70, 535);
            mainPanel.Controls.Add(lblDescriptionTitle);

            // DESCRIPTION CONTENT
            lblDescription = new Label();
            lblDescription.Text = "";
            lblDescription.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblDescription.ForeColor = Color.FromArgb(60, 60, 60);
            lblDescription.Size = new Size(380, 65);
            lblDescription.Location = new Point(70, 565);
            lblDescription.AutoEllipsis = true;
            mainPanel.Controls.Add(lblDescription);

            // RIGHT CARD SHADOW
            Panel cardShadow = new Panel();
            cardShadow.Size = new Size(475, 455);
            cardShadow.Location = new Point(477, 165);
            cardShadow.BackColor = Color.FromArgb(190, 190, 190);
            cardShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(cardShadow);

            // RIGHT CARD
            Panel cardPanel = new Panel();
            cardPanel.Size = new Size(475, 455);
            cardPanel.Location = new Point(465, 155);
            cardPanel.BackColor = Color.FromArgb(235, 234, 231);
            cardPanel.Paint += CardPanelPaint;
            mainPanel.Controls.Add(cardPanel);
            cardPanel.BringToFront();

            // TITLE BOX
            Panel titleBox = CreateWhiteInfoBox(42, 48, 365, 58);
            cardPanel.Controls.Add(titleBox);

            lblTitle = new Label();
            lblTitle.Text = "ARTWORK TITLE";
            lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblTitle.Size = new Size(330, 35);
            lblTitle.Location = new Point(18, 12);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.AutoEllipsis = true;
            titleBox.Controls.Add(lblTitle);

            // ARTIST BOX
            Panel artistBox = CreateWhiteInfoBox(42, 122, 245, 34);
            cardPanel.Controls.Add(artistBox);

            lblPostedBy = new Label();
            lblPostedBy.Text = "ARTIST";
            lblPostedBy.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblPostedBy.ForeColor = Color.FromArgb(45, 45, 45);
            lblPostedBy.Size = new Size(220, 25);
            lblPostedBy.Location = new Point(14, 5);
            lblPostedBy.TextAlign = ContentAlignment.MiddleLeft;
            lblPostedBy.AutoEllipsis = true;
            artistBox.Controls.Add(lblPostedBy);

            // PRICE BOX
            Panel priceBox = CreateWhiteInfoBox(42, 170, 175, 66);
            cardPanel.Controls.Add(priceBox);

            lblPrice = new Label();
            lblPrice.Text = "PRICE";
            lblPrice.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(45, 45, 45);
            lblPrice.Size = new Size(145, 40);
            lblPrice.Location = new Point(16, 13);
            lblPrice.TextAlign = ContentAlignment.MiddleLeft;
            lblPrice.AutoEllipsis = true;
            priceBox.Controls.Add(lblPrice);

            // CATEGORY hidden only for code use
            lblCategory = new Label();
            lblCategory.Visible = false;
            cardPanel.Controls.Add(lblCategory);

            // DIVIDER LINE
            Label divider = new Label();
            divider.BackColor = Color.FromArgb(200, 200, 200);
            divider.Size = new Size(435, 1);
            divider.Location = new Point(20, 260);
            cardPanel.Controls.Add(divider);

            // QUESTION LABEL
            Label lblQuestion = new Label();
            lblQuestion.Text = "Do you want to download this artwork?";
            lblQuestion.Font = new Font("Segoe UI", 15, FontStyle.Regular);
            lblQuestion.ForeColor = Color.FromArgb(70, 70, 70);
            lblQuestion.Size = new Size(420, 40);
            lblQuestion.Location = new Point(58, 282);
            cardPanel.Controls.Add(lblQuestion);

            // CONFIRM SHADOW
            Panel confirmShadow = new Panel();
            confirmShadow.Size = new Size(175, 66);
            confirmShadow.Location = new Point(72, 346);
            confirmShadow.BackColor = Color.FromArgb(185, 185, 185);
            confirmShadow.Paint += RoundedShadowPaint;
            cardPanel.Controls.Add(confirmShadow);

            // CONFIRM BUTTON
            btnConfirmDownload = new Button();
            btnConfirmDownload.Text = "CONFIRM\nDOWNLOAD";
            btnConfirmDownload.Size = new Size(175, 66);
            btnConfirmDownload.Location = new Point(68, 338);
            btnConfirmDownload.BackColor = Color.FromArgb(35, 35, 35);
            btnConfirmDownload.ForeColor = Color.White;
            btnConfirmDownload.FlatStyle = FlatStyle.Flat;
            btnConfirmDownload.FlatAppearance.BorderSize = 0;
            btnConfirmDownload.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnConfirmDownload.Cursor = Cursors.Hand;
            btnConfirmDownload.Paint += RoundedButtonPaint;
            btnConfirmDownload.Click += btnConfirmDownload_Click;
            cardPanel.Controls.Add(btnConfirmDownload);
            btnConfirmDownload.BringToFront();

            // CANCEL SHADOW
            Panel cancelShadow = new Panel();
            cancelShadow.Size = new Size(175, 66);
            cancelShadow.Location = new Point(260, 346);
            cancelShadow.BackColor = Color.FromArgb(185, 185, 185);
            cancelShadow.Paint += RoundedShadowPaint;
            cardPanel.Controls.Add(cancelShadow);

            // CANCEL BUTTON
            btnCancel = new Button();
            btnCancel.Text = "CANCEL";
            btnCancel.Size = new Size(175, 66);
            btnCancel.Location = new Point(256, 338);
            btnCancel.BackColor = Color.FromArgb(145, 145, 145);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Paint += RoundedButtonPaint;
            btnCancel.Click += btnCancel_Click;
            cardPanel.Controls.Add(btnCancel);
            btnCancel.BringToFront();
        }

        private void LoadCheckoutDetails()
        {
            var artwork = DataStore.Artworks.FirstOrDefault(a => a.ArtworkID == selectedArtworkID);

            if (artwork != null)
            {
                lblTitle.Text = artwork.Title.ToUpper();
                lblPostedBy.Text = artwork.PostedBy.ToUpper();
                lblCategory.Text = artwork.Category;
                lblDescription.Text = artwork.Description;
                lblPrice.Text = "₱" + artwork.Price.ToString("0.00");

                pictureBoxArtwork.ImageLocation = artwork.ImagePath;
                pictureBoxArtwork.SizeMode = PictureBoxSizeMode.Zoom;

                // AUTO FIT TEXT
                FitLabelText(lblTitle, artwork.Title.ToUpper(), 12, 7);
                FitLabelText(lblPostedBy, artwork.PostedBy.ToUpper(), 11, 7);
                FitLabelText(lblPrice, "₱" + artwork.Price.ToString("0.00"), 12, 8);
                FitLabelText(lblDescription, artwork.Description, 10, 7);
            }
        }

        private void btnConfirmDownload_Click(object sender, EventArgs e)
        {
            Download download = new Download
            {
                DownloadID = DataStore.NextDownloadID++,
                UserID = currentUserID,
                ArtworkID = selectedArtworkID
            };

            DataStore.Downloads.Add(download);

            DownloadSuccessForm success = new DownloadSuccessForm(currentUserID, this);
            success.ShowDialog(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ArtworkDetailsForm details = new ArtworkDetailsForm(currentUserID, selectedArtworkID);
            details.Show();
            this.Hide();
        }

        private Panel CreateWhiteInfoBox(int x, int y, int width, int height)
        {
            Panel panel = new Panel();
            panel.Size = new Size(width, height);
            panel.Location = new Point(x, y);
            panel.BackColor = Color.White;
            panel.Paint += WhiteInfoBoxPaint;
            return panel;
        }

        private void FitLabelText(Label label, string text, float maxSize, float minSize)
        {
            label.Text = text;

            for (float size = maxSize; size >= minSize; size -= 0.5f)
            {
                Font testFont = new Font(label.Font.FontFamily, size, label.Font.Style);
                Size textSize = TextRenderer.MeasureText(text, testFont);

                if (textSize.Width <= label.Width && textSize.Height <= label.Height)
                {
                    label.Font = testFont;
                    return;
                }
            }

            label.Font = new Font(label.Font.FontFamily, minSize, label.Font.Style);
        }

        private Image LoadIcon(string fileName, int width, int height)
        {
            string path = Path.Combine(Application.StartupPath, fileName);

            if (File.Exists(path))
            {
                using (Image original = Image.FromFile(path))
                {
                    return new Bitmap(original, new Size(width, height));
                }
            }

            return null;
        }

        private void BottomBorderPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 2))
            {
                e.Graphics.DrawLine(pen, 0, panel.Height - 2, panel.Width, panel.Height - 2);
            }
        }

        private void ImagePanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void CardPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 20))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(235, 234, 231)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void WhiteInfoBoxPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void RoundedShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 18))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void RoundedButtonPaint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 18))
            {
                button.Region = new Region(path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);

            path.CloseFigure();
            return path;
        }

        // Keep this so old Designer event won't error
        private void CheckoutForm_Load(object sender, EventArgs e)
        {
            LoadCheckoutDetails();
        }
    }
}