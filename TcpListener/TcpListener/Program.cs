using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



TcpListener listener = new(IPAddress.Parse("127.0.0.1"), 8080);
listener.Start();
Console.WriteLine($"TCP Listener started on 127.0.0.1:8080");
TcpClient client;
Task receiveTask;
Task sendTask;
try
{
    while (true)
    {
        client = await listener.AcceptTcpClientAsync();
        Console.WriteLine($"New client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}");
        receiveTask = Task.Run(async () =>
        {
            try
            {
                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received from {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}: {data}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        });

        sendTask = Task.Run(async () =>
        {
            try
            {
                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    string responseMessage = Console.ReadLine();
                    byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        });
        await HandleClientAsync(client);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

async Task HandleClientAsync(TcpClient client)
{
    try
    {
        while (true)
        {
            await Task.WhenAny(receiveTask, sendTask);
            client.Close();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    finally
    {
        client.Close();
    }
}
