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
        public static string gameid;

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
            //BGames bgames = new BGames();

            BGamesAndBunit bGamesAndBunit = new BGamesAndBunit(); 

            var response = await _service.Client.GetAsync($"/api/bgames/{id}"); 
            var response2 = await _service.Client.GetAsync($"/api/bunit/lunits/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var pobraneGry = response.Content.ReadAsStringAsync().Result;
                bGamesAndBunit.BGames = JsonConvert.DeserializeObject<BGames>(pobraneGry);
            }

            if (response2.IsSuccessStatusCode)
            {
                var pobraneegzemplarze = response2.Content.ReadAsStringAsync().Result;
                bGamesAndBunit.BUnit = JsonConvert.DeserializeObject<List<BUnit>>(pobraneegzemplarze);
            }

            gameid = bGamesAndBunit.BGames.oid;

            return View(bGamesAndBunit);
        }

        // GET: BGamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: BGamesController/Create
        public ActionResult CreateUnit()
        {
            BUnit bUnit = new BUnit();
            bUnit.gameid = gameid;
            
            return View(bUnit);
        }

        // POST: BGamesController/CreateUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUnit(IFormCollection collection)
        {
            BUnit bUnit = new BUnit();

            bUnit.gameid = collection["gameid"];
            bUnit.dateadded = DateTime.Now.ToString();
            bUnit.loaned = "false";
            bUnit.description = collection["description"];
            bUnit.barcode = collection["barcode"];

            var jdata = JsonConvert.SerializeObject(bUnit);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PostAsync ------");
            var httpResponse = await _service.Client.PostAsync($"/api/BUnit/", httpContent);
            Console.WriteLine("---- PostAsync END------");
            Console.WriteLine(httpResponse);

            try
            {
                return RedirectToAction(nameof(Details), new { id = bUnit.gameid});
            }
            catch
            {
                return View();
            }
        }

        // GET: BGamesController/Edit/5
        public async Task<ActionResult> EditUnit(string id)
        {
            BUnit bunit = new BUnit();
            var response = await _service.Client.GetAsync($"/api/bunit/{id}");

            if (response.IsSuccessStatusCode)
            {
                var dane = response.Content.ReadAsStringAsync().Result;
                bunit = JsonConvert.DeserializeObject<BUnit>(dane);
                Console.WriteLine(dane);
                Console.WriteLine("-------- Edit ------------");
            }

            return View(bunit);
        }

        // POST: BGamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUnit(string id, IFormCollection collection)
        {
            BUnit unit = new BUnit();

            unit.oid = collection["oid"];
            unit.gameid = collection["gameid"];
            unit.dateadded = collection["dateadded"];
            unit.loaned = collection["loaned"];
            unit.dateofrent = collection["dateofrent"];
            unit.barcode = collection["barcode"];
            unit.description = collection["description"];

            var jdata = JsonConvert.SerializeObject(unit);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PutAsync ------");
            var httpResponse = await _service.Client.PutAsync($"/api/BUnit/{id}", httpContent);
            Console.WriteLine("---- PutAsync END------");
            Console.WriteLine(httpResponse);

            try
            {
                return RedirectToAction(nameof(Details), new { id = unit.gameid});
            }
            catch
            {
                return View();
            }
        }

        // GET: BGamesController/DeleteUnit/5
        public async Task<ActionResult> DeleteUnit(string id)
        {
            BUnit bunit = new BUnit();
            var response = await _service.Client.GetAsync($"/api/bunit/{id}");

            if (response.IsSuccessStatusCode)
            {
                var dane = response.Content.ReadAsStringAsync().Result;
                bunit = JsonConvert.DeserializeObject<BUnit>(dane);
            }

            return View(bunit);
        }

        // POST: BGamesController/DeleteUnit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUnit(string id, IFormCollection collection)
        {
            BUnit unit = new BUnit();

            unit.oid = collection["oid"];
            unit.gameid = collection["gameid"];
            unit.dateadded = collection["dateadded"];
            unit.loaned = collection["loaned"];
            unit.dateofrent = collection["dateofrent"];
            unit.barcode = collection["barcode"];
            unit.description = collection["description"];

            var jdata = JsonConvert.SerializeObject(unit);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PutAsync ------");
            var httpResponse = await _service.Client.DeleteAsync($"/api/BUnit/{id}");
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

        public async Task<ActionResult> AddToBasket(string id, string gameid)
        {
            BBasket bBasket = new BBasket();

            bBasket.username = User.Identity.Name;
            bBasket.gameid = gameid;
            bBasket.unitid = id;
            bBasket.DateCreated = DateTime.Now.ToString();

            var jdata = JsonConvert.SerializeObject(bBasket);

            var httpContent = new StringContent(jdata, Encoding.UTF8, "application/json");
            Console.WriteLine("---- PostAsync ------");
            var httpResponse = await _service.Client.PostAsync($"/api/BBasket/", httpContent);
            Console.WriteLine("---- PostAsync END------");
            Console.WriteLine(httpResponse);

            try
            {
                return RedirectToAction(nameof(Details), new { id = gameid });
            }
            catch
            {
                return View();
            }
        }
    }
}
