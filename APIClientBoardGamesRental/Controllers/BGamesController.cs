using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using APIClientBoardGamesRental.Models;
using APIClientBoardGamesRental.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIClientBoardGamesRental.Controllers
{
    public class BGamesController : Controller
    {
        private IAPIService _service;

        public BGamesController(IAPIService service)
        {
            _service = service;
        }
        [Authorize]
        // GET: BGamesController
        public async Task<ActionResult> Index()
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

        // GET: BGamesController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            BGames bgames = new BGames();

            BGamesAndBunit bGamesAndBunit = new BGamesAndBunit(); 

            var response = await _service.Client.GetAsync($"/api/bgames/{id}");

            if (response.IsSuccessStatusCode)
            {
                var pobraneGry = response.Content.ReadAsStringAsync().Result;
                bGamesAndBunit.BGames = JsonConvert.DeserializeObject<BGames>(pobraneGry);
            }

            return View(bGamesAndBunit);
        }

        // GET: BGamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BGamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            BGames game = new BGames();

            game.oid = collection["oid"];
            game.name = collection["name"];
            game.briefdescribe = collection["briefdescribe"];
            game.image = collection["image"];
            game.minPlayers = Convert.ToInt32(collection["minPlayers"]);
            game.maxPlayers = Convert.ToInt32(collection["maxPlayers"]);
            game.playingTime = Convert.ToInt32(collection["playingTime"]);
            game.yearPublished = Convert.ToInt32(collection["yearPublished"]);
            game.Rating = Convert.ToInt32(collection["Rating"]);
            game.type = collection["type"];
            game.itemtype = collection["itemtype"];
            game.ilosc = Convert.ToInt32(collection["ilosc"]);
            game.cena = Convert.ToInt32(collection["cena"]);
            game.describe = collection["describe"];

            var jdata = JsonConvert.SerializeObject(game);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PostAsync ------");
            var httpResponse = await _service.Client.PostAsync($"/api/BGames/", httpContent);
            Console.WriteLine("---- PostAsync END------");
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

        // GET: BGamesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            BGames bgames = new BGames();
            var response = await _service.Client.GetAsync($"/api/bgames/{id}");

            if (response.IsSuccessStatusCode)
            {
                var pobraneGry = response.Content.ReadAsStringAsync().Result;
                bgames = JsonConvert.DeserializeObject<BGames>(pobraneGry);
                Console.WriteLine(pobraneGry);
                Console.WriteLine("-------- Edit ------------");
            }

            return View(bgames);
        }

        // POST: BGamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            BGames game = new BGames();
            
            game.oid = collection["oid"];
            game.name = collection["name"];
            game.briefdescribe = collection["briefdescribe"];
            game.image = collection["image"];
            game.minPlayers = Convert.ToInt32(collection["minPlayers"]);
            game.maxPlayers = Convert.ToInt32(collection["maxPlayers"]);
            game.playingTime = Convert.ToInt32(collection["playingTime"]);
            game.yearPublished = Convert.ToInt32(collection["yearPublished"]);
            game.Rating = Convert.ToInt32(collection["Rating"]);
            game.type = collection["type"];
            game.itemtype = collection["itemtype"];
            game.ilosc = Convert.ToInt32(collection["ilosc"]);
            game.cena = Convert.ToInt32(collection["cena"]);
            game.describe = collection["describe"];

            var jdata = JsonConvert.SerializeObject(game);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PutAsync ------");
            var httpResponse = await _service.Client.PutAsync($"/api/BGames/{id}", httpContent);
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

        // GET: BGamesController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            BGames bgames = new BGames();
            var response = await _service.Client.GetAsync($"/api/bgames/{id}");

            if (response.IsSuccessStatusCode)
            {
                var pobraneGry = response.Content.ReadAsStringAsync().Result;
                bgames = JsonConvert.DeserializeObject<BGames>(pobraneGry);
            }

            return View(bgames);
        }

        // POST: BGamesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            BGames game = new BGames();

            game.oid = collection["oid"];
            game.name = collection["name"];
            game.briefdescribe = collection["briefdescribe"];
            game.image = collection["image"];
            game.minPlayers = Convert.ToInt32(collection["minPlayers"]);
            game.maxPlayers = Convert.ToInt32(collection["maxPlayers"]);
            game.playingTime = Convert.ToInt32(collection["playingTime"]);
            game.yearPublished = Convert.ToInt32(collection["yearPublished"]);
            game.Rating = Convert.ToInt32(collection["Rating"]);
            game.type = collection["type"];
            game.itemtype = collection["itemtype"];
            game.ilosc = Convert.ToInt32(collection["ilosc"]);
            game.cena = Convert.ToInt32(collection["cena"]);
            game.describe = collection["describe"];

            var jdata = JsonConvert.SerializeObject(game);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PutAsync ------");
            var httpResponse = await _service.Client.DeleteAsync($"/api/BGames/{id}");
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
