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
        Form1 formm;
        bool facedone = false;
        Timer timer = new Timer();
        public LoginPage()
        {
            InitializeComponent();
            this.FormClosed += LoginPage_FormClosed;
        }

        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            formm.Close();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            
            Conn.connectToSocket();
            timer.Tick += Timer_Tick;
            timer.Start();

            string face = "0,FACEID";
            Conn.send(face);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
            if (facedone)
            {
                string face = "ayklam,GAZE";
                //Conn.send(face);
                //Conn.recieveMessage();
            }
            

            if (!facedone&&Conn.FaceID())
            {
                
                facedone = true;
                formm = new Form1(Conn);
                this.Hide();
                if (Conn.whoperson() == "AHMED")
                {
                    this.Text = "AYMAN";
                    formm.ShowDialog();

                }
                
                
            }
        }
    }
}
