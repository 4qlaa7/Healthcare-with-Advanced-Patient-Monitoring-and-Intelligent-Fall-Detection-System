using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Media;
using System.Diagnostics;

namespace HCI_Project
{
    public partial class Form1 : Form
    {

        public static Client_Connection Conn;
        Form activeform;
        Timer t = new Timer();
        Microphone Mic = new Microphone();
        bool micisopened=false;
        private SoundPlayer clicksound;
        private SoundPlayer switchmic;
        int savevalue = 0;
        Process pp;
        public Form1(Client_Connection con)
        {
            Conn = con;
            //this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.Load += Form1_Load;
            t.Tick += T_Tick;
            t.Interval = 500;
            openchild(new HomePage());            
            clicksound = new SoundPlayer("clicking.wav"); 
            switchmic = new SoundPlayer("openmic.wav");
            //Conn.connectToSocket();
            this.FormClosed += Form1_FormClosed;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            pp.Kill();
            Conn.closeConnection();
            
        }

        private void T_Tick(object sender, EventArgs e)
        {
            HomeBtn.BackColor = Color.White;
            RoomsBtn.BackColor = Color.White;
            Guide.BackColor = Color.White;
            SOSBtn.BackColor = Color.White;
            HistoryBtn.BackColor = Color.White;
            if (savevalue!= Mic.value)
            {
                changeForm();
                savevalue = Mic.value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Start();
            
            pp = Process.Start("reacTIVision.exe");
        }

        private void openchild(Form childform)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childform);
            this.panel2.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }

        public void changeForm()
        {
            clicksound.Play();
            switch (Mic.value)
            {
                case 0:                    
                    HomeBtn.PerformClick();
                    break;
                case 1:
                    RoomsBtn.PerformClick();
                    break;
                case 2:
                    SOSBtn.PerformClick();
                    break;
                case 3:
                    HistoryBtn.PerformClick();
                    break;
                case 4:
                    Guide.PerformClick();
                    break;
                case 5:
                    Application.Exit();
                    break;
            }
        }

       
        public void changebtnscolor(Button B)
        {
            B.BackColor = Color.MediumSeaGreen;
        }
        
        
        private void HomeBtn_Click(object sender, EventArgs e)
        {
            clicksound.Play();
            changebtnscolor(HomeBtn);
            openchild(new HomePage());
        }
        private void RoomsBtn_Click(object sender, EventArgs e)
        {
            changebtnscolor(RoomsBtn);
            clicksound.Play();
            openchild(new Rooms()); 
        }

        private void SOSBtn_Click(object sender, EventArgs e)
        {
            changebtnscolor(SOSBtn);
            clicksound.Play();
            openchild(new SOS());
        }

        private void HistoryBtn_Click(object sender, EventArgs e)
        {
            changebtnscolor(HistoryBtn);
            clicksound.Play();
            openchild(new History());
        }

        private void Guide_Click(object sender, EventArgs e)
        {
            changebtnscolor(Guide);
            clicksound.Play();
            openchild(new Guide());
        }

        private void MicBtn_Click(object sender, EventArgs e)
        {
            switchmic.Play();
            switch (micisopened)
            {
                case false:
                    Mic.StartListening();
                    MicBtn.BackColor = Color.LightGreen;
                    micisopened = true;
                    break;
                case true:
                    Mic.StopListening();
                    MicBtn.BackColor = Color.Red;
                    micisopened = false;
                    break;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
