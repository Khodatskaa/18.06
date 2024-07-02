using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _18._06
{
    public class Server
    {
        static async Task Main(string[] args)
        {
            int port = 8080;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();

            Console.WriteLine($"Server started at {DateTime.Now:HH:mm} on {ipAddress}:{port}");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"At {DateTime.Now:HH:mm} from {((IPEndPoint)client.Client.RemoteEndPoint).Address} received the line: {message}");

            string responseMessage = "Hello, client!";
            byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
            await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);

            client.Close();
        }
    }
}
