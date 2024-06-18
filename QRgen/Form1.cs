using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace QRgen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Add MouseUp event handler for the PictureBox
            pictureBoxQRCode.MouseUp += pictureBoxQRCode_MouseUp;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string inputText = textBoxInput.Text;
            if (!string.IsNullOrEmpty(inputText))
            {
                GenerateQRCode(inputText);
            }
            else
            {
                MessageBox.Show("Please enter some text to generate a QR code.", "Input Needed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GenerateQRCode(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(trackBar1.Value);
                    pictureBoxQRCode.Image = qrCodeImage;
                }
            }
        }

        private void pictureBoxQRCode_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                ToolStripMenuItem copyImageItem = new ToolStripMenuItem("Copy Image");
                copyImageItem.Click += CopyImageItem_Click;
                contextMenu.Items.Add(copyImageItem);
                contextMenu.Show(pictureBoxQRCode, e.Location);
            }
        }

        private void CopyImageItem_Click(object sender, EventArgs e)
        {
            if (pictureBoxQRCode.Image != null)
            {
                Clipboard.SetImage(pictureBoxQRCode.Image);
                MessageBox.Show("QR code copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No image to copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

  
    }
}
