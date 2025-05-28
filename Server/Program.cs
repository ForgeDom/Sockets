using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Project;

class Program
{
    static void Main(string[] args)
    {
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Сервер запущено. Очікування підключення...");
        while (true)
        {
            using var client = listener.AcceptTcpClient();
            var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string time = DateTime.Now.ToString("HH:mm");
            Console.WriteLine($"О {time} від {remoteEndPoint?.Address} отримано рядок: {received}");
            string response = "Привіт, клієнт!";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);
            Console.ReadKey();
        }
    }
}
