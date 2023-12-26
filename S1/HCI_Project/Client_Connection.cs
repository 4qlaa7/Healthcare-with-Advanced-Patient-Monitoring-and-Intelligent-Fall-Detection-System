using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.IO;
using System.Linq;

namespace HCI_Project
{
    public  class Client_Connection
    {        
        NetworkStream stream;        
        public List<string> data;
        TcpClient tcpClient;
        String host;
        int portNumber;
        public string personwho;

        public Client_Connection(String Host= "localhost", int PortNum= 4344)
        {
            host = Host;
            portNumber= PortNum;
            data = new List<string>();

        }
        
        public bool connectToSocket()
        {
            try
            {
                tcpClient = new TcpClient(host, portNumber);
                stream = tcpClient.GetStream();
                Console.WriteLine("Connection Made ! with " + host);
                return true;
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("Connection Failed: " + e);
                return false;
            }
        }
        public byte[] recieveframe()
        {
            byte[] sizeBytes = new byte[8];
            int bytesRead = stream.Read(sizeBytes, 0, sizeBytes.Length);
            if (bytesRead < 8)
            {
                // Incomplete frame size, wait for more data
                //return sizeBytes;
                
            }
            long size = BitConverter.ToInt64(sizeBytes, 0);
            byte[] data = new byte[size];
            int totalBytesRead = 0;
            while (totalBytesRead < size)
            {
                int bytesReadThisTime = stream.Read(data, totalBytesRead, (int)size - totalBytesRead);

                if (bytesReadThisTime == 0)
                {
                    // End of stream
                    break;
                }

                totalBytesRead += bytesReadThisTime;
            }
            return data;
            
        }
        public bool send(string msg)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                byte[] sizeBytes = BitConverter.GetBytes(data.Length);
                tcpClient.GetStream().Write(sizeBytes, 0, sizeBytes.Length);
                tcpClient.GetStream().Write(data, 0, data.Length);
                Console.WriteLine("Message sent: " + msg);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to send message: " + e);
                return false;
            }
        }
        public bool FaceID()
        {
            try
            {
                byte[] receiveBuffer = new byte[8 * 1024];
                int bytesReceived = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                string dataa = Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
                string[] points = dataa.Split(',');
                Console.WriteLine(dataa.ToString());
                Console.WriteLine(points.ToString());
                if (Array.Exists(points, element => element == "FACEID") && Array.Exists(points, element => element == "True"))
                {
                    Console.WriteLine(points[2].ToString());
                    personwho = points[2];
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection not initialized : " + e);
                return false;
            }
        }
        public string whoperson()
        {
            return personwho;
        }
        public bool recieveMessage()
        {
            try
            {               
                byte[] receiveBuffer = new byte[8*1024];
                int bytesReceived = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                string dataa = Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
                string[] points = dataa.Split(',');
                Console.WriteLine(dataa.ToString());
                for (int i = 0; i < points.Length; i += 2)
                {
                    data.Add(points[i]);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection not initialized : " + e);
                return false;
            }
        }
        public void recivevideo(string path= "received_video2.mp4")
        {

            using (FileStream receivedVideo = new FileStream(path, FileMode.Create))
            {
                byte[] videoData = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(videoData, 0, videoData.Length)) > 0)
                {
                    receivedVideo.Write(videoData, 0, bytesRead);
                }
            }

            Console.WriteLine("Data received and saved to 'received_video2.mp4'.");

        }

        public bool closeConnection()
        {
            stream.Close();
            tcpClient.Close();
            Console.WriteLine("Connection terminated ");
            return true;
        }


    }
}



