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
		void change_angel(bool left, bool right, bool downright, bool downleft)
		{
			if (left)
			{
				if (Form1.CurrentMouseMode == Form1.Modes.MainMenu)
				{
					//openchild(forms[4]);
					//Form1.newform = new Rooms();
					//Form1.CurrentMouseMode = Form1.Modes.Rooms;
				}
				if (Form1.CurrentMouseMode == Form1.Modes.SOS || Form1.CurrentMouseMode == Form1.Modes.History || Form1.CurrentMouseMode == Form1.Modes.Patient)
				{
					Form1.CurrentMouseMode = Form1.Modes.MainMenu;

				}
			}
			if (right)
			{
				if (Form1.CurrentMouseMode == Form1.Modes.MainMenu)
				{
					//openchild(forms[3]);
					Form1.newform = new SOS();
					Form1.CurrentMouseMode = Form1.Modes.SOS;
				}

			}
			if (downleft)
			{
				if (Form1.CurrentMouseMode == Form1.Modes.MainMenu)
				{
					//Form1.openchild(new Guide());
					Form1.newform = new Guide();
					Form1.CurrentMouseMode = Form1.Modes.History;
				}

			}
			if (downright)
			{
				if (Form1.CurrentMouseMode == Form1.Modes.MainMenu)
				{

					//Form1.openchild(new History());
					Form1.newform = new History();
					Form1.CurrentMouseMode = Form1.Modes.Patient;
				}

			}


		}
		public void pat_rotate(float angel)
		{
			//Left
			if (angel >= 4.5)
			{
				Guide.prev = true;
				Guide.next = false;
				Guide.index++;

			}
			//right
			if (angel >= 1 && angel < 2)
			{
				Guide.prev = false;
				Guide.next = true;
				Guide.index++;
			}
			//normal
			if (angel < 1)
			{
				Console.WriteLine("normal" + angel);
				Guide.prev = false;
				Guide.next = false;
				Guide.index = 0;
			}
		}
		public void isrotate(float angel)
		{
			//Console.WriteLine(angel);
			//normal angel
			if (angel < 1)
			{
				Form1.ctor = 0;
				Form1.left = false;
				Form1.right = false;
				Form1.downright = false;
				Form1.downleft = false;
			}

			//left angel
			if (angel >= 4.5)
			{
				Form1.left = true;
				Form1.right = false;
				Form1.downleft = false;
				Form1.downright = false;
				if (Form1.ctor > 40)
				{
					change_angel(Form1.left, Form1.right, Form1.downright, Form1.downleft);
					Form1.ctor = 0;
				}
				Form1.ctor++;
			}
			//down left angel
			if (angel >= 3 && angel < 4)
			{
				Form1.left = false;
				Form1.right = false;
				Form1.downleft = true;
				Form1.downright = false;
				if (Form1.ctor > 40)
				{
					change_angel(Form1.left, Form1.right, Form1.downright, Form1.downleft);
					Form1.ctor = 0;
				}
				Form1.ctor++;
			}

			//right angel
			if (angel >= 1 && angel < 2)
			{
				Form1.right = true;
				Form1.left = false;
				Form1.downleft = false;
				Form1.downright = false;
				if (Form1.ctor > 40)
				{
					change_angel(Form1.left, Form1.right, Form1.downright, Form1.downleft);
					Form1.ctor = 0;
				}
				Form1.ctor++;
			}
			//down right angel
			if (angel >= 2 && angel < 3)
			{

				Form1.right = false;
				Form1.left = false;
				Form1.downleft = false;
				Form1.downright = true;
				if (Form1.ctor > 40)
				{
					change_angel(Form1.left, Form1.right, Form1.downright, Form1.downleft);
					Form1.ctor = 0;
				}
				Form1.ctor++;
			}

		}
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			bool isok53 = false;
			bool isok52 = false;
			int allmed = 0;
			List<string> med_id_mess = new List<string>();
			List<string> Ll = new List<string>();
			TextFileHandler pnn = new TextFileHandler("medicen.txt");
			Ll = pnn.ReadFile();
			if (objectList.Count > 0)
			{
				lock (objectList)
				{
					foreach (TuioObject tobj in objectList.Values)
					{
						if (tobj.SymbolID != 53 && tobj.SymbolID != 52)
						{
							for (int i = 0; i < Ll.Count; i++)
							{
								string[] parts = Ll[i].Split(',');
								if (int.Parse(parts[2]) == tobj.SymbolID)
								{
									allmed++;
								}
								else
								{
									med_id_mess.Add(parts[0]);
								}
							}
							//Console.WriteLine(allmed+"l "+ Ll.Count);
							if (allmed != Ll.Count)
							{
								HomePage.mess = "Denger";
								HomePage.code_mess = "";
								for (int i = 0; i < med_id_mess.Count; i++)
								{
									HomePage.code_mess += "med Missing:" + med_id_mess[i] + "\n";
								}
								HomePage.img_path = "RedWarning.png";
							}
							else
							{
								HomePage.mess = "ALL Clear";
								HomePage.code_mess = "No Med Missing";
								HomePage.img_path = "GreenSafe.png";
							}
							isrotate(tobj.Angle);
						}
						else
						{
							if (tobj.SymbolID == 53)
							{
								pat_rotate(tobj.Angle);
								isok53 = true;
							}
							if (tobj.SymbolID == 52)
							{
								isok52 = true;
								if (isok52 && isok53)
								{
									Console.WriteLine("both");
									Guide.P_enter = true;
								}
							}

						}

					}
				}
			}
			else
			{
				HomePage.mess = "Denger";
				HomePage.code_mess = "All med are missing";
				//
				HomePage.img_path = "RedWarning.png";
			}
		}
	}
}
