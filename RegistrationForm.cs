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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            User newUser = new User
            {
                UserID = DataStore.NextUserID++,
                FullName = txtFullName.Text,
                Username = txtUserName.Text,
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
