using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class DashboardForm : Form
    {
        int currentUserID;

        private ComboBox cmbCategory;
        private TextBox txtSearchBox;
        private FlowLayoutPanel artworkPanel;

        public DashboardForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;

            DesignDashboard();
            LoadArtworks();
        }

        private void DesignDashboard()
        {
            this.Text = "PIXMART Dashboard";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1350, 760);
            this.BackColor = Color.FromArgb(235, 235, 235);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.Controls.Clear();

            // MAIN PANEL
            Panel mainPanel = new Panel();
            mainPanel.Size = new Size(1120, 720);
            mainPanel.Location = new Point(
                (this.ClientSize.Width - mainPanel.Width) / 2,
                10
            );
            mainPanel.BackColor = Color.FromArgb(242, 242, 242);
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(mainPanel);

            // HEADER SHADOW
            Panel headerShadow = new Panel();
            headerShadow.Size = new Size(1120, 155);
            headerShadow.Location = new Point(0, 6);
            headerShadow.BackColor = Color.FromArgb(205, 205, 205);
            mainPanel.Controls.Add(headerShadow);

            // HEADER PANEL
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(1120, 155);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.FromArgb(248, 248, 248);
            headerPanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(headerPanel);
            headerPanel.BringToFront();

            // UPLOAD BUTTON SHADOW
            Panel uploadShadow = new Panel();
            uploadShadow.Size = new Size(180, 68);
            uploadShadow.Location = new Point(35, 33);
            uploadShadow.BackColor = Color.FromArgb(190, 190, 190);
            uploadShadow.Paint += RoundedShadowPaint;
            headerPanel.Controls.Add(uploadShadow);

            // UPLOAD BUTTON
            Button btnUploadNew = new Button();
            btnUploadNew.Text = "UPLOAD";
            btnUploadNew.Size = new Size(180, 68);
            btnUploadNew.Location = new Point(30, 28);
            btnUploadNew.BackColor = Color.FromArgb(35, 35, 35);
            btnUploadNew.ForeColor = Color.White;
            btnUploadNew.FlatStyle = FlatStyle.Flat;
            btnUploadNew.FlatAppearance.BorderSize = 0;
            btnUploadNew.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnUploadNew.Cursor = Cursors.Hand;
            btnUploadNew.Paint += RoundedButtonPaint;
            btnUploadNew.Click += btnUpload_Click;

            Image uploadIcon = LoadIcon("upload-icon.png", 34, 34);

            if (uploadIcon != null)
            {
                btnUploadNew.Image = uploadIcon;
                btnUploadNew.ImageAlign = ContentAlignment.MiddleLeft;
                btnUploadNew.TextAlign = ContentAlignment.MiddleRight;
                btnUploadNew.Padding = new Padding(18, 0, 22, 0);
            }
            else
            {
                btnUploadNew.Text = "⇧  UPLOAD";
            }

            headerPanel.Controls.Add(btnUploadNew);
            btnUploadNew.BringToFront();

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
            Button btnProfileNew = new Button();
            btnProfileNew.Size = new Size(50, 50);
            btnProfileNew.Location = new Point(970, 25);
            btnProfileNew.BackColor = Color.Transparent;
            btnProfileNew.FlatStyle = FlatStyle.Flat;
            btnProfileNew.FlatAppearance.BorderSize = 0;
            btnProfileNew.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnProfileNew.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 235, 235);
            btnProfileNew.Cursor = Cursors.Hand;
            btnProfileNew.Click += btnProfile_Click;

            Image profileIcon = LoadIcon("profile-icon.png", 42, 42);

            if (profileIcon != null)
            {
                btnProfileNew.Image = profileIcon;
                btnProfileNew.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                btnProfileNew.Text = "◉";
                btnProfileNew.Font = new Font("Segoe UI Symbol", 22, FontStyle.Bold);
                btnProfileNew.ForeColor = Color.FromArgb(35, 35, 35);
            }

            headerPanel.Controls.Add(btnProfileNew);

            // DOWNLOADED GALLERY ICON
            Button btnGalleryNew = new Button();
            btnGalleryNew.Size = new Size(50, 50);
            btnGalleryNew.Location = new Point(1035, 25);
            btnGalleryNew.BackColor = Color.Transparent;
            btnGalleryNew.FlatStyle = FlatStyle.Flat;
            btnGalleryNew.FlatAppearance.BorderSize = 0;
            btnGalleryNew.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnGalleryNew.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 235, 235);
            btnGalleryNew.Cursor = Cursors.Hand;
            btnGalleryNew.Click += btnGallery_Click;

            Image galleryIcon = LoadIcon("gallery-icon.png", 42, 42);

            if (galleryIcon != null)
            {
                btnGalleryNew.Image = galleryIcon;
                btnGalleryNew.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                btnGalleryNew.Text = "▧";
                btnGalleryNew.Font = new Font("Segoe UI", 24, FontStyle.Bold);
                btnGalleryNew.ForeColor = Color.FromArgb(35, 35, 35);
            }

            headerPanel.Controls.Add(btnGalleryNew);

            // FILTER SHADOW
            Panel filterShadow = new Panel();
            filterShadow.Size = new Size(1120, 115);
            filterShadow.Location = new Point(0, 162);
            filterShadow.BackColor = Color.FromArgb(205, 205, 205);
            mainPanel.Controls.Add(filterShadow);

            // FILTER PANEL
            Panel filterPanel = new Panel();
            filterPanel.Size = new Size(1120, 115);
            filterPanel.Location = new Point(0, 155);
            filterPanel.BackColor = Color.FromArgb(248, 248, 248);
            filterPanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(filterPanel);
            filterPanel.BringToFront();

            // CATEGORY SHADOW
            Panel categoryShadow = new Panel();
            categoryShadow.Size = new Size(180, 42);
            categoryShadow.Location = new Point(154, 42);
            categoryShadow.BackColor = Color.FromArgb(205, 205, 205);
            categoryShadow.Paint += RoundedShadowPaint;
            filterPanel.Controls.Add(categoryShadow);

            // CATEGORY PANEL
            Panel categoryPanel = new Panel();
            categoryPanel.Size = new Size(180, 42);
            categoryPanel.Location = new Point(150, 38);
            categoryPanel.BackColor = Color.White;
            categoryPanel.Paint += RoundedWhitePanelPaint;
            filterPanel.Controls.Add(categoryPanel);
            categoryPanel.BringToFront();

            cmbCategory = new ComboBox();
            cmbCategory.Size = new Size(160, 30);
            cmbCategory.Location = new Point(10, 7);
            cmbCategory.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.BackColor = Color.White;

            cmbCategory.Items.Add("CATEGORY");
            cmbCategory.Items.Add("Digital Arts");
            cmbCategory.Items.Add("Photography");
            cmbCategory.Items.Add("Abstract Art");
            cmbCategory.SelectedIndex = 0;

            categoryPanel.Controls.Add(cmbCategory);

            // SEARCH SHADOW
            Panel searchShadow = new Panel();
            searchShadow.Size = new Size(605, 42);
            searchShadow.Location = new Point(374, 42);
            searchShadow.BackColor = Color.FromArgb(205, 205, 205);
            searchShadow.Paint += RoundedShadowPaint;
            filterPanel.Controls.Add(searchShadow);

            // SEARCH PANEL
            Panel searchPanel = new Panel();
            searchPanel.Size = new Size(605, 42);
            searchPanel.Location = new Point(370, 38);
            searchPanel.BackColor = Color.White;
            searchPanel.Paint += RoundedWhitePanelPaint;
            filterPanel.Controls.Add(searchPanel);
            searchPanel.BringToFront();

            txtSearchBox = new TextBox();
            txtSearchBox.Size = new Size(520, 26);
            txtSearchBox.Location = new Point(18, 10);
            txtSearchBox.Font = new Font("Segoe UI", 12);
            txtSearchBox.BorderStyle = BorderStyle.None;
            txtSearchBox.BackColor = Color.White;
            txtSearchBox.TextChanged += (s, e) => LoadArtworks(txtSearchBox.Text);
            searchPanel.Controls.Add(txtSearchBox);

            // SEARCH BUTTON SHADOW
            Panel searchBtnShadow = new Panel();
            searchBtnShadow.Size = new Size(60, 42);
            searchBtnShadow.Location = new Point(979, 42);
            searchBtnShadow.BackColor = Color.FromArgb(205, 205, 205);
            searchBtnShadow.Paint += RoundedShadowPaint;
            filterPanel.Controls.Add(searchBtnShadow);

            // SEARCH BUTTON
            Button btnSearchNew = new Button();
            btnSearchNew.Text = "⌕";
            btnSearchNew.Size = new Size(60, 42);
            btnSearchNew.Location = new Point(975, 38);
            btnSearchNew.BackColor = Color.FromArgb(35, 35, 35);
            btnSearchNew.ForeColor = Color.White;
            btnSearchNew.FlatStyle = FlatStyle.Flat;
            btnSearchNew.FlatAppearance.BorderSize = 0;
            btnSearchNew.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            btnSearchNew.Cursor = Cursors.Hand;
            btnSearchNew.Paint += RoundedButtonPaint;
            btnSearchNew.Click += (s, e) => LoadArtworks(txtSearchBox.Text);
            filterPanel.Controls.Add(btnSearchNew);
            btnSearchNew.BringToFront();

            cmbCategory.SelectedIndexChanged += (s, e) => LoadArtworks(txtSearchBox.Text);

            // ARTWORK DISPLAY AREA
            artworkPanel = new FlowLayoutPanel();
            artworkPanel.Size = new Size(1060, 420);
            artworkPanel.Location = new Point(30, 295);
            artworkPanel.BackColor = Color.FromArgb(238, 238, 238);
            artworkPanel.AutoScroll = true;
            artworkPanel.WrapContents = true;
            artworkPanel.FlowDirection = FlowDirection.LeftToRight;
            artworkPanel.Padding = new Padding(15);
            mainPanel.Controls.Add(artworkPanel);
        }

        private void LoadArtworks(string searchKeyword = "")
        {
            if (artworkPanel == null)
            {
                return;
            }

            artworkPanel.Controls.Clear();

            var artworks = DataStore.Artworks.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                searchKeyword = searchKeyword.ToLower();

                artworks = artworks.Where(a =>
                    a.Title.ToLower().Contains(searchKeyword) ||
                    a.Category.ToLower().Contains(searchKeyword) ||
                    a.PostedBy.ToLower().Contains(searchKeyword) ||
                    a.Price.ToString("0.00").Contains(searchKeyword)
                );
            }

            if (cmbCategory != null && cmbCategory.Text != "CATEGORY")
            {
                string selectedCategory = cmbCategory.Text;

                artworks = artworks.Where(a =>
                    a.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)
                );
            }

            foreach (Artwork artwork in artworks)
            {
                AddArtworkCard(artwork);
            }
        }

        private void AddArtworkCard(Artwork artwork)
        {
            Panel cardShadow = new Panel();
            cardShadow.Width = 220;
            cardShadow.Height = 295;
            cardShadow.Margin = new Padding(18);
            cardShadow.BackColor = Color.FromArgb(210, 210, 210);
            cardShadow.Paint += ArtworkShadowPaint;

            Panel panel = new Panel();
            panel.Width = 210;
            panel.Height = 280;
            panel.Location = new Point(0, 0);
            panel.BackColor = Color.White;
            panel.Paint += ArtworkCardPaint;

            PictureBox picture = new PictureBox();
            picture.Width = 180;
            picture.Height = 130;
            picture.Top = 15;
            picture.Left = 15;
            picture.ImageLocation = artwork.ImagePath;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.BackColor = Color.LightGray;

            Label title = new Label();
            title.Text = artwork.Title;
            title.Top = 155;
            title.Left = 15;
            title.Width = 180;
            title.Height = 22;
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            Label postedBy = new Label();
            postedBy.Text = "By: " + artwork.PostedBy;
            postedBy.Top = 180;
            postedBy.Left = 15;
            postedBy.Width = 180;
            postedBy.Height = 22;
            postedBy.ForeColor = Color.DimGray;

            Label category = new Label();
            category.Text = artwork.Category;
            category.Top = 205;
            category.Left = 15;
            category.Width = 180;
            category.Height = 22;
            category.ForeColor = Color.Gray;

            Label price = new Label();
            price.Text = "Price: ₱" + artwork.Price.ToString("0.00");
            price.Top = 230;
            price.Left = 15;
            price.Width = 180;
            price.Height = 22;

            Button btnView = new Button();
            btnView.Text = "View";
            btnView.Top = 250;
            btnView.Left = 15;
            btnView.Width = 180;
            btnView.Height = 28;
            btnView.BackColor = Color.Black;
            btnView.ForeColor = Color.White;
            btnView.FlatStyle = FlatStyle.Flat;
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Cursor = Cursors.Hand;

            int artworkID = artwork.ArtworkID;

            btnView.Click += (s, e) =>
            {
                ArtworkDetailsForm details = new ArtworkDetailsForm(currentUserID, artworkID);
                details.Show();
                this.Hide();
            };

            panel.Controls.Add(picture);
            panel.Controls.Add(title);
            panel.Controls.Add(postedBy);
            panel.Controls.Add(category);
            panel.Controls.Add(price);
            panel.Controls.Add(btnView);

            cardShadow.Controls.Add(panel);
            artworkPanel.Controls.Add(cardShadow);
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

        private void RoundedShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 18))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(205, 205, 205)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void RoundedWhitePanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 18))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.FromArgb(225, 225, 225), 1))
                {
                    e.Graphics.DrawPath(pen, path);
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

        private void ArtworkShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(6, 6, panel.Width - 8, panel.Height - 8);

            using (GraphicsPath path = GetRoundedPath(rect, 12))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(205, 205, 205)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void ArtworkCardPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 12))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
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

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profile = new ProfileForm(currentUserID);
            profile.Show();
            this.Hide();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadArtworkForm upload = new UploadArtworkForm(currentUserID);
            upload.Show();
            this.Hide();
        }

        private void btnGallery_Click(object sender, EventArgs e)
        {
            DownloadedGalleryForm gallery = new DownloadedGalleryForm(currentUserID);
            gallery.Show();
            this.Hide();
        }

        // These are kept to prevent errors from old Designer events.
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadArtworks();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Logout button removed from dashboard design.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSearchBox != null)
            {
                txtSearchBox.Clear();
            }

            LoadArtworks();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchBox != null)
            {
                LoadArtworks(txtSearchBox.Text);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchBox != null)
            {
                LoadArtworks(txtSearchBox.Text);
            }
        }
    }
}