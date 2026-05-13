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
    public partial class ArtworkDetailsForm : Form
    {
        int currentUserID;
        int selectedArtworkID;
        public ArtworkDetailsForm(int userID, int artworkID)
        {
            InitializeComponent();
            currentUserID = userID;
            selectedArtworkID = artworkID;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            CheckoutForm checkout = new CheckoutForm(currentUserID, selectedArtworkID);
            checkout.Show();
            this.Hide();
        }

        private void ArtworkDetailsForm_Load(object sender, EventArgs e)
        {
            var artwork = DataStore.Artworks.FirstOrDefault(a => a.ArtworkID == selectedArtworkID);

            if (artwork != null)
            {
                lblTitle.Text = artwork.Title;
                lblPostedBy.Text = "Posted By: " + artwork.PostedBy;
                lblCategory.Text = "Category: " + artwork.Category;
                lblPrice.Text = "Price: ₱" + artwork.Price.ToString("0.00");
                lblDescription.Text = artwork.Description;

                pictureBoxArtwork.ImageLocation = artwork.ImagePath;
                pictureBoxArtwork.SizeMode = PictureBoxSizeMode.StretchImage;

                if (artwork.UserID == currentUserID)
                {
                    btnDownload.Enabled = false;
                    btnDownload.Text = "Your Artwork";
                }
                else
                {
                    btnDownload.Enabled = true;
                    btnDownload.Text = "Download";
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm(currentUserID);
            dashboard.Show();
            this.Hide();
        }
    }
}

