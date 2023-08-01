using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8604

string? ip = Console.ReadLine();
int port = Convert.ToInt32(Console.ReadLine());

TcpClient client = new();
await client.ConnectAsync(IPAddress.Parse(ip), port);
Console.WriteLine($"Connected to {ip}:{port}");

Task receiveTask = Task.Run(async () =>
{
    try
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Server Response: {response}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
});

Task sendTask = Task.Run(async () =>
{
    try
    {
        while (true)
        {
            string message = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            await stream.WriteAsync(data, 0, data.Length);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
});

await Task.WhenAny(receiveTask, sendTask);
client.Close();