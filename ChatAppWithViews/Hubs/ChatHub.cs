using Microsoft.AspNetCore.SignalR;
using SimpleChatAppRabbitMQ.Services;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly RabbitMQService _rabbitMQService;

    public ChatHub(RabbitMQService rabbitMQService)
    {
        _rabbitMQService = rabbitMQService;
    }

    public async Task SendMessage(string user, string message)
    {
        // Send the message to RabbitMQ
        _rabbitMQService.SendMessage($"{user}: {message}");

        // Broadcast the message to all clients
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}