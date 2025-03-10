// using Microsoft.AspNetCore.Mvc;
// using SimpleChatAppRabbitMQ.Models;
// using SimpleChatAppRabbitMQ.Services;
// using System.Collections.Generic;

// namespace SimpleChatAppRabbitMQ.Controllers
// {
//     public class ChatController : Controller
//     {
//         private static List<Message> Messages = new();
//         private readonly RabbitMQService _rabbitMQService;

//         public ChatController()
//         {
//             _rabbitMQService = new RabbitMQService();
//             _rabbitMQService.ReceiveMessages(OnMessageReceived);
//         }

//         private void OnMessageReceived(string message)
//         {
//             Messages.Add(new Message { Sender = "User", Content = message });
//         }

        

//         public IActionResult Index()
//         {
//             return View(Messages);
//         }

//         [HttpPost]
//         public IActionResult SendMessage(string sender, string content)
//         {
//             var message = new Message { Sender = sender, Content = content };
//             Messages.Add(message);

//             _rabbitMQService.SendMessage(content);
//             return RedirectToAction("Index");
//         }
//     }
// }