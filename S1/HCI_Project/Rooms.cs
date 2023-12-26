using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace HCI_Project
{
    public partial class Rooms : Form
    {
        Timer t = new Timer();
        Form f = new Form2();
        public Rooms()
        {
            InitializeComponent();
            t.Tick += T_Tick;
            t.Interval = 1;
            t.Start();
            f.Show();
            f.Size= new System.Drawing.Size(0, 0);
            


        }
        void changecamera()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("camera.xml"); // Replace with the actual XML file path

            // Select the root element
            XmlNode root = xmlDoc.DocumentElement;

            // Iterate through <camera> elements
            foreach (XmlNode cameraNode in root.SelectNodes("camera"))
            {
                string cameraId = cameraNode.Attributes["id"].Value;
                Console.WriteLine($"Camera ID: {cameraId}");
                string newCameraId = "1";
                cameraNode.Attributes["id"].Value = newCameraId;

                // Save the updated XML back to the file
                xmlDoc.Save("camera.xml"); // Save to the same file
                Console.WriteLine("Camera ID changed and XML file updated.");

            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            getdata();
        }
        void ShowImage(Image image)
        {
            pictureBox1.Image = image;
        }
        void getdata()
        {
            byte[] data = Form1.Conn.recieveframe();
            Image frame = ByteArrayToImage(data);

            if (frame != null)
            {
                ShowImage(frame);
            }
            label1.Text = Form2.text;
        }
        static Image ByteArrayToImage(byte[] byteArray)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    return Image.FromStream(ms, true, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting byte array to Image: " + ex.Message);
                return null;
            }
        }
    }
}
