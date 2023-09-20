using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System;
using System.Net.Http.Headers;
using System.Text;
using WebApplicationObligatorio.Models;

namespace WebApplicationObligatorio.Controllers
{
    public class CabinController : Controller
    {
        private string localhost = "https://localhost:7206/api/Cabin";
        // GET: CabinController
        public ActionResult Index()
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri = new Uri(localhost);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabinModel[] cabins = JsonConvert.DeserializeObject<CabinModel[]>(response.Result);

                        

            LoadCabinTypes();
            return View(cabins);
        }

        private void LoadCabinTypes()
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri2 = new Uri("https://localhost:7206/api/CabinType");
            HttpRequestMessage solicitud2 = new HttpRequestMessage(HttpMethod.Get, uri2);

            Task<HttpResponseMessage> respuesta2 = cliente.SendAsync(solicitud2);
            respuesta2.Wait();

            Task<string> response2 = respuesta2.Result.Content.ReadAsStringAsync();

            CabinTypeModel[] cabinsType = JsonConvert.DeserializeObject<CabinTypeModel[]>(response2.Result);
            
            
            var cabinItems = cabinsType.Select(CabinType => new SelectListItem
            {
                Value = CabinType.Id.ToString(),
                Text = CabinType.Name
            });
            ViewBag.CabinTypes = cabinItems;

        }
        [HttpPost]
        public ActionResult Index(string? name, string? cabinType, int? capacity, bool? enabledReservation)
        {
            LoadCabinTypes();
            IEnumerable<CabinModel> cabins = new List<CabinModel>();
            if (name != null)
            {
                cabins = GetCabinsByName(name);

            }
            else if (cabinType != null)
            {
                cabins = GetCabinsByType(cabinType);

            }
            else if (capacity != null)
            {
                cabins = GetCabinsByCapacity(capacity);

            }
            else if (enabledReservation == true)
            {
                cabins = GetCabinsOnlyEnable();

            }
            if (cabins == null)
            {
                ViewBag.Message = "No se encontraron resultados para el filtro indicado";
                IEnumerable<CabinModel> cabins2 = new List<CabinModel>();
                return View(cabins2);
            }
            return View(cabins);

        }
        public IEnumerable<CabinModel> GetCabinsByName(string name) 
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri = new Uri(localhost + "/" + name);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabinModel[] cabins = JsonConvert.DeserializeObject<CabinModel[]>(response.Result);
            return cabins;
        }
        public IEnumerable<CabinModel> GetCabinsByType(string cabinType)
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri = new Uri(localhost + "/CabinType/" + cabinType);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabinModel[] cabins = JsonConvert.DeserializeObject<CabinModel[]>(response.Result);
            return cabins;
        }
        public IEnumerable<CabinModel> GetCabinsByCapacity(int? capacity)
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri = new Uri(localhost + "/" + capacity);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabinModel[] cabins = JsonConvert.DeserializeObject<CabinModel[]>(response.Result);
            return cabins;
        }
        public IEnumerable<CabinModel> GetCabinsOnlyEnable()
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri = new Uri(localhost + "/Enable");
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabinModel[] cabins = JsonConvert.DeserializeObject<CabinModel[]>(response.Result);
            return cabins;
        }
        
      

        // GET: CabinController/Create
        public ActionResult Create(string Message)
        {
            ViewBag.Message = Message;
            LoadCabinTypes();
            return View();
        }

        // POST: CabinController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabinModel cabin, IFormFile image)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                /******************* HEADERS *******************/

                Uri uri = new Uri("https://localhost:7206/api/Cabin");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

                /******************* CONTENIDO O BODY ********************/
                cabin.Picture = GetPictureName(cabin.Name);
                string json = JsonConvert.SerializeObject(cabin);
                HttpContent contenido =
                new StringContent(json, Encoding.UTF8, "application/json");
                solicitud.Content = contenido;
                /*************** END CONTENIDO O BODY ********************/

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Console.WriteLine(respuesta.Result.StatusCode.ToString());

                Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                if (respuesta.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { message = "Cabaña creada exitosamente." });
                }
                return RedirectToAction("Create", new { Message = response.Result });                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create", new { Message = ex.Message }); 
            }
        }
        public string GetPictureName(string name) 
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            /******************* HEADERS *******************/

            Uri uri = new Uri("https://localhost:7206/api/Cabin/Picture/" + name);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Console.WriteLine(respuesta.Result.StatusCode.ToString());

            Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));
            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            if (respuesta.Result.IsSuccessStatusCode)
            {
                return response.Result;
            }
            return "No se encontro";
        }

        // GET: CabinController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CabinController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CabinController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CabinController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
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
