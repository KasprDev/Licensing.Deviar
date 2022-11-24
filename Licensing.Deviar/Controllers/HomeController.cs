using Licensing.Deviar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Licensing.Deviar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    }
}