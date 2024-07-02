using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _18._06
{
    public class Server
    {
        static void Main(string[] args)
        {
            int port = 8080;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();

            Console.WriteLine($"Server started at {DateTime.Now.ToString("HH:mm")} on {ipAddress}:{port}");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).ToLower();

                string responseMessage;
                if (request == "date")
                {
                    responseMessage = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if (request == "time")
                {
                    responseMessage = DateTime.Now.ToString("HH:mm:ss");
                }
                else
                {
                    responseMessage = "Invalid request";
                }

                byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
                stream.Write(responseBuffer, 0, responseBuffer.Length);
               
                client.Close();
            }
        }
    }
}
