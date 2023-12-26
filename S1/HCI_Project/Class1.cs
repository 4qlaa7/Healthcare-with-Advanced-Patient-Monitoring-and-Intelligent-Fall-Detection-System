using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project
{
    class Class1
    {
//        using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Net.Sockets;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

//namespace LatOne
//    {
//        public partial class Form1 : Form
//        {

//            TcpClient client = new TcpClient("127.0.0.1", 5555);
//            Timer timer = new Timer();

//            public Form1()
//            {
//                InitializeComponent();
//                timer.Tick += Timer_Tick;
//                timer.Interval = 1; // Set the timer interval in milliseconds (adjust as needed)
//                timer.Start();
//            }

//            private void Timer_Tick(object sender, EventArgs e)
//            {
//                try
//                {
//                    NetworkStream stream = client.GetStream();

//                    // Read the frame size (assuming it is an Int64)
//                    byte[] sizeBytes = new byte[8];
//                    int bytesRead = stream.Read(sizeBytes, 0, sizeBytes.Length);

//                    if (bytesRead < 8)
//                    {
//                        // Incomplete frame size, wait for more data
//                        return;
//                    }

//                    long size = BitConverter.ToInt64(sizeBytes, 0);

//                    // Read the frame data in chunks
//                    byte[] data = new byte[size];
//                    int totalBytesRead = 0;

//                    while (totalBytesRead < size)
//                    {
//                        int bytesReadThisTime = stream.Read(data, totalBytesRead, (int)size - totalBytesRead);

//                        if (bytesReadThisTime == 0)
//                        {
//                            // End of stream
//                            break;
//                        }

//                        totalBytesRead += bytesReadThisTime;
//                    }

//                    Image frame = ByteArrayToImage(data);

//                    if (frame != null)
//                    {
//                        ShowImage(frame);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Error in Timer_Tick: " + ex.Message);
//                }
//            }
//            static Image ByteArrayToImage(byte[] byteArray)
//            {
//                try
//                {
//                    using (MemoryStream ms = new MemoryStream(byteArray))
//                    {
//                        return Image.FromStream(ms, true, true);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Error converting byte array to Image: " + ex.Message);
//                    return null;
//                }
//            }
//            private void Form1_Load(object sender, EventArgs e)
//            {
//                TcpClient client = new TcpClient("127.0.0.1", 5555);

//            }

//            // Display the image in a simple form
//            void ShowImage(Image image)
//            {
//                pictureBox1.Image = image;
//            }


//        }
//    }
}
}
