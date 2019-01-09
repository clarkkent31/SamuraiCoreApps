using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SamuraiCore.Data;
using SamuraiCore.Entity;
using SamuraiCore.Repository.Interfaces;
using SamuraiCore.WebApp.Models;

namespace SamuraiCore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private SamuraiContext _context;
        private IRepositorySamurai _repositorySamurai;
        public HomeController(SamuraiContext context, IRepositorySamurai repositorySamurai)
        {
            _context = context;
            _repositorySamurai = repositorySamurai;
        }

        public IActionResult Index()
        {
            var result = _repositorySamurai.GetById(1);
            ViewData["Message"] = result.Name;

            //_context.Samurai.Add(new Samurai
            //{
            //    Name = "Rukia"
            //});
            //_context.SaveChanges();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
