using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Що запитати у сервера? (date/time)");
        string? request = Console.ReadLine();
        if (request != "date" && request != "time")
        {
            Console.WriteLine("Некоректний запит.");
            return;
        }
        using var client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 5001);
        var stream = client.GetStream();
        byte[] data = Encoding.UTF8.GetBytes(request);
        await stream.WriteAsync(data, 0, data.Length);
        byte[] buffer = new byte[100];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Відповідь від сервера: {response}");
        Console.ReadKey();
    }
}