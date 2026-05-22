using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class ProfileForm : Form
    {
        int currentUserID;

        private TextBox txtFullNameValue;
        private Panel detailsBox;

        public ProfileForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;

            DesignProfileForm();
            LoadUserDetails();
        }

        private void DesignProfileForm()
        {
            this.Text = "PIXMART Profile";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1350, 760);
            this.BackColor = Color.FromArgb(235, 235, 235);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
            mainPanel.BackColor = Color.FromArgb(242, 242, 242);
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

            // PROFILE BAR SHADOW
            Panel profileBarShadow = new Panel();
            profileBarShadow.Size = new Size(1120, 115);
            profileBarShadow.Location = new Point(0, 165);
            profileBarShadow.BackColor = Color.FromArgb(190, 190, 190);
            mainPanel.Controls.Add(profileBarShadow);

            // PROFILE BAR
            Panel profileBar = new Panel();
            profileBar.Size = new Size(1120, 115);
            profileBar.Location = new Point(0, 155);
            profileBar.BackColor = Color.FromArgb(248, 248, 248);
            profileBar.Paint += ProfileBarPaint;
            mainPanel.Controls.Add(profileBar);
            profileBar.BringToFront();

            Label lblProfileTitle = new Label();
            lblProfileTitle.Text = "PROFILE";
            lblProfileTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblProfileTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblProfileTitle.Size = new Size(300, 70);
            lblProfileTitle.Location = new Point(45, 25);
            lblProfileTitle.TextAlign = ContentAlignment.MiddleLeft;
            profileBar.Controls.Add(lblProfileTitle);

            // LOGOUT SHADOW
            Panel logoutShadow = new Panel();
            logoutShadow.Size = new Size(180, 68);
            logoutShadow.Location = new Point(881, 29);
            logoutShadow.BackColor = Color.FromArgb(185, 185, 185);
            logoutShadow.Paint += RoundedShadowPaint;
            profileBar.Controls.Add(logoutShadow);

            // LOGOUT BUTTON
            Button btnLogout = new Button();
            btnLogout.Text = "LOGOUT";
            btnLogout.Size = new Size(180, 68);
            btnLogout.Location = new Point(875, 23);
            btnLogout.BackColor = Color.FromArgb(35, 35, 35);
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Paint += RoundedButtonPaint;
            btnLogout.Click += btnLogout_Click;
            profileBar.Controls.Add(btnLogout);
            btnLogout.BringToFront();

            // USER DETAILS TITLE
            Label lblDetailsTitle = new Label();
            lblDetailsTitle.Text = "USER DETAILS";
            lblDetailsTitle.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblDetailsTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblDetailsTitle.Size = new Size(300, 35);
            lblDetailsTitle.Location = new Point(65, 320);
            mainPanel.Controls.Add(lblDetailsTitle);

            // DETAILS BOX SHADOW
            Panel detailsShadow = new Panel();
            detailsShadow.Size = new Size(1030, 185);
            detailsShadow.Location = new Point(53, 373);
            detailsShadow.BackColor = Color.FromArgb(195, 195, 195);
            detailsShadow.Paint += DetailsShadowPaint;
            mainPanel.Controls.Add(detailsShadow);

            // DETAILS BOX
            detailsBox = new Panel();
            detailsBox.Size = new Size(1030, 185);
            detailsBox.Location = new Point(45, 365);
            detailsBox.BackColor = Color.FromArgb(242, 242, 242);
            detailsBox.Paint += DetailsBoxPaint;
            mainPanel.Controls.Add(detailsBox);
            detailsBox.BringToFront();

            // NAME
            txtFullNameValue = CreateProfileTextBox("Name", 18);
            detailsBox.Controls.Add(txtFullNameValue);

            Button editName = CreateEditButton(14);
            editName.Click += (s, e) => ToggleEdit(txtFullNameValue, editName);
            detailsBox.Controls.Add(editName);

            // USERNAME
            txtUsername = CreateProfileTextBox("User Name", 63);
            detailsBox.Controls.Add(txtUsername);

            Button editUsername = CreateEditButton(59);
            editUsername.Click += (s, e) => ToggleEdit(txtUsername, editUsername);
            detailsBox.Controls.Add(editUsername);

            // PASSWORD
            txtPassword = CreateProfileTextBox("Password", 108);
            detailsBox.Controls.Add(txtPassword);

            Button editPassword = CreateEditButton(104);
            editPassword.Click += (s, e) => ToggleEdit(txtPassword, editPassword);
            detailsBox.Controls.Add(editPassword);

            // EMAIL
            txtEmail = CreateProfileTextBox("Email", 153);
            txtEmail.ReadOnly = true;
            detailsBox.Controls.Add(txtEmail);

            // BACK BUTTON SHADOW
            Panel backShadow = new Panel();
            backShadow.Size = new Size(130, 50);
            backShadow.Location = new Point(84, 598);
            backShadow.BackColor = Color.FromArgb(185, 185, 185);
            backShadow.Paint += RoundedShadowPaint;
            mainPanel.Controls.Add(backShadow);

            // BACK BUTTON
            btnBack = new Button();
            btnBack.Text = "BACK";
            btnBack.Size = new Size(130, 50);
            btnBack.Location = new Point(78, 592);
            btnBack.BackColor = Color.FromArgb(35, 35, 35);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBack.Cursor = Cursors.Hand;
            btnBack.Paint += RoundedButtonPaint;
            btnBack.Click += btnBack_Click;
            mainPanel.Controls.Add(btnBack);
            btnBack.BringToFront();
        }

        private TextBox CreateProfileTextBox(string placeholder, int top)
        {
            TextBox box = new TextBox();
            box.Text = placeholder;
            box.Location = new Point(25, top);
            box.Size = new Size(850, 30);
            box.Font = new Font("Segoe UI", 10);
            box.BorderStyle = BorderStyle.None;
            box.BackColor = Color.FromArgb(242, 242, 242);
            box.ForeColor = Color.Gray;
            box.ReadOnly = true;
            return box;
        }

        private Button CreateEditButton(int top)
        {
            Button button = new Button();
            button.Text = "Edit";
            button.Size = new Size(70, 28);
            button.Location = new Point(925, top);
            button.BackColor = Color.FromArgb(242, 242, 242);
            button.ForeColor = Color.FromArgb(0, 80, 180);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 9);
            button.Cursor = Cursors.Hand;
            return button;
        }

        private void ToggleEdit(TextBox textBox, Button editButton)
        {
            if (editButton.Text == "Edit")
            {
                textBox.ReadOnly = false;
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
                textBox.Focus();
                editButton.Text = "Save";
            }
            else
            {
                SaveProfileChanges();
                textBox.ReadOnly = true;
                textBox.BackColor = Color.FromArgb(242, 242, 242);
                textBox.ForeColor = Color.Black;
                editButton.Text = "Edit";
            }
        }

        private void LoadUserDetails()
        {
            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);

            if (user != null)
            {
                txtFullNameValue.Text = user.FullName;
                txtUsername.Text = user.Username;
                txtPassword.Text = user.Password;
                txtEmail.Text = user.Email;

                txtFullNameValue.ForeColor = Color.Black;
                txtUsername.ForeColor = Color.Black;
                txtPassword.ForeColor = Color.Black;
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void SaveProfileChanges()
        {
            if (txtFullNameValue.Text == "" ||
                txtUsername.Text == "" ||
                txtPassword.Text == "")
            {
                MessageBox.Show("Please complete all editable fields.");
                return;
            }

            var existingUser = DataStore.Users.FirstOrDefault(u =>
                u.Username == txtUsername.Text &&
                u.UserID != currentUserID
            );

            if (existingUser != null)
            {
                MessageBox.Show("Username is already taken.");
                LoadUserDetails();
                return;
            }

            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);

            if (user != null)
            {
                user.FullName = txtFullNameValue.Text;
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;

                MessageBox.Show("Profile updated successfully!");
                LoadUserDetails();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
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

        private void ProfileBarPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (Pen purplePen = new Pen(Color.FromArgb(98, 0, 238), 3))
            {
                e.Graphics.DrawLine(purplePen, 0, 0, panel.Width, 0);
            }
        }

        private void DetailsBoxPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (Pen border = new Pen(Color.FromArgb(45, 45, 45), 2))
            {
                e.Graphics.DrawRectangle(border, 0, 0, panel.Width - 1, panel.Height - 1);
            }

            using (Pen line = new Pen(Color.FromArgb(45, 45, 45), 2))
            {
                e.Graphics.DrawLine(line, 20, 44, panel.Width - 20, 44);
                e.Graphics.DrawLine(line, 20, 89, panel.Width - 20, 89);
                e.Graphics.DrawLine(line, 20, 134, panel.Width - 20, 134);
            }
        }

        private void DetailsShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(195, 195, 195)))
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
        private void ProfileForm_Load(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void btnEdit_Click(object sender, EventArgs e) { }
        private void btnSave_Click(object sender, EventArgs e) { }
    }
}