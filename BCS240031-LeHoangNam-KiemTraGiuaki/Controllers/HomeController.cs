using BCS240031_LeHoangNam_KiemTraGiuaki.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Rooms");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
