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
    public partial class UploadArtworkForm : Form
    {
        int currentUserID;
        string imagePath = "";
        public UploadArtworkForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "" || txtCategory.Text == "" || txtDescription.Text == "" || txtprice.Text == "" || imagePath == "")
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
                PostedBy = user.FullName,
                Title = txtTitle.Text,
                Category = txtCategory.Text,
                Description = txtDescription.Text,
                Price = decimal.TryParse(txtprice.Text, out var Price) ? price : 0,
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
                pictureBoxArtwork.ImageLocation = imagePath;
                pictureBoxArtwork.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
