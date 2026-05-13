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
    public partial class DownloadSuccessForm : Form
    {
        int currentUserID;
        public DownloadSuccessForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DownloadedGalleryForm gallery = new DownloadedGalleryForm(currentUserID);
            gallery.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DownloadSuccessForm_Load(object sender, EventArgs e)
        {

        }
    }
}
