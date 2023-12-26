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
        public static Form newform;
        Bitmap off;
        Timer t = new Timer();
        Microphone Mic = new Microphone();
        bool micisopened=false;
        private SoundPlayer clicksound;
        private SoundPlayer switchmic;
        int savevalue = 0;
        Process pp;
        Form f = new Form2();
        public enum Modes { MainMenu, Rooms, SOS, Guide, History };        
        public static Modes CurrentMouseMode = Modes.MainMenu;
        public static bool right = false, left = false, downright = false, downleft = false;
        public static int ctor=0;
        public Form1(Client_Connection con)
        {
            Conn = con;
            //this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.Load += Form1_Load;
            this.WindowState = FormWindowState.Maximized;
            t.Tick += T_Tick;
            this.Paint += Form1_Paint;
            t.Interval = 500;
            newform = new HomePage();
            activeform = new HomePage();
            openchild(newform);            
            clicksound = new SoundPlayer("clicking.wav"); 
            switchmic = new SoundPlayer("openmic.wav");
            //Conn.connectToSocket();
            this.FormClosed += Form1_FormClosed;
            f.Show();
            f.Size = new System.Drawing.Size(0, 0);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        void DrawScene(Graphics g2)
        {
            g2.Clear(Color.White);
            SolidBrush b = new SolidBrush(Color.Yellow);
            if (CurrentMouseMode == Modes.MainMenu)
            {
                g2.DrawString("Rooms", new Font("Arial", 12), Brushes.Black, 100, 175);
                g2.DrawString("SOS", new Font("Arial", 12), Brushes.Black, 300, 175);
                g2.DrawString("Guide", new Font("Arial", 12), Brushes.Black, 100, 475);
                g2.DrawString("History", new Font("Arial", 12), Brushes.Black, 300, 475);
                if (left)
                {
                    g2.FillEllipse(Brushes.Red, 100, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 100, 200, 50, 50);
                }
                if (right)
                {
                    g2.FillEllipse(Brushes.Red, 300, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 300, 200, 50, 50);
                }
                if (downright)
                {
                    g2.FillEllipse(Brushes.Red, 300, 500, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 300, 500, 50, 50);
                }
                if (downleft)
                {
                    g2.FillEllipse(Brushes.Red, 100, 500, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 100, 500, 50, 50);
                }


            }
            if (CurrentMouseMode == Modes.SOS)
            {
                g2.DrawString("back", new Font("Arial", 12), Brushes.Black, 100, 175);
                g2.DrawString("add", new Font("Arial", 12), Brushes.Black, 300, 175);
                if (left)
                {

                    g2.FillEllipse(Brushes.Red, 100, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 100, 200, 50, 50);
                }
                if (right)
                {
                    g2.FillEllipse(Brushes.Red, 300, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 300, 200, 50, 50);
                }
            }
            if (CurrentMouseMode == Modes.Guide)
            {
                g2.DrawString("back", new Font("Arial", 12), Brushes.Black, 100, 175);
                g2.DrawString("add", new Font("Arial", 12), Brushes.Black, 300, 175);
                if (left)
                {

                    g2.FillEllipse(Brushes.Red, 100, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 100, 200, 50, 50);
                }
                if (right)
                {
                    g2.FillEllipse(Brushes.Red, 300, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 300, 200, 50, 50);
                }
            }
            if (CurrentMouseMode == Modes.History)
            {
                g2.DrawString("back", new Font("Arial", 12), Brushes.Black, 100, 175);
                g2.DrawString("add", new Font("Arial", 12), Brushes.Black, 300, 175);
                if (left)
                {

                    g2.FillEllipse(Brushes.Red, 100, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 100, 200, 50, 50);
                }
                if (right)
                {
                    g2.FillEllipse(Brushes.Red, 300, 200, 50, 50);
                }
                else
                {
                    g2.FillEllipse(b, 300, 200, 50, 50);
                }
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            pp.Kill();
            Conn.closeConnection();
            
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (newform!=activeform)
            {
                openchild(newform);                
            }
            //HomeBtn.BackColor = Color.White;
            //RoomsBtn.BackColor = Color.White;
            //Guide.BackColor = Color.White;
            //SOSBtn.BackColor = Color.White;
            //HistoryBtn.BackColor = Color.White;
            if (savevalue!= Mic.value)
            {
                changeForm();
                savevalue = Mic.value;
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Start();
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            pp = Process.Start("reacTIVision.exe");
        }

        public void openchild(Form childform)
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
                    //HomeBtn.PerformClick();
                    break;
                case 1:
                    //RoomsBtn.PerformClick();
                    break;
                case 2:
                    //SOSBtn.PerformClick();
                    break;
                case 3:
                    //HistoryBtn.PerformClick();
                    break;
                case 4:
                    //Guide.PerformClick();
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
            //changebtnscolor(HomeBtn);
            openchild(new HomePage());
        }
        private void RoomsBtn_Click(object sender, EventArgs e)
        {
            //changebtnscolor(RoomsBtn);
            clicksound.Play();
            openchild(new Rooms()); 
        }

        private void SOSBtn_Click(object sender, EventArgs e)
        {
            //changebtnscolor(SOSBtn);
            clicksound.Play();
            openchild(new SOS());
        }

        private void HistoryBtn_Click(object sender, EventArgs e)
        {
            //changebtnscolor(HistoryBtn);
            clicksound.Play();
            openchild(new History());
        }

        private void Guide_Click(object sender, EventArgs e)
        {
            //changebtnscolor(Guide);
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
                    //MicBtn.BackColor = Color.LightGreen;
                    micisopened = true;
                    break;
                case true:
                    Mic.StopListening();
                    //MicBtn.BackColor = Color.Red;
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
