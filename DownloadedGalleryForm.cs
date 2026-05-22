using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class DownloadedGalleryForm : Form
    {
        int currentUserID;

        private FlowLayoutPanel downloadedPanel;

        public DownloadedGalleryForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;

            DesignDownloadedGalleryForm();
            LoadDownloadedArtworks();
        }

        private void DesignDownloadedGalleryForm()
        {
            this.Text = "PIXMART Purchase Gallery";
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

            // HEADER PANEL
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(1120, 155);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.FromArgb(248, 248, 248);
            headerPanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(headerPanel);
            headerPanel.BringToFront();

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
            btnProfileIcon.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnProfileIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 235, 235);
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
                btnProfileIcon.ForeColor = Color.FromArgb(35, 35, 35);
            }

            headerPanel.Controls.Add(btnProfileIcon);

            // GALLERY ICON
            Button btnGalleryIcon = new Button();
            btnGalleryIcon.Size = new Size(50, 50);
            btnGalleryIcon.Location = new Point(1035, 25);
            btnGalleryIcon.BackColor = Color.Transparent;
            btnGalleryIcon.FlatStyle = FlatStyle.Flat;
            btnGalleryIcon.FlatAppearance.BorderSize = 0;
            btnGalleryIcon.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnGalleryIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 235, 235);
            btnGalleryIcon.Cursor = Cursors.Hand;

            Image galleryIcon = LoadIcon("gallery-icon.png", 42, 42);

            if (galleryIcon != null)
            {
                btnGalleryIcon.Image = galleryIcon;
            }
            else
            {
                btnGalleryIcon.Text = "▧";
                btnGalleryIcon.Font = new Font("Segoe UI", 24, FontStyle.Bold);
                btnGalleryIcon.ForeColor = Color.FromArgb(35, 35, 35);
            }

            headerPanel.Controls.Add(btnGalleryIcon);

            // TITLE BAR SHADOW
            Panel titleBarShadow = new Panel();
            titleBarShadow.Size = new Size(1120, 115);
            titleBarShadow.Location = new Point(0, 165);
            titleBarShadow.BackColor = Color.FromArgb(185, 185, 185);
            mainPanel.Controls.Add(titleBarShadow);

            // TITLE BAR
            Panel titleBar = new Panel();
            titleBar.Size = new Size(1120, 115);
            titleBar.Location = new Point(0, 155);
            titleBar.BackColor = Color.FromArgb(248, 248, 248);
            titleBar.Paint += TitleBarPaint;
            mainPanel.Controls.Add(titleBar);
            titleBar.BringToFront();

            Label lblTitle = new Label();
            lblTitle.Text = "PURCHASE GALLERY";
            lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblTitle.Size = new Size(500, 70);
            lblTitle.Location = new Point(40, 25);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            titleBar.Controls.Add(lblTitle);

            // CONTENT SHADOW
            Panel contentShadow = new Panel();
            contentShadow.Size = new Size(1030, 315);
            contentShadow.Location = new Point(53, 318);
            contentShadow.BackColor = Color.FromArgb(200, 200, 200);
            contentShadow.Paint += ContentShadowPaint;
            mainPanel.Controls.Add(contentShadow);

            // DOWNLOADED ARTWORK AREA
            downloadedPanel = new FlowLayoutPanel();
            downloadedPanel.Size = new Size(1030, 315);
            downloadedPanel.Location = new Point(45, 310);
            downloadedPanel.BackColor = Color.FromArgb(250, 250, 250);
            downloadedPanel.AutoScroll = true;
            downloadedPanel.WrapContents = true;
            downloadedPanel.FlowDirection = FlowDirection.LeftToRight;
            downloadedPanel.Padding = new Padding(18);
            downloadedPanel.Paint += ContentPanelPaint;
            mainPanel.Controls.Add(downloadedPanel);
            downloadedPanel.BringToFront();

            // BACK BUTTON SHADOW
            Panel backShadow = new Panel();
            backShadow.Size = new Size(130, 50);
            backShadow.Location = new Point(501, 657);
            backShadow.BackColor = Color.FromArgb(185, 185, 185);
            backShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(backShadow);

            // BACK BUTTON
            Button btnBackNew = new Button();
            btnBackNew.Text = "BACK";
            btnBackNew.Size = new Size(130, 50);
            btnBackNew.Location = new Point(495, 650);
            btnBackNew.BackColor = Color.FromArgb(35, 35, 35);
            btnBackNew.ForeColor = Color.White;
            btnBackNew.FlatStyle = FlatStyle.Flat;
            btnBackNew.FlatAppearance.BorderSize = 0;
            btnBackNew.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBackNew.Cursor = Cursors.Hand;
            btnBackNew.Paint += RoundedButtonPaint;
            btnBackNew.Click += btnBack_Click;
            mainPanel.Controls.Add(btnBackNew);
            btnBackNew.BringToFront();
        }

        private void LoadDownloadedArtworks()
        {
            if (downloadedPanel == null)
            {
                return;
            }

            downloadedPanel.Controls.Clear();

            var downloadedArtworks =
                from download in DataStore.Downloads
                join artwork in DataStore.Artworks
                on download.ArtworkID equals artwork.ArtworkID
                where download.UserID == currentUserID
                select artwork;

            if (!downloadedArtworks.Any())
            {
                Label emptyLabel = new Label();
                emptyLabel.Text = "No purchased artworks yet.";
                emptyLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                emptyLabel.ForeColor = Color.Gray;
                emptyLabel.Size = new Size(980, 250);
                emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
                downloadedPanel.Controls.Add(emptyLabel);
                return;
            }

            foreach (Artwork artwork in downloadedArtworks)
            {
                AddDownloadedArtworkCard(artwork);
            }
        }

        private void AddDownloadedArtworkCard(Artwork artwork)
        {
            Panel cardShadow = new Panel();
            cardShadow.Width = 210;
            cardShadow.Height = 250;
            cardShadow.Margin = new Padding(15);
            cardShadow.BackColor = Color.FromArgb(205, 205, 205);
            cardShadow.Paint += ArtworkShadowPaint;

            Panel card = new Panel();
            card.Width = 200;
            card.Height = 240;
            card.Location = new Point(0, 0);
            card.BackColor = Color.White;
            card.Paint += ArtworkCardPaint;

            PictureBox picture = new PictureBox();
            picture.Width = 170;
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
            title.Width = 170;
            title.Height = 25;
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(35, 35, 35);

            Label postedBy = new Label();
            postedBy.Text = "By: " + artwork.PostedBy;
            postedBy.Top = 182;
            postedBy.Left = 15;
            postedBy.Width = 170;
            postedBy.Height = 22;
            postedBy.Font = new Font("Segoe UI", 9);
            postedBy.ForeColor = Color.DimGray;

            Label category = new Label();
            category.Text = artwork.Category;
            category.Top = 205;
            category.Left = 15;
            category.Width = 170;
            category.Height = 22;
            category.Font = new Font("Segoe UI", 9);
            category.ForeColor = Color.Gray;

            card.Controls.Add(picture);
            card.Controls.Add(title);
            card.Controls.Add(postedBy);
            card.Controls.Add(category);

            cardShadow.Controls.Add(card);
            downloadedPanel.Controls.Add(cardShadow);
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

        private void TitleBarPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 2))
            {
                e.Graphics.DrawLine(pen, 0, panel.Height - 2, panel.Width, panel.Height - 2);
            }
        }

        private void ContentPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen border = new Pen(Color.FromArgb(225, 225, 225), 1))
            {
                e.Graphics.DrawRectangle(border, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void ContentShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(200, 200, 200)))
            {
                e.Graphics.FillRectangle(brush, 0, 0, panel.Width, panel.Height);
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

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(185, 185, 185)))
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

        // Kept to avoid old Designer event errors
        private void DownloadedGalleryForm_Load(object sender, EventArgs e)
        {
            LoadDownloadedArtworks();
        }
    }
}