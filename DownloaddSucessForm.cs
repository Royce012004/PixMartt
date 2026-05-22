using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PixMartt
{
    public partial class DownloadSuccessForm : Form
    {
        int currentUserID;
        Form previousForm;

        public DownloadSuccessForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;

            DesignDownloadSuccessForm();
        }

        public DownloadSuccessForm(int userID, Form ownerForm)
        {
            InitializeComponent();
            currentUserID = userID;
            previousForm = ownerForm;

            DesignDownloadSuccessForm();
        }

        private void DesignDownloadSuccessForm()
        {
            this.Text = "Download Complete";
            this.Size = new Size(430, 330);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            this.ShowInTaskbar = false;
            this.AutoScaleMode = AutoScaleMode.None;

            this.Controls.Clear();

            // SHADOW
            Panel shadowPanel = new Panel();
            shadowPanel.Size = new Size(365, 270);
            shadowPanel.Location = new Point(38, 38);
            shadowPanel.BackColor = Color.FromArgb(170, 170, 170);
            shadowPanel.Paint += ShadowPanelPaint;
            this.Controls.Add(shadowPanel);

            // POPUP CARD
            Panel popupCard = new Panel();
            popupCard.Size = new Size(365, 270);
            popupCard.Location = new Point(25, 25);
            popupCard.BackColor = Color.White;
            popupCard.Paint += PopupCardPaint;
            this.Controls.Add(popupCard);
            popupCard.BringToFront();

            // TITLE
            Label lblTitle = new Label();
            lblTitle.Text = "DOWNLOAD COMPLETE";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblTitle.Size = new Size(330, 45);
            lblTitle.Location = new Point(25, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            popupCard.Controls.Add(lblTitle);

            // LINE
            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(80, 80, 80);
            divider.Size = new Size(335, 1);
            divider.Location = new Point(15, 82);
            popupCard.Controls.Add(divider);

            // MESSAGE
            Label lblMessage = new Label();
            lblMessage.Text = "ARTWORK\nDOWNLOADED!";
            lblMessage.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMessage.ForeColor = Color.FromArgb(45, 45, 45);
            lblMessage.Size = new Size(300, 70);
            lblMessage.Location = new Point(33, 115);
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            popupCard.Controls.Add(lblMessage);

            // OK BUTTON SHADOW
            Panel okShadow = new Panel();
            okShadow.Size = new Size(150, 42);
            okShadow.Location = new Point(112, 208);
            okShadow.BackColor = Color.FromArgb(170, 170, 170);
            okShadow.Paint += ButtonShadowPaint;
            popupCard.Controls.Add(okShadow);

            // OK BUTTON
            btnBack = new Button();
            btnBack.Text = "OK !";
            btnBack.Size = new Size(150, 42);
            btnBack.Location = new Point(107, 202);
            btnBack.BackColor = Color.FromArgb(45, 50, 50);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBack.Cursor = Cursors.Hand;
            btnBack.Paint += RoundedButtonPaint;
            btnBack.Click += btnBack_Click;
            popupCard.Controls.Add(btnBack);
            btnBack.BringToFront();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DownloadedGalleryForm gallery = new DownloadedGalleryForm(currentUserID);
            gallery.Show();

            if (previousForm != null)
            {
                previousForm.Hide();
            }

            this.Close();
        }

        private void ShadowPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void PopupCardPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void ButtonShadowPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                panel.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void RoundedButtonPaint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 14))
            {
                button.Region = new Region(path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);

            path.CloseFigure();
            return path;
        }

        // Keep these to avoid old Designer event errors
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DownloadSuccessForm_Load(object sender, EventArgs e)
        {

        }
    }
}