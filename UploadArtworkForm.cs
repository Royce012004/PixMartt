using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class UploadArtworkForm : Form
    {
        int currentUserID;
        string imagePath = "";

        private ComboBox cmbCategory;
        private Panel pictureShadowPanel;
        private Panel pictureContainerPanel;

        public UploadArtworkForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;

            DesignUploadArtworkForm();
        }

        private void DesignUploadArtworkForm()
        {
            this.Text = "PIXMART Upload Artwork";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1350, 760);
            this.BackColor = Color.FromArgb(235, 235, 235);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = AutoScaleMode.None;

            this.Controls.Clear();

            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);
            string artistName = user != null ? user.FullName : "Artist";

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

            // LOGO
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

            // WELCOME BAR SHADOW
            Panel welcomeShadow = new Panel();
            welcomeShadow.Size = new Size(1120, 115);
            welcomeShadow.Location = new Point(0, 165);
            welcomeShadow.BackColor = Color.FromArgb(185, 185, 185);
            mainPanel.Controls.Add(welcomeShadow);

            // WELCOME BAR
            Panel welcomePanel = new Panel();
            welcomePanel.Size = new Size(1120, 115);
            welcomePanel.Location = new Point(0, 155);
            welcomePanel.BackColor = Color.FromArgb(248, 248, 248);
            welcomePanel.Paint += BottomBorderPaint;
            mainPanel.Controls.Add(welcomePanel);
            welcomePanel.BringToFront();

            Label lblWelcome = new Label();
            lblWelcome.Text = "WELCOME, " + artistName.ToUpper();
            lblWelcome.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(45, 45, 45);
            lblWelcome.Size = new Size(850, 70);
            lblWelcome.Location = new Point(50, 25);
            lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
            welcomePanel.Controls.Add(lblWelcome);

            // UPLOAD TITLE
            Label lblUploadTitle = new Label();
            lblUploadTitle.Text = "UPLOAD ARTWORK";
            lblUploadTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblUploadTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblUploadTitle.Size = new Size(350, 45);
            lblUploadTitle.Location = new Point(55, 300);
            mainPanel.Controls.Add(lblUploadTitle);

            // PICTURE SHADOW PANEL (hidden at first)
            pictureShadowPanel = new Panel();
            pictureShadowPanel.Size = new Size(430, 275);
            pictureShadowPanel.Location = new Point(110, 370);
            pictureShadowPanel.BackColor = Color.FromArgb(145, 145, 145);
            pictureShadowPanel.Visible = false;
            pictureShadowPanel.Paint += PictureShadowPaint;
            mainPanel.Controls.Add(pictureShadowPanel);

            // PICTURE CONTAINER (no gray placeholder)
            pictureContainerPanel = new Panel();
            pictureContainerPanel.Size = new Size(430, 275);
            pictureContainerPanel.Location = new Point(100, 360);
            pictureContainerPanel.BackColor = Color.Transparent;
            pictureContainerPanel.Visible = false;
            pictureContainerPanel.Paint += PictureContainerPaint;
            mainPanel.Controls.Add(pictureContainerPanel);
            pictureContainerPanel.BringToFront();

            pictureBoxArtwork = new PictureBox();
            pictureBoxArtwork.Size = new Size(430, 275);
            pictureBoxArtwork.Location = new Point(0, 0);
            pictureBoxArtwork.BackColor = Color.Transparent;
            pictureBoxArtwork.SizeMode = PictureBoxSizeMode.Zoom;
            pictureContainerPanel.Controls.Add(pictureBoxArtwork);

            // BROWSE BUTTON
            Button btnBrowseNew = new Button();
            btnBrowseNew.Text = "BROWSE";
            btnBrowseNew.Size = new Size(135, 34);
            btnBrowseNew.Location = new Point(245, 655);
            btnBrowseNew.BackColor = Color.White;
            btnBrowseNew.ForeColor = Color.FromArgb(45, 45, 45);
            btnBrowseNew.FlatStyle = FlatStyle.Flat;
            btnBrowseNew.FlatAppearance.BorderSize = 1;
            btnBrowseNew.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBrowseNew.Cursor = Cursors.Hand;
            btnBrowseNew.Paint += SmallRoundedButtonPaint;
            btnBrowseNew.Click += btnBrowse_Click;

            Image uploadIcon = LoadIcon("upload-icon.png", 22, 22);

            if (uploadIcon != null)
            {
                btnBrowseNew.Image = uploadIcon;
                btnBrowseNew.ImageAlign = ContentAlignment.MiddleLeft;
                btnBrowseNew.TextAlign = ContentAlignment.MiddleRight;
                btnBrowseNew.Padding = new Padding(10, 0, 12, 0);
            }

            mainPanel.Controls.Add(btnBrowseNew);
            btnBrowseNew.BringToFront();

            // RIGHT FORM AREA
            int labelX = 590;
            int fieldX = 710;
            int startY = 365;
            int spacing = 45;

            Label lblTitle = CreateFieldLabel("TITLE", labelX, startY);
            mainPanel.Controls.Add(lblTitle);

            Panel titlePanel = CreateInputPanel(fieldX, startY - 2, 390, 28);
            mainPanel.Controls.Add(titlePanel);

            txtTitle = CreateInputTextBox();
            titlePanel.Controls.Add(txtTitle);

            Label lblDescription = CreateFieldLabel("DESCRIPTION", labelX, startY + spacing);
            mainPanel.Controls.Add(lblDescription);

            Panel descriptionPanel = CreateInputPanel(fieldX, startY + spacing - 2, 390, 28);
            mainPanel.Controls.Add(descriptionPanel);

            txtDescription = CreateInputTextBox();
            descriptionPanel.Controls.Add(txtDescription);

            Label lblCategory = CreateFieldLabel("CATEGORY", labelX, startY + spacing * 2);
            mainPanel.Controls.Add(lblCategory);

            Panel categoryPanel = CreateInputPanel(fieldX, startY + spacing * 2 - 2, 390, 28);
            mainPanel.Controls.Add(categoryPanel);

            cmbCategory = new ComboBox();
            cmbCategory.Size = new Size(370, 24);
            cmbCategory.Location = new Point(10, 2);
            cmbCategory.Font = new Font("Segoe UI", 10);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.BackColor = Color.White;
            cmbCategory.Items.Add("Digital Arts");
            cmbCategory.Items.Add("Photography");
            cmbCategory.Items.Add("Abstract Art");
            cmbCategory.SelectedIndex = 0;
            categoryPanel.Controls.Add(cmbCategory);

            Label lblPrice = CreateFieldLabel("PRICE", labelX, startY + spacing * 3);
            mainPanel.Controls.Add(lblPrice);

            Panel pricePanel = CreateInputPanel(fieldX, startY + spacing * 3 - 2, 210, 28);
            mainPanel.Controls.Add(pricePanel);

            txtprice = CreateInputTextBox();
            pricePanel.Controls.Add(txtprice);

            // UPLOAD BUTTON SHADOW
            Panel uploadShadow = new Panel();
            uploadShadow.Size = new Size(210, 80);
            uploadShadow.Location = new Point(606, 558);
            uploadShadow.BackColor = Color.FromArgb(185, 185, 185);
            uploadShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(uploadShadow);

            // UPLOAD BUTTON
            Button btnUploadNew = new Button();
            btnUploadNew.Text = "UPLOAD";
            btnUploadNew.Size = new Size(210, 80);
            btnUploadNew.Location = new Point(600, 550);
            btnUploadNew.BackColor = Color.FromArgb(35, 35, 35);
            btnUploadNew.ForeColor = Color.White;
            btnUploadNew.FlatStyle = FlatStyle.Flat;
            btnUploadNew.FlatAppearance.BorderSize = 0;
            btnUploadNew.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUploadNew.Cursor = Cursors.Hand;
            btnUploadNew.Paint += RoundedButtonPaint;
            btnUploadNew.Click += btnUpload_Click;
            mainPanel.Controls.Add(btnUploadNew);
            btnUploadNew.BringToFront();

            // CANCEL BUTTON SHADOW
            Panel cancelShadow = new Panel();
            cancelShadow.Size = new Size(210, 80);
            cancelShadow.Location = new Point(846, 558);
            cancelShadow.BackColor = Color.FromArgb(185, 185, 185);
            cancelShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(cancelShadow);

            // CANCEL BUTTON
            Button btnCancelNew = new Button();
            btnCancelNew.Text = "CANCEL";
            btnCancelNew.Size = new Size(210, 80);
            btnCancelNew.Location = new Point(840, 550);
            btnCancelNew.BackColor = Color.FromArgb(125, 125, 125);
            btnCancelNew.ForeColor = Color.White;
            btnCancelNew.FlatStyle = FlatStyle.Flat;
            btnCancelNew.FlatAppearance.BorderSize = 0;
            btnCancelNew.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnCancelNew.Cursor = Cursors.Hand;
            btnCancelNew.Paint += RoundedButtonPaint;
            btnCancelNew.Click += btnCancel_Click;
            mainPanel.Controls.Add(btnCancelNew);
            btnCancelNew.BringToFront();
        }

        private Label CreateFieldLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(130, 30);
            label.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(45, 45, 45);
            label.TextAlign = ContentAlignment.MiddleLeft;
            return label;
        }

        private Panel CreateInputPanel(int x, int y, int width, int height)
        {
            Panel panel = new Panel();
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, height);
            panel.BackColor = Color.White;
            panel.Paint += InputPanelPaint;
            return panel;
        }

        private TextBox CreateInputTextBox()
        {
            TextBox box = new TextBox();
            box.Location = new Point(10, 5);
            box.Size = new Size(360, 20);
            box.Font = new Font("Segoe UI", 10);
            box.BorderStyle = BorderStyle.None;
            box.BackColor = Color.White;
            return box;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "" ||
                cmbCategory.Text == "" ||
                txtDescription.Text == "" ||
                txtprice.Text == "" ||
                imagePath == "")
            {
                MessageBox.Show("Please complete all fields and select an image.");
                return;
            }

            decimal price;

            if (!decimal.TryParse(txtprice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);

            Artwork artwork = new Artwork
            {
                ArtworkID = DataStore.NextArtworkID++,
                UserID = currentUserID,
                PostedBy = user != null ? user.FullName : "Unknown Artist",
                Title = txtTitle.Text,
                Category = cmbCategory.Text,
                Description = txtDescription.Text,
                Price = price,
                ImagePath = imagePath
            };

            DataStore.Artworks.Add(artwork);

            MessageBox.Show("Artwork posted successfully!");

            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFile.FileName;

                pictureBoxArtwork.Image = Image.FromFile(imagePath);
                pictureBoxArtwork.SizeMode = PictureBoxSizeMode.Zoom;

                pictureContainerPanel.Visible = true;
                pictureShadowPanel.Visible = true;

                pictureContainerPanel.BringToFront();
            }
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

        private void InputPanelPaint(object sender, PaintEventArgs e)
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

                using (Pen pen = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void RoundedGrayPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 20))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(215, 215, 215)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void PictureContainerPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 20))
            {
                panel.Region = new Region(path);
            }
        }

        private void PictureShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 20))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 150, 150)))
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

            using (GraphicsPath path = GetRoundedPath(rect, 20))
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

            using (GraphicsPath path = GetRoundedPath(rect, 20))
            {
                button.Region = new Region(path);
            }
        }

        private void SmallRoundedButtonPaint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
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

        // Kept to avoid old Designer event errors
        private void UploadArtworkForm_Load(object sender, EventArgs e) { }
    }
}