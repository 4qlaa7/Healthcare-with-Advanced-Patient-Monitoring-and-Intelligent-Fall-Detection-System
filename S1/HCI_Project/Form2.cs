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

namespace HCI_Project
{
    public partial class Form2 : Form, TuioListener
    {
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
		public static string text = "unrecognized";
		public Form2()
        {
            InitializeComponent();
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
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			
			if (objectList.Count > 0)
			{
				lock (objectList)
				{
					foreach (TuioObject tobj in objectList.Values)
					{						
						if (tobj.SymbolID == 15)
						{
							text = "you are in bedroom1";
						}
						if (tobj.SymbolID == 12)
						{
							text = "you are in bathroom";
						}
						if (tobj.SymbolID == 13)
						{
							text = "you are in Kitchen";
						}
						if (tobj.SymbolID == 14)
						{
							text = "you are in LivingRoom";
						}
						Console.WriteLine("mewooo "+ tobj.SymbolID);

					}
				}
			}			
		}
	}
}
