using Microsoft.AspNetCore.Mvc;
using SimpleChatAppRabbitMQ.Models;
using SimpleChatAppRabbitMQ.Services;

namespace SimpleChatAppRabbitMQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseService _databaseService;

        public HomeController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string sender, string content)
        {
            _databaseService.SaveMessage(sender, content);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _databaseService.GetMessages();
            return Json(messages);
        }
    }
}
