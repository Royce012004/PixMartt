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
    public partial class DownloadedGalleryForm : Form
    {
        int currentUserID;
        public DownloadedGalleryForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
        }

        private void DownloadedGalleryForm_Load(object sender, EventArgs e)
        {
            LoadDownloadedArtworks();
        }
        private void LoadDownloadedArtworks()
        {
            flowLayoutPanel1.Controls.Clear();

            var downloadedArtworks =
                from download in DataStore.Downloads
                join artwork in DataStore.Artworks
                on download.ArtworkID equals artwork.ArtworkID
                where download.UserID == currentUserID
                select artwork;

            foreach (Artwork artwork in downloadedArtworks)
            {
                Panel panel = new Panel();
                panel.Width = 180;
                panel.Height = 200;
                panel.BorderStyle = BorderStyle.FixedSingle;

                PictureBox picture = new PictureBox();
                picture.Width = 150;
                picture.Height = 120;
                picture.Top = 10;
                picture.Left = 15;
                picture.ImageLocation = artwork.ImagePath;
                picture.SizeMode = PictureBoxSizeMode.StretchImage;

                Label title = new Label();
                title.Text = artwork.Title;
                title.Top = 135;
                title.Left = 15;
                title.Width = 150;

                Label postedBy = new Label();
                postedBy.Text = "By: " + artwork.PostedBy;
                postedBy.Top = 160;
                postedBy.Left = 15;
                postedBy.Width = 150;

                panel.Controls.Add(picture);
                panel.Controls.Add(title);
                panel.Controls.Add(postedBy);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }
    }
}
