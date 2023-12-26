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

        private void T_Tick(object sender, EventArgs e)
        {
            //alert();
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
