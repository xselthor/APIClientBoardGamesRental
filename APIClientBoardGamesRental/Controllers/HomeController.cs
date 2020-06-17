using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIClientBoardGamesRental.Models;
using APIClientBoardGamesRental.Services;
using Newtonsoft.Json;


namespace APIClientBoardGamesRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAPIService _service;

        public HomeController(ILogger<HomeController> logger, IAPIService service)
        {
            _logger = logger;
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            List<BGames> bgames = new List<BGames>();
            var response = await _service.Client.GetAsync("/api/bgames/");

            if (response.IsSuccessStatusCode)
            {
                var pobraneGry = response.Content.ReadAsStringAsync().Result;
                bgames = JsonConvert.DeserializeObject<List<BGames>>(pobraneGry);
            }
            
            return View(bgames);

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
