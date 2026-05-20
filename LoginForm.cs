using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace PixMartt
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            DesignLoginForm();
        }

        private void DesignLoginForm()
        {
            this.Text = "PIXMART Login";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1300, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(235, 235, 235);
            this.BackColor = Color.FromArgb(235, 235, 235);

            this.Controls.Clear();

            // MAIN CARD
            Panel mainCard = new Panel();
            mainCard.Size = new Size(880, 640);
            mainCard.Location = new Point(
                (this.ClientSize.Width - mainCard.Width) / 2,
                (this.ClientSize.Height - mainCard.Height) / 2
            );
            mainCard.BackColor = Color.White;
            mainCard.Paint += RoundedPanelPaint;
            this.Controls.Add(mainCard);

            // LEFT IMAGE
            PictureBox picLogin = new PictureBox();
            picLogin.Location = new Point(0, 0);
            picLogin.Size = new Size(445, 640);
            picLogin.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogin.BackColor = Color.White;

            string imagePath = Path.Combine(Application.StartupPath, "login-art.png");
            if (File.Exists(imagePath))
            {
                picLogin.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Image not found:\n" + imagePath);
            }

            mainCard.Controls.Add(picLogin);

            // RIGHT LOGIN PANEL
            Panel loginPanel = new Panel();
            loginPanel.Location = new Point(445, 0);
            loginPanel.Size = new Size(435, 640);
            loginPanel.Paint += LoginPanelPaint;
            mainCard.Controls.Add(loginPanel);

            // X BUTTON
            Label lblClose = new Label();
            lblClose.Text = "×";
            lblClose.Font = new Font("Segoe UI", 24, FontStyle.Regular);
            lblClose.ForeColor = Color.Gray;
            lblClose.AutoSize = true;
            lblClose.BackColor = Color.Transparent;
            lblClose.Location = new Point(390, 18);
            lblClose.Cursor = Cursors.Hand;
            lblClose.Click += (s, e) => Application.Exit();
            loginPanel.Controls.Add(lblClose);
            lblClose.Parent = loginPanel;

            // TITLE
            lblTitle = new Label();
            lblTitle.Text = "LOG IN / SIGN UP";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Size = new Size(300, 35);
            lblTitle.Location = new Point(70, 85);
            loginPanel.Controls.Add(lblTitle);
            lblTitle.Parent = loginPanel;

            // USERNAME
            txtUsername = new TextBox();
            txtUsername.Size = new Size(300, 32);
            txtUsername.Location = new Point(70, 135);
            txtUsername.Font = new Font("Segoe UI", 11);
            txtUsername.ForeColor = Color.Gray;
            txtUsername.Text = "Email or username";
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.GotFocus += RemoveUsernamePlaceholder;
            txtUsername.LostFocus += AddUsernamePlaceholder;
            loginPanel.Controls.Add(txtUsername);

            // PASSWORD
            txtPassword = new TextBox();
            txtPassword.Size = new Size(300, 32);
            txtPassword.Location = new Point(70, 178);
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.ForeColor = Color.Gray;
            txtPassword.Text = "Password";
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.GotFocus += RemovePasswordPlaceholder;
            txtPassword.LostFocus += AddPasswordPlaceholder;
            loginPanel.Controls.Add(txtPassword);

            // LOGIN BUTTON
            btnLogin = new Button();
            btnLogin.Text = "LOG IN";
            btnLogin.Size = new Size(300, 44);
            btnLogin.Location = new Point(70, 290);
            btnLogin.BackColor = Color.Black;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += btnLogin_Click;
            loginPanel.Controls.Add(btnLogin);

            // CREATE ACCOUNT BUTTON
            btnCreateAccount = new Button();
            btnCreateAccount.Text = "Create your account";
            btnCreateAccount.Size = new Size(300, 44);
            btnCreateAccount.Location = new Point(70, 360);
            btnCreateAccount.BackColor = Color.White;
            btnCreateAccount.ForeColor = Color.Black;
            btnCreateAccount.FlatStyle = FlatStyle.Flat;
            btnCreateAccount.FlatAppearance.BorderColor = Color.Gray;
            btnCreateAccount.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            btnCreateAccount.Cursor = Cursors.Hand;
            btnCreateAccount.Click += btnCreateAccount_Click_1;
            loginPanel.Controls.Add(btnCreateAccount);
        }

        private void LoginPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                panel.ClientRectangle,
                Color.FromArgb(120, 120, 120),
                Color.White,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, panel.ClientRectangle);
            }
        }

        private void RoundedPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            int radius = 12;

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.LightGray, 1))
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

        private void RemoveUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Email or username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void AddUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Email or username";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void RemovePasswordPlaceholder(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '●';
            }
        }

        private void AddPasswordPlaceholder(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.PasswordChar = '\0';
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Email or username" || txtPassword.Text == "Password")
            {
                MessageBox.Show("Please enter your username and password.");
                return;
            }

            var user = DataStore.Users.FirstOrDefault(u =>
             u.Username == txtUsername.Text &&
             u.Password == txtPassword.Text
         );

            if (user != null)
            {
                MessageBox.Show("Login successful!");

                DashboardForm dashboard = new DashboardForm(user.UserID);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void btnCreateAccount_Click_1(object sender, EventArgs e)
        {
            RegistrationForm register = new RegistrationForm();
            register.Show();
            this.Hide();
        }
    }
}

