using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace SimpleChatAppRabbitMQ.Services
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory _factory;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly string _queueName = "chat_queue";
        private readonly string _hostName = "localhost";

        public RabbitMQService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _factory = new ConnectionFactory() { HostName = _hostName };
        }

        public async Task SendMessage(string message)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
