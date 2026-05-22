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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            DesignRegistrationForm();
        }

        private void DesignRegistrationForm()
        {
            this.Text = "Create Account";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1300, 760);
            this.BackColor = Color.FromArgb(235, 235, 235);

            this.Controls.Clear();

            // MAIN CARD - same style as LoginForm
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
            PictureBox picRegister = new PictureBox();
            picRegister.Location = new Point(0, 0);
            picRegister.Size = new Size(445, 640);
            picRegister.SizeMode = PictureBoxSizeMode.StretchImage;
            picRegister.BackColor = Color.White;

            string imagePath = Path.Combine(Application.StartupPath, "login-art.png");

            if (File.Exists(imagePath))
            {
                picRegister.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Image not found:\n" + imagePath);
            }

            mainCard.Controls.Add(picRegister);

            // RIGHT GRADIENT PANEL
            Panel registerPanel = new Panel();
            registerPanel.Location = new Point(445, 0);
            registerPanel.Size = new Size(435, 640);
            registerPanel.Paint += RegisterPanelPaint;
            mainCard.Controls.Add(registerPanel);

            // X BUTTON
            Label lblClose = new Label();
            lblClose.Text = "×";
            lblClose.Font = new Font("Segoe UI", 24, FontStyle.Regular);
            lblClose.ForeColor = Color.Gray;
            lblClose.BackColor = Color.Transparent;
            lblClose.AutoSize = true;
            lblClose.Location = new Point(390, 18);
            lblClose.Cursor = Cursors.Hand;
            lblClose.Click += btnBack_Click;
            registerPanel.Controls.Add(lblClose);

            // TITLE
            lblTitle = new Label();
            lblTitle.Text = "CREATE ACCOUNT";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Size = new Size(300, 35);
            lblTitle.Location = new Point(70, 85);
            registerPanel.Controls.Add(lblTitle);

            // FULL NAME
            txtFullName = new TextBox();
            txtFullName.Size = new Size(300, 32);
            txtFullName.Location = new Point(70, 140);
            txtFullName.Font = new Font("Segoe UI", 11);
            txtFullName.ForeColor = Color.Gray;
            txtFullName.Text = "Full Name";
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.GotFocus += RemoveFullNamePlaceholder;
            txtFullName.LostFocus += AddFullNamePlaceholder;
            registerPanel.Controls.Add(txtFullName);

            // USERNAME
            txtUsername = new TextBox();
            txtUsername.Size = new Size(300, 32);
            txtUsername.Location = new Point(70, 195);
            txtUsername.Font = new Font("Segoe UI", 11);
            txtUsername.ForeColor = Color.Gray;
            txtUsername.Text = "Username";
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.GotFocus += RemoveUsernamePlaceholder;
            txtUsername.LostFocus += AddUsernamePlaceholder;
            registerPanel.Controls.Add(txtUsername);

            // PASSWORD
            txtPassword = new TextBox();
            txtPassword.Size = new Size(300, 32);
            txtPassword.Location = new Point(70, 250);
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.ForeColor = Color.Gray;
            txtPassword.Text = "Password";
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.GotFocus += RemovePasswordPlaceholder;
            txtPassword.LostFocus += AddPasswordPlaceholder;
            registerPanel.Controls.Add(txtPassword);

            // EMAIL
            txtEmail = new TextBox();
            txtEmail.Size = new Size(300, 32);
            txtEmail.Location = new Point(70, 305);
            txtEmail.Font = new Font("Segoe UI", 11);
            txtEmail.ForeColor = Color.Gray;
            txtEmail.Text = "Email";
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.GotFocus += RemoveEmailPlaceholder;
            txtEmail.LostFocus += AddEmailPlaceholder;
            registerPanel.Controls.Add(txtEmail);

            // GET STARTED BUTTON
            btnRegister = new Button();
            btnRegister.Text = "GET STARTED";
            btnRegister.Size = new Size(300, 44);
            btnRegister.Location = new Point(70, 385);
            btnRegister.BackColor = Color.Black;
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Click += btnRegister_Click;
            registerPanel.Controls.Add(btnRegister);

        }

        private void RegisterPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                panel.ClientRectangle,
                Color.FromArgb(150, 150, 150),
                Color.FromArgb(235, 235, 235),
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

                using (Pen pen = new Pen(Color.White, 1))
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

        private void RemoveFullNamePlaceholder(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Full Name")
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = Color.Black;
            }
        }

        private void AddFullNamePlaceholder(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")
            {
                txtFullName.Text = "Full Name";
                txtFullName.ForeColor = Color.Gray;
            }
        }

        private void RemoveUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void AddUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Username";
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

        private void RemoveEmailPlaceholder(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void AddEmailPlaceholder(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Color.Gray;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text == "" || txtFullName.Text == "Full Name" ||
            txtUsername.Text == "" || txtUsername.Text == "Username" ||
            txtPassword.Text == "" || txtPassword.Text == "Password" ||
            txtEmail.Text == "" || txtEmail.Text == "Email")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            bool usernameExists = DataStore.Users.Any(u => u.Username == txtUsername.Text);

            if (usernameExists)
            {
                MessageBox.Show("Username already exists. Please choose another username.");
                return;
            }

            User newUser = new User
            {
                UserID = DataStore.NextUserID++,
                FullName = txtFullName.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Email = txtEmail.Text
            };

            DataStore.Users.Add(newUser);

            MessageBox.Show("Account created successfully!");

            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }
    }
}
