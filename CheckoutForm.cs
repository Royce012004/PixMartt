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
    public partial class CheckoutForm : Form
    {
        int currentUserID;
        int selectedArtworkID;
        public CheckoutForm(int userID, int artworkID)
        {
            InitializeComponent();
            currentUserID = userID;
            selectedArtworkID = artworkID;
        }

        private void CheckoutForm_Load(object sender, EventArgs e)
        {
            var artwork = DataStore.Artworks.FirstOrDefault(a => a.ArtworkID == selectedArtworkID);

            if (artwork != null)
            {
                lblTitle.Text = artwork.Title;
                lblPostedBy.Text = "Posted By: " + artwork.PostedBy;
                lblCategory.Text = "Category: " + artwork.Category;
                lblDescription.Text = artwork.Description;
                lblPrice.Text = "Price: ₱" + artwork.Price.ToString("0.00");

                pictureBoxArtwork.ImageLocation = artwork.ImagePath;
                pictureBoxArtwork.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnConfirmDownload_Click(object sender, EventArgs e)
        {
            Download download = new Download
            {
                DownloadID = DataStore.NextDownloadID++,
                UserID = currentUserID,
                ArtworkID = selectedArtworkID
            };

            DataStore.Downloads.Add(download);

            DownloadSuccessForm success = new DownloadSuccessForm(currentUserID);
            success.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ArtworkDetailsForm details = new ArtworkDetailsForm(currentUserID, selectedArtworkID);
            details.Show();
            this.Hide();
        }
    }
}
