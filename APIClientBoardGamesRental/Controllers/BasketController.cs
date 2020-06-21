using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIClientBoardGamesRental.Models;
using APIClientBoardGamesRental.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIClientBoardGamesRental.Controllers
{
    public class BasketController : Controller
    {   
        private IAPIService _service;
        
        public BasketController(IAPIService service)
        {
            _service = service; 
        }

        // GET: BasketController
        public async Task<IActionResult> Index()
        {
            BGamesAndBunitList basket = new BGamesAndBunitList();

            var response = await _service.Client.GetAsync($"/api/BBasket/basket/{User.Identity.Name}");
            var response2 = await _service.Client.GetAsync($"/api/BGames");


            if (response.IsSuccessStatusCode)
            {
                var dane = response.Content.ReadAsStringAsync().Result;
                basket.BBaskets = JsonConvert.DeserializeObject<List<BBasket>>(dane);
            }

            if (response2.IsSuccessStatusCode)
            {
                var dane = response2.Content.ReadAsStringAsync().Result;
                basket.BGames = JsonConvert.DeserializeObject<List<BGames>>(dane);
            }

            return View(basket);
        }
    }
}
