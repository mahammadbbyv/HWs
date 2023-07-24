using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

try
{
    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
    int port = 8080;
    TcpListener listener = new TcpListener(ipAddress, port);
    listener.Start();
    Console.WriteLine($"TCP Listener started on {ipAddress}:{port}");

    while (true)
    {
        TcpClient client = listener.AcceptTcpClient();
        HandleClient(client);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
static void HandleClient(TcpClient client)
{
    try
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Received: {data}");
        string responseMessage = Console.ReadLine();
        byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
        stream.Write(responseData, 0, responseData.Length);
        client.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}