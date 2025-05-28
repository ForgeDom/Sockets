using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Project;

class Program
{
    static void Main(string[] args)
    {
        var listener = new TcpListener(IPAddress.Any, 5001);
        listener.Start();
        Console.WriteLine("Сервер запущено. Очікування підключення...");
        while (true)
        {
            using var client = listener.AcceptTcpClient();
            var stream = client.GetStream();
            byte[] buffer = new byte[100];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim().ToLower();
            string response = request switch
            {
                "date" => DateTime.Now.ToString("yyyy-MM-dd"),
                "time" => DateTime.Now.ToString("HH:mm:ss"),
                _ => "Невідомий запит"
            };
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);
        }
    }
}

