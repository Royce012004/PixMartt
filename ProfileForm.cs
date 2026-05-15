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
        private void ProfileForm_Load(object sender, EventArgs e)
        {
            LoadUserDetails();
            SetEditMode(false);
        }
        private void LoadUserDetails()
        {
            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);

            if (user != null)
            {
                lblFullName.Text = "Full Name: " + user.FullName;

                txtUsername.Text = user.Username;
                txtPassword.Text = user.Password;
                txtEmail.Text = user.Email;
            }
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

      
        
        private void SetEditMode(bool isEditing)
        {
            txtUsername.Enabled = isEditing;
            txtPassword.Enabled = isEditing;
            txtEmail.Enabled = isEditing;

            btnSave.Visible = isEditing;
            btnEdit.Visible = !isEditing;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            var existingUser = DataStore.Users.FirstOrDefault(u =>
                u.Username == txtUsername.Text &&
                u.UserID != currentUserID
            );

            if (existingUser != null)
            {
                MessageBox.Show("Username is already taken.");
                return;
            }

            var user = DataStore.Users.FirstOrDefault(u => u.UserID == currentUserID);

            if (user != null)
            {
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;
                user.Email = txtEmail.Text;

                MessageBox.Show("Profile updated successfully!");

                LoadUserDetails();
                SetEditMode(false);
            }
        }
    }
}
