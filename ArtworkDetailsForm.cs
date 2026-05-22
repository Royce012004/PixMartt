using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class ArtworkDetailsForm : Form
    {
        int currentUserID;
        int selectedArtworkID;

        private ComboBox cmbCategory;
        private TextBox txtSearch;

        public ArtworkDetailsForm(int userID, int artworkID)
        {
            InitializeComponent();
            currentUserID = userID;
            selectedArtworkID = artworkID;

            DesignArtworkDetailsForm();
            LoadArtworkDetails();
        }

        private void DesignArtworkDetailsForm()
        {
            this.Text = "PIXMART Artwork Details";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1350, 760);
            this.BackColor = Color.FromArgb(235, 235, 235);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = AutoScaleMode.None;

            this.Controls.Clear();

            // MAIN SHADOW
            Panel mainShadow = new Panel();
            mainShadow.Size = new Size(1120, 720);
            mainShadow.Location = new Point((this.ClientSize.Width - 1120) / 2 + 8, 18);
            mainShadow.BackColor = Color.FromArgb(190, 190, 190);
            this.Controls.Add(mainShadow);

            // MAIN PANEL
            Panel mainPanel = new Panel();
            mainPanel.Size = new Size(1120, 720);
            mainPanel.Location = new Point((this.ClientSize.Width - mainPanel.Width) / 2, 10);
            mainPanel.BackColor = Color.FromArgb(248, 248, 248);
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(mainPanel);
            mainPanel.BringToFront();

            // HEADER SHADOW
            Panel headerShadow = new Panel();
            headerShadow.Size = new Size(1120, 155);
            headerShadow.Location = new Point(0, 8);
            headerShadow.BackColor = Color.FromArgb(195, 195, 195);
            mainPanel.Controls.Add(headerShadow);

            // HEADER
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(1120, 155);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.FromArgb(248, 248, 248);
            headerPanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(headerPanel);
            headerPanel.BringToFront();

            // UPLOAD BUTTON SHADOW
            Panel uploadHeaderShadow = new Panel();
            uploadHeaderShadow.Size = new Size(180, 68);
            uploadHeaderShadow.Location = new Point(37, 35);
            uploadHeaderShadow.BackColor = Color.FromArgb(185, 185, 185);
            uploadHeaderShadow.Paint += RoundedShadowPaint;
            headerPanel.Controls.Add(uploadHeaderShadow);

            // UPLOAD BUTTON
            Button btnUploadHeader = new Button();
            btnUploadHeader.Text = "UPLOAD";
            btnUploadHeader.Size = new Size(180, 68);
            btnUploadHeader.Location = new Point(30, 28);
            btnUploadHeader.BackColor = Color.FromArgb(35, 35, 35);
            btnUploadHeader.ForeColor = Color.White;
            btnUploadHeader.FlatStyle = FlatStyle.Flat;
            btnUploadHeader.FlatAppearance.BorderSize = 0;
            btnUploadHeader.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnUploadHeader.Cursor = Cursors.Hand;
            btnUploadHeader.Paint += RoundedButtonPaint;
            btnUploadHeader.Click += (s, e) =>
            {
                UploadArtworkForm upload = new UploadArtworkForm(currentUserID);
                upload.Show();
                this.Hide();
            };

            Image uploadIcon = LoadIcon("upload-icon.png", 35, 35);

            if (uploadIcon != null)
            {
                btnUploadHeader.Image = uploadIcon;
                btnUploadHeader.ImageAlign = ContentAlignment.MiddleLeft;
                btnUploadHeader.TextAlign = ContentAlignment.MiddleRight;
                btnUploadHeader.Padding = new Padding(20, 0, 25, 0);
            }

            headerPanel.Controls.Add(btnUploadHeader);
            btnUploadHeader.BringToFront();

            // PIXMART LOGO
            PictureBox logoPicture = new PictureBox();
            logoPicture.Size = new Size(430, 110);
            logoPicture.Location = new Point(345, 20);
            logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
            logoPicture.BackColor = Color.Transparent;

            Image logoImage = LoadIcon("pixmart-logo.png", 430, 110);

            if (logoImage != null)
            {
                logoPicture.Image = logoImage;
                headerPanel.Controls.Add(logoPicture);
            }
            else
            {
                Label lblLogo = new Label();
                lblLogo.Text = "PIXMART";
                lblLogo.Font = new Font("Impact", 54, FontStyle.Regular);
                lblLogo.ForeColor = Color.FromArgb(35, 35, 35);
                lblLogo.TextAlign = ContentAlignment.MiddleCenter;
                lblLogo.Size = new Size(430, 110);
                lblLogo.Location = new Point(345, 20);
                headerPanel.Controls.Add(lblLogo);
            }

            // PROFILE ICON
            Button btnProfileIcon = new Button();
            btnProfileIcon.Size = new Size(50, 50);
            btnProfileIcon.Location = new Point(970, 25);
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

            Image profileIcon = LoadIcon("profile-icon.png", 42, 42);

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
            btnGalleryIcon.Size = new Size(50, 50);
            btnGalleryIcon.Location = new Point(1035, 25);
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

            Image galleryIcon = LoadIcon("gallery-icon.png", 42, 42);

            if (galleryIcon != null)
            {
                btnGalleryIcon.Image = galleryIcon;
            }
            else
            {
                btnGalleryIcon.Text = "▧";
                btnGalleryIcon.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            }

            headerPanel.Controls.Add(btnGalleryIcon);

            // SEARCH BAR SHADOW
            Panel searchShadow = new Panel();
            searchShadow.Size = new Size(1120, 115);
            searchShadow.Location = new Point(0, 165);
            searchShadow.BackColor = Color.FromArgb(185, 185, 185);
            mainPanel.Controls.Add(searchShadow);

            // SEARCH PANEL
            Panel searchPanel = new Panel();
            searchPanel.Size = new Size(1120, 115);
            searchPanel.Location = new Point(0, 155);
            searchPanel.BackColor = Color.FromArgb(248, 248, 248);
            searchPanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(searchPanel);
            searchPanel.BringToFront();

            // CATEGORY SHADOW
            Panel categoryShadow = new Panel();
            categoryShadow.Size = new Size(180, 44);
            categoryShadow.Location = new Point(158, 39);
            categoryShadow.BackColor = Color.FromArgb(190, 190, 190);
            categoryShadow.Paint += SmallRoundedShadowPaint;
            searchPanel.Controls.Add(categoryShadow);

            // CATEGORY BOX
            Panel categoryBox = new Panel();
            categoryBox.Size = new Size(180, 44);
            categoryBox.Location = new Point(150, 33);
            categoryBox.BackColor = Color.White;
            categoryBox.Paint += SmallWhiteRoundedPanelPaint;
            searchPanel.Controls.Add(categoryBox);
            categoryBox.BringToFront();

            cmbCategory = new ComboBox();
            cmbCategory.Size = new Size(150, 28);
            cmbCategory.Location = new Point(15, 9);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cmbCategory.Items.Add("CATEGORY");
            cmbCategory.Items.Add("Digital Arts");
            cmbCategory.Items.Add("Photography");
            cmbCategory.Items.Add("Abstract Art");
            cmbCategory.SelectedIndex = 0;
            categoryBox.Controls.Add(cmbCategory);

            // SEARCH BOX SHADOW
            Panel searchBoxShadow = new Panel();
            searchBoxShadow.Size = new Size(605, 44);
            searchBoxShadow.Location = new Point(385, 39);
            searchBoxShadow.BackColor = Color.FromArgb(190, 190, 190);
            searchBoxShadow.Paint += SmallRoundedShadowPaint;
            searchPanel.Controls.Add(searchBoxShadow);

            // SEARCH BOX
            Panel searchBox = new Panel();
            searchBox.Size = new Size(605, 44);
            searchBox.Location = new Point(378, 33);
            searchBox.BackColor = Color.White;
            searchBox.Paint += SmallWhiteRoundedPanelPaint;
            searchPanel.Controls.Add(searchBox);
            searchBox.BringToFront();

            txtSearch = new TextBox();
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Location = new Point(15, 12);
            txtSearch.Size = new Size(520, 24);
            txtSearch.Font = new Font("Segoe UI", 11);
            searchBox.Controls.Add(txtSearch);

            // SEARCH BUTTON
            Button btnSearch = new Button();
            btnSearch.Size = new Size(62, 44);
            btnSearch.Location = new Point(983, 33);
            btnSearch.BackColor = Color.FromArgb(35, 35, 35);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI Symbol", 22, FontStyle.Bold);
            btnSearch.Text = "⌕";
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Paint += SearchButtonPaint;
            btnSearch.Click += (s, e) =>
            {
                DashboardForm dashboard = new DashboardForm(currentUserID);
                dashboard.Show();
                this.Hide();
            };
            searchPanel.Controls.Add(btnSearch);
            btnSearch.BringToFront();

            // BACK ICON BUTTON
            btnBack = new Button();
            btnBack.Size = new Size(48, 48);
            btnBack.Location = new Point(20, 292);
            btnBack.BackColor = Color.Transparent;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Click += btnBack_Click;

            Image backIcon = LoadIcon("back-icon.png", 42, 42);

            if (backIcon != null)
            {
                btnBack.Image = backIcon;
            }
            else
            {
                btnBack.Text = "←";
                btnBack.Font = new Font("Segoe UI", 24, FontStyle.Bold);
                btnBack.ForeColor = Color.FromArgb(45, 45, 45);
            }

            mainPanel.Controls.Add(btnBack);

            // IMAGE SHADOW
            Panel imageShadow = new Panel();
            imageShadow.Size = new Size(305, 305);
            imageShadow.Location = new Point(128, 329);
            imageShadow.BackColor = Color.FromArgb(180, 180, 180);
            imageShadow.Paint += ArtworkImageShadowPaint;
            mainPanel.Controls.Add(imageShadow);

            // IMAGE PANEL
            Panel imagePanel = new Panel();
            imagePanel.Size = new Size(305, 305);
            imagePanel.Location = new Point(120, 320);
            imagePanel.BackColor = Color.White;
            imagePanel.Paint += ArtworkImagePanelPaint;
            mainPanel.Controls.Add(imagePanel);
            imagePanel.BringToFront();

            pictureBoxArtwork = new PictureBox();
            pictureBoxArtwork.Size = new Size(305, 305);
            pictureBoxArtwork.Location = new Point(0, 0);
            pictureBoxArtwork.BackColor = Color.White;
            pictureBoxArtwork.SizeMode = PictureBoxSizeMode.Zoom;
            imagePanel.Controls.Add(pictureBoxArtwork);

            // DETAILS
            lblTitle = new Label();
            lblTitle.Text = "TITLE";
            lblTitle.Location = new Point(540, 340);
            lblTitle.Size = new Size(430, 55);
            lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            mainPanel.Controls.Add(lblTitle);

            lblPostedBy = new Label();
            lblPostedBy.Text = "ARTIST NAME";
            lblPostedBy.Location = new Point(545, 395);
            lblPostedBy.Size = new Size(430, 35);
            lblPostedBy.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            lblPostedBy.ForeColor = Color.Black;
            mainPanel.Controls.Add(lblPostedBy);

            lblCategory = new Label();
            lblCategory.Text = "CATEGORY";
            lblCategory.Location = new Point(545, 435);
            lblCategory.Size = new Size(430, 35);
            lblCategory.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblCategory.ForeColor = Color.Black;
            mainPanel.Controls.Add(lblCategory);

            lblDescription = new Label();
            lblDescription.Text = "DESCRIPTION";
            lblDescription.Location = new Point(545, 475);
            lblDescription.Size = new Size(430, 60);
            lblDescription.Font = new Font("Segoe UI", 15, FontStyle.Regular);
            lblDescription.ForeColor = Color.Black;
            mainPanel.Controls.Add(lblDescription);

            lblPrice = new Label();
            lblPrice.Text = "₱0.00";
            lblPrice.Location = new Point(545, 535);
            lblPrice.Size = new Size(130, 45);
            lblPrice.BackColor = Color.FromArgb(130, 130, 130);
            lblPrice.ForeColor = Color.White;
            lblPrice.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblPrice.TextAlign = ContentAlignment.MiddleCenter;
            SetRoundedRegion(lblPrice, 18);
            mainPanel.Controls.Add(lblPrice);

            // DOWNLOAD BUTTON SHADOW
            Panel downloadShadow = new Panel();
            downloadShadow.Size = new Size(380, 72);
            downloadShadow.Location = new Point(563, 616);
            downloadShadow.BackColor = Color.FromArgb(185, 185, 185);
            downloadShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(downloadShadow);

            // DOWNLOAD / BUY BUTTON
            btnDownload = new Button();
            btnDownload.Text = "DOWNLOAD / BUY";
            btnDownload.Size = new Size(380, 72);
            btnDownload.Location = new Point(555, 607);
            btnDownload.BackColor = Color.FromArgb(35, 35, 35);
            btnDownload.ForeColor = Color.White;
            btnDownload.FlatStyle = FlatStyle.Flat;
            btnDownload.FlatAppearance.BorderSize = 0;
            btnDownload.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnDownload.Cursor = Cursors.Hand;
            btnDownload.Paint += RoundedButtonPaint;
            btnDownload.Click += btnDownload_Click;
            mainPanel.Controls.Add(btnDownload);
            btnDownload.BringToFront();
        }

        private void LoadArtworkDetails()
        {
            var artwork = DataStore.Artworks.FirstOrDefault(a => a.ArtworkID == selectedArtworkID);

            if (artwork != null)
            {
                lblTitle.Text = artwork.Title.ToUpper();
                lblPostedBy.Text = artwork.PostedBy;
                lblCategory.Text = artwork.Category.ToUpper();
                lblDescription.Text = artwork.Description;
                lblPrice.Text = "₱" + artwork.Price.ToString("0.00");

                pictureBoxArtwork.ImageLocation = artwork.ImagePath;
                pictureBoxArtwork.SizeMode = PictureBoxSizeMode.Zoom;

                if (artwork.UserID == currentUserID)
                {
                    btnDownload.Enabled = false;
                    btnDownload.Text = "YOUR ARTWORK";
                    btnDownload.BackColor = Color.FromArgb(120, 120, 120);
                }
                else
                {
                    btnDownload.Enabled = true;
                    btnDownload.Text = "DOWNLOAD / BUY";
                    btnDownload.BackColor = Color.FromArgb(35, 35, 35);
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            CheckoutForm checkout = new CheckoutForm(currentUserID, selectedArtworkID);
            checkout.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
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

        private void SmallWhiteRoundedPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 10))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void SmallRoundedShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 10))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(190, 190, 190)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void SearchButtonPaint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 10))
            {
                button.Region = new Region(path);
            }
        }

        private void ArtworkImageShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(180, 180, 180)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void ArtworkImagePanelPaint(object sender, PaintEventArgs e)
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

        private void SetRoundedRegion(Control control, int radius)
        {
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            {
                control.Region = new Region(path);
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

        // Kept to avoid old Designer event errors
        private void ArtworkDetailsForm_Load(object sender, EventArgs e)
        {
            LoadArtworkDetails();
        }
    }
}