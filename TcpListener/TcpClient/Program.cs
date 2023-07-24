using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

#pragma warning disable CS8604
string ip = Console.ReadLine();
int port = Convert.ToInt32(Console.ReadLine());
try
{
    while (true)
    {
        TcpClient client = new TcpClient();
        client.Connect(IPAddress.Parse(ip), port);
        Console.WriteLine($"Connected to 127.0.0.1:8080");
        string? message = Console.ReadLine();
        byte[] data = Encoding.ASCII.GetBytes(message);

        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Server Response: {response}");
        client.Close();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
#pragma warning restore CS8604