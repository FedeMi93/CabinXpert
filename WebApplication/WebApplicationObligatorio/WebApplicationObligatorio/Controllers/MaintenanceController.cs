using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApplicationObligatorio.Models;

namespace WebApplicationObligatorio.Controllers
{
    public class MaintenanceController : Controller
    {
        private string localhost = "https://localhost:7206/api/Maintenance";
        // GET: MaintenanceController
        public ActionResult Index(int cabinId, IEnumerable<MaintenanceModel> maintenances)
        {
            ViewBag.CabinId = cabinId;
            return View(maintenances);
        }
        [HttpPost]
        public ActionResult Index(int cabinId, DateTime fromDate, DateTime toDate)
        {
            string NewFromDate = fromDate.ToString("yyyy-MM-dd");
            string NewToDate = toDate.ToString("yyyy-MM-dd");
            ViewBag.CabinId = cabinId;
            DateTime currentDate = DateTime.Now;
            var EmptyMaintenances = new List<MaintenanceModel>();
            if (fromDate > currentDate)
            {
                ViewBag.Message = "Se selecciono una fecha mayor a la de hoy.";
                return View(EmptyMaintenances);
            }
            if (fromDate > toDate)
            {
                ViewBag.Message = "La fecha desde, no puede ser mayor a la fecha hasta donde se quiere buscar.";
                return View(EmptyMaintenances);
            }


            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            /******************* HEADERS *******************/

            Uri uri = new Uri(localhost + "/" + cabinId + "/" + NewFromDate + "/" + NewToDate);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            
            /*************** END CONTENIDO O BODY ********************/

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Console.WriteLine(respuesta.Result.StatusCode.ToString());

            Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));
            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();            

            if (respuesta.Result.IsSuccessStatusCode)
            {
                
                if (respuesta.Result.StatusCode.ToString() == "NoContent")
                {
                    ViewBag.Message = "No se encontraron mantenimientos entre las fechas indicadas.";
                }
                else { 
                    MaintenanceModel[] maintenances = JsonConvert.DeserializeObject<MaintenanceModel[]>(response.Result);               
                    return View(maintenances);                
                }
            }
            return View(EmptyMaintenances);
            


        }

            // GET: MaintenanceController/Details/5
            public ActionResult Details()
        {
            return View();
        }

        // GET: MaintenanceController/Create
        public ActionResult Create(int cabinId, string message)
        {
            ViewBag.message = message;
            ViewBag.CabinId = cabinId;
            return View();
        }

        // POST: MaintenanceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaintenanceModel maintenance)
        {
            try
            {

                var token = HttpContext.Session.GetString("token");

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                /******************* HEADERS *******************/

                Uri uri = new Uri(localhost);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

                /******************* CONTENIDO O BODY ********************/
                string json = JsonConvert.SerializeObject(maintenance);
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
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Create", new { message = response.Result });

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Create", new { message = e.Message });
            }
        }

        // GET: MaintenanceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaintenanceController/Edit/5
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

        // GET: MaintenanceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaintenanceController/Delete/5
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
