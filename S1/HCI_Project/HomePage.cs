using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI_Project
{
    public partial class HomePage : Form
    {

        Timer t = new Timer();
        public static string mess = "All Clear";
        public static string code_mess = "Code";
        public static string img_path = "GreenSafe.png";
        public HomePage()
        {
            InitializeComponent();
            t.Tick += T_Tick;
            this.Load += HomePage_Load;
            t.Start();
            this.KeyDown += HomePage_KeyDown;
        }

        private void HomePage_KeyDown(object sender, KeyEventArgs e)
        {
            //alert();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            //alert();
        }
        string img = "GreenSafe.png";
        private void T_Tick(object sender, EventArgs e)
        {
            this.label1.Text = mess;
            this.label2.Text = code_mess;
            if (img != img_path)
            {
                this.Sign.BackgroundImage = new Bitmap(img_path);
                img = img_path;
            }

        }

        void alert()
        {

            Form1.Conn.recieveMessage();
            label1.Text = Form1.Conn.data[0];
            //axWindowsMediaPlayer1.URL = "1.mp4";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HomePage_Load_1(object sender, EventArgs e)
        {

        }
    }
}
