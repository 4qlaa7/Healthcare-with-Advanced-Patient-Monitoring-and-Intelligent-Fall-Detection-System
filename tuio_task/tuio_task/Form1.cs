using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TUIO;

namespace tuio_task
{
    public partial class Form1 : Form, TuioListener
	{
		Form activeform;
		int i = 0;
		private TuioClient client;
		private Dictionary<long, TuioObject> objectList;
		private Dictionary<long, TuioCursor> cursorList;
		private Dictionary<long, TuioBlob> blobList;
		private bool verbose;
		public static bool isok = false;
		Font font = new Font("Arial", 10.0f);
		SolidBrush fntBrush = new SolidBrush(Color.White);
		SolidBrush bgrBrush = new SolidBrush(Color.FromArgb(0, 0, 64));
		SolidBrush curBrush = new SolidBrush(Color.FromArgb(192, 0, 192));
		SolidBrush objBrush = new SolidBrush(Color.FromArgb(64, 0, 0));
		SolidBrush blbBrush = new SolidBrush(Color.FromArgb(64, 64, 64));
		Pen curPen = new Pen(new SolidBrush(Color.Blue), 1);
		public int ctor = 0;
		Bitmap off;
		bool ishere = false;
		public static string text = "unrecognized";

		public Form1()
        {
			InitializeComponent();
			this.Paint += Tuioform_Paint;
			this.Load += Tuioform_Load;
			verbose = false;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
							ControlStyles.UserPaint |
							ControlStyles.DoubleBuffer, true);
			objectList = new Dictionary<long, TuioObject>(128);
			cursorList = new Dictionary<long, TuioCursor>(128);
			blobList = new Dictionary<long, TuioBlob>(128);
			client = new TuioClient(3333);
			client.addTuioListener(this);
			client.connect();
			isok = true;
		}
		private void Tuioform_Load(object sender, EventArgs e)
		{
			off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
		}

		private void Tuioform_Paint(object sender, PaintEventArgs e)
		{
			DrawDubb(e.Graphics);
		}
		void DrawScene(Graphics g2)
		{
			g2.Clear(Color.White);
			SolidBrush b = new SolidBrush(Color.Yellow);
			g2.FillEllipse(b, this.ClientSize.Width / 2, this.ClientSize.Height / 2, ctor, ctor);
			if (!ishere)
			{
				g2.FillEllipse(b, this.ClientSize.Width / 2 - 100, this.ClientSize.Height / 2, 100, 100);
			}
		}
		void DrawDubb(Graphics g)
		{
			Graphics g2 = Graphics.FromImage(off);
			DrawScene(g2);
			g.DrawImage(off, 0, 0);
		}
		public void addTuioObject(TuioObject o)
		{
			lock (objectList)
			{
				objectList.Add(o.SessionID, o);
			}
			if (verbose) Console.WriteLine("add obj " + o.SymbolID + " (" + o.SessionID + ") " + o.X + " " + o.Y + " " + o.Angle);
		}

		public void updateTuioObject(TuioObject o)
		{

			if (verbose) Console.WriteLine("set obj " + o.SymbolID + " " + o.SessionID + " " + o.X + " " + o.Y + " " + o.Angle + " " + o.MotionSpeed + " " + o.RotationSpeed + " " + o.MotionAccel + " " + o.RotationAccel);
		}

		public void removeTuioObject(TuioObject o)
		{
			lock (objectList)
			{
				objectList.Remove(o.SessionID);
			}
			if (verbose) Console.WriteLine("del obj " + o.SymbolID + " (" + o.SessionID + ")");
		}

		public void addTuioCursor(TuioCursor c)
		{
			lock (cursorList)
			{
				cursorList.Add(c.SessionID, c);
			}
			if (verbose) Console.WriteLine("add cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y);
		}

		public void updateTuioCursor(TuioCursor c)
		{
			if (verbose) Console.WriteLine("set cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y + " " + c.MotionSpeed + " " + c.MotionAccel);
		}

		public void removeTuioCursor(TuioCursor c)
		{
			lock (cursorList)
			{
				cursorList.Remove(c.SessionID);
			}
			if (verbose) Console.WriteLine("del cur " + c.CursorID + " (" + c.SessionID + ")");
		}

		public void addTuioBlob(TuioBlob b)
		{
			lock (blobList)
			{
				blobList.Add(b.SessionID, b);
			}
			if (verbose) Console.WriteLine("add blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area);
		}

		public void updateTuioBlob(TuioBlob b)
		{

			if (verbose) Console.WriteLine("set blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area + " " + b.MotionSpeed + " " + b.RotationSpeed + " " + b.MotionAccel + " " + b.RotationAccel);
		}

		public void removeTuioBlob(TuioBlob b)
		{
			lock (blobList)
			{
				blobList.Remove(b.SessionID);
			}
			if (verbose) Console.WriteLine("del blb " + b.BlobID + " (" + b.SessionID + ")");
		}

		public void refresh(TuioTime frameTime)
		{
			Invalidate();
		}
		Form[] forms = { new Form2(), new Form3(), new Form4(), new Form5() };
		public void isrotate(float angel)
		{
			Console.WriteLine(angel);
			if (angel < 1 || angel >= 3 && angel < 4)
			{
				Console.WriteLine("Normal:" + angel);
			}
			if (angel >= 4)
			{
				Console.WriteLine("left:");
				if (ctor > 100)
				{
					if(i!=0)
                    {
						i--;
                    }
					openchild(forms[i]);
					Console.WriteLine("left:" + angel + ":" + ctor);
					ctor = 0;
				}
				ctor++;
			}
			if (angel >= 1 && angel < 3)
			{
				Console.WriteLine("Right:" + angel);
				if (ctor > 100)
				{
					if (i != 3)
					{
						i++;
					}
					openchild(forms[i]);
					Console.WriteLine("Right:" + angel + ":" + ctor);
					ctor = 0;
				}
				ctor++;
			}
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
			this.panel1.Controls.Add(childform);
			this.panel1.Tag = childform;
			childform.BringToFront();
			childform.Show();
			dothis();
		}
		void dothis()
        {
			forms[0] = new Form2();
			forms[1] = new Form3();
			forms[2] = new Form4();
			forms[3] = new Form5();
		}
		
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			ishere = false;
			if (objectList.Count > 0)
			{
				lock (objectList)
				{
					foreach (TuioObject tobj in objectList.Values)
					{
						//Console.WriteLine("mewooo " + tobj.SymbolID);
						if (tobj.SymbolID == 11)
						{
							Console.WriteLine("save");
							ishere = true;

						}
						isrotate(tobj.Angle);
						DrawDubb(this.CreateGraphics());
					}

				}
			}
			DrawDubb(this.CreateGraphics());
		}
	}
}

