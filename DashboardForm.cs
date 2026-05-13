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
    public partial class DashboardForm : Form
    {
        int currentUserID;
        public DashboardForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profile = new ProfileForm(currentUserID);
            profile.Show();
            this.Hide();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadArtworks();
        }
        private void LoadArtworks()
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (Artwork artwork in DataStore.Artworks)
            {
                Panel panel = new Panel();
                panel.Width = 210;
                panel.Height = 260;
                panel.BorderStyle = BorderStyle.FixedSingle;

                PictureBox picture = new PictureBox();
                picture.Width = 160;
                picture.Height = 120;
                picture.Top = 10;
                picture.Left = 15;
                picture.ImageLocation = artwork.ImagePath;
                picture.SizeMode = PictureBoxSizeMode.StretchImage;

                Label title = new Label();
                title.Text = artwork.Title;
                title.Top = 140;
                title.Left = 15;
                title.Width = 170;
                title.Height = 20;

                Label postedBy = new Label();
                postedBy.Text = "By: " + artwork.PostedBy;
                postedBy.Top = 165;
                postedBy.Left = 15;
                postedBy.Width = 180;
                postedBy.Height = 20;

                Label price = new Label();
                price.Text = "Price: ₱" + artwork.Price.ToString("0.00");
                price.Top = 190;
                price.Left = 15;
                price.Width = 180;
                price.Height = 20;

                Button btnView = new Button();
                btnView.Text = "View";
                btnView.Top = 220;
                btnView.Left = 15;
                btnView.Width = 100;

                int artworkID = artwork.ArtworkID;

                btnView.Click += (s, e) =>
                {
                    ArtworkDetailsForm details = new ArtworkDetailsForm(currentUserID, artworkID);
                    details.Show();
                    this.Hide();
                };

                panel.Controls.Add(picture);
                panel.Controls.Add(title);
                panel.Controls.Add(postedBy);
                panel.Controls.Add(btnView);
                panel.Controls.Add(price);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadArtworkForm upload = new UploadArtworkForm(currentUserID);
            upload.Show();
            this.Hide();
        }

        private void btnGallery_Click(object sender, EventArgs e)
        {
            DownloadedGalleryForm gallery = new DownloadedGalleryForm(currentUserID);
            gallery.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide(); 
        }
    }
}
