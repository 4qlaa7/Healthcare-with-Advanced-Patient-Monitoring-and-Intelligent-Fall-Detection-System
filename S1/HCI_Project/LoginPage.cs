using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI_Project
{
    public partial class LoginPage : Form
    {
        public static Client_Connection Conn = new Client_Connection();
        bool facedone = false;
        Timer timer = new Timer();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            Conn.connectToSocket();
            timer.Tick += Timer_Tick;
            timer.Start();

            string face = ",FACEID";
            Conn.send(face);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (facedone == false && Conn.FaceID())
            {
                this.Text = "True";
                facedone = true;
                Form1 form = new Form1(Conn);
                this.Hide();
                form.ShowDialog();
                
                
            }
        }
    }
}
