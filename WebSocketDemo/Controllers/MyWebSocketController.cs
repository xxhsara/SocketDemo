using Microsoft.AspNetCore.Mvc;

namespace WebSocketDemo.Controllers
{
    public class MyWebSocketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
