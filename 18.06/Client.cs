using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _18._06
{
    public class Client
    {
        static async Task Main(string[] args)
        {
            string serverIp = "127.0.0.1";
            int port = 8080;

            using TcpClient client = new TcpClient();
            await client.ConnectAsync(serverIp, port);
            NetworkStream stream = client.GetStream();

            string message = "Hello, server!";
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(buffer, 0, buffer.Length);

            buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"At {DateTime.Now:HH:mm} from {serverIp} received the line: {responseMessage}");
        }
    }
}
