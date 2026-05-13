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
    public partial class ProfileForm : Form
    {
        int currentUserID;
        public ProfileForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);

            if (user != null)
            {
                lblFullName.Text = "Full Name: " + user.FullName;
                lblUsername.Text = "Username: " + user.Username;
                lblEmail.Text = "Email: " + user.Email;
            }
        }
    }
}
