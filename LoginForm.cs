using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
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

