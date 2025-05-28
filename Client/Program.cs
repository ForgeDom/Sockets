using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Sockets
{
    class Program
    {
        static void Main(string[] args)
        {
            using var client = new TcpClient();
            client.Connect("127.0.0.1", 5000);
            var stream = client.GetStream();
            string message = "Привіт, сервер!";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            string time = DateTime.Now.ToString("HH:mm");
            Console.WriteLine($"О {time} від {remoteEndPoint?.Address} отримано рядок: {response}");
            Console.ReadKey();
        }
    }
}

