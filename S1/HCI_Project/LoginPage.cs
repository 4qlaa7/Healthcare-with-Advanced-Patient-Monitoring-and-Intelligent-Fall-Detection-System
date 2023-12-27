using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI_Project
{
    public partial class LoginPage : Form
    {
        public static Client_Connection Conn = new Client_Connection();
        private int rotationAngle = 0;
        Form1 formm;
        bool facedone = false;
        Timer timer = new Timer();
        public LoginPage()
        {
            InitializeComponent();
            this.FormClosed += LoginPage_FormClosed;
            Size = new Size(500, 500);
            DoubleBuffered = true;
        }

        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            formm.Close();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            
            Conn.connectToSocket();
            string face = "kkk,FACEID";
            Conn.send(face);
            timer.Tick += Timer_Tick;
            timer.Interval = 50;
            timer.Start();
            
            

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DrawLoadingAnimation(this.CreateGraphics());
            rotationAngle += 10; // Adjust the rotation speed as needed
            Invalidate();          
            
            if (facedone)
            {
                string face = "kkk,GAZE";
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
                
                timer.Stop();
            }
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawLoadingAnimation(e.Graphics);
        }

        private void DrawLoadingAnimation(Graphics g)
        {
            int centerX = ClientSize.Width / 2;
            int centerY = ClientSize.Height / 2;
            int radius = 50;
            int numArcs = 8;

            for (int i = 0; i < numArcs; i++)
            {
                double startAngle = i * 360.0 / numArcs + rotationAngle;
                double endAngle = startAngle + 45; // Adjust the arc length as needed

                int x = (int)(centerX + radius * Math.Cos(Math.PI / 180 * startAngle));
                int y = (int)(centerY + radius * Math.Sin(Math.PI / 180 * startAngle));

                Rectangle arcBounds = new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius);

                g.DrawArc(Pens.Blue, arcBounds, (float)startAngle, (float)(endAngle - startAngle));
            }

            // Draw loading text
            string loadingText = "Loading While Detecting Face !";
            Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            SizeF textSize = g.MeasureString(loadingText, font);
            int textX = centerX - (int)(textSize.Width / 2);
            int textY = centerY + radius + 60; // Adjust the spacing as needed
            g.DrawString(loadingText, font, Brushes.Black, textX, textY);
        }

    }
}
