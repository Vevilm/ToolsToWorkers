using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Models.Repositories;

namespace ToolsToWorkers.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserRepository _userRepository { get; }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index()
        {
            
        }
    }
}
