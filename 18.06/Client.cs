using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _18._06
{
    public class Client
    {
        static void Main(string[] args)
        {
            string serverIp = "127.0.0.1";
            int port = 8080;

            TcpClient client = new TcpClient(serverIp, port);
            NetworkStream stream = client.GetStream();

            string message = "Hello, server!";
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);

            buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"At {DateTime.Now.ToString("HH:mm")} from {serverIp} received the line: {responseMessage}");

            client.Close();
        }
    }
}
