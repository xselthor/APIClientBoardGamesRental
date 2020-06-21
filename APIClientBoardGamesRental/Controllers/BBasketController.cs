using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIClientBoardGamesRental.Models;
using APIClientBoardGamesRental.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIClientBoardGamesRental.Controllers
{
    public class BBasketController : Controller
    {   
        private IAPIService _service;
        
        public BBasketController(IAPIService service)
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

        public async Task<ActionResult> DeleteFromBasket(string id, string gameid)
        {
            BBasket bBasket = new BBasket();

            bBasket.username = User.Identity.Name;
            bBasket.gameid = gameid;
            bBasket.unitid = id;
            bBasket.DateCreated = DateTime.Now.ToString();

            var jdata = JsonConvert.SerializeObject(bBasket);
            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");

            Console.WriteLine("---- PutAsync ------"); 
             var httpResponse = await _service.Client.DeleteAsync($"/api/BBasket/{id}");
            Console.WriteLine("---- PutAsync END------");
            Console.WriteLine(httpResponse);
            
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
