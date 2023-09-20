using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using WebApplicationObligatorio.Models;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;


namespace WebApplicationObligatorio.Controllers
{
    public class CabinTypeController : Controller
    {
        private string localhost = "https://localhost:7206/api/CabinType";

        // GET: CabinTypeController1
        public ActionResult Index(string message, string messageDBN, string messageDelete)
        {
            var token = HttpContext.Session.GetString("token");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Uri uri = new Uri("https://localhost:7206/api/CabinType");
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabinTypeModel[] cabinsTypes = JsonConvert.DeserializeObject<CabinTypeModel[]>(response.Result);

            ViewBag.message = message;
            ViewBag.messageDBN = messageDBN;
            ViewBag.messageDelete = messageDelete;
            return View(cabinsTypes);
        }

        [HttpGet]
        public ActionResult Details(CabinTypeModel cabinType)
        {
            return View(cabinType);
        }
        // GET: CabinTypeController1/Details/5
        public ActionResult DetailsById(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Uri uri = new Uri("https://localhost:7206/api/CabinType/" + id);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                if (respuesta.Result.IsSuccessStatusCode)
                {
                    CabinTypeModel cabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);
                    return RedirectToAction("Details", cabinType);
                }
                return RedirectToAction("Index", new { message = "Algo salio mal al cargar los detalles." });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }
        public ActionResult DetailsByName(string name)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Uri uri = new Uri("https://localhost:7206/api/CabinType/" + name);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                if (respuesta.Result.IsSuccessStatusCode)
                {
                    CabinTypeModel cabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);
                    //ViewBag.CabinType = cabinType;
                    return RedirectToAction("Details", cabinType);
                }
                return RedirectToAction("Index", new { messageDBN = "No se encontraron coincidencias con el nombre ingresado." });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Index", new { message = e.Message });
            }

        }

        // GET: CabinTypeController1/Create
        public ActionResult Create(string message)
        {
            ViewBag.message = message;
            return View();
        }

        // POST: CabinTypeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabinTypeModel cabinCreateModel)
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
                string json = JsonConvert.SerializeObject(cabinCreateModel);
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
        [HttpGet]
        public ActionResult Edit(CabinTypeModel cabinType)
        {
            return View(cabinType);
        }

        public ActionResult EditByName(string name)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Uri uri = new Uri("https://localhost:7206/api/CabinType/" + name);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                CabinTypeModel cabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);

                return RedirectToAction("Edit", cabinType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }
        public ActionResult EditById(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Uri uri = new Uri("https://localhost:7206/api/CabinType/" + id);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                CabinTypeModel cabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);

                return RedirectToAction("Edit", cabinType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }


        [HttpPost]
        public ActionResult EditPost(CabinTypeModel cabinType)
        {

            var token = HttpContext.Session.GetString("token");
            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Index");
            }
            try
            {

                HttpClient cliente = new HttpClient();

                /******************* HEADERS *******************/
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                /******************* END HEADERS *******************/

                Uri uri = new Uri("https://localhost:7206/api/CabinType/" + cabinType.Id + "/" + cabinType.Description + "/" + cabinType.CostPerPerson);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Put, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                CabinTypeModel newCabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);

                if (respuesta.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("DetailsById", new { id = cabinType.Id });
                }
                return RedirectToAction("EditByName", new { message = "Los campos ingresados no pudieron ser cambiados." });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }

        }


        public ActionResult Delete(CabinTypeModel cabinType)
        {
            return View(cabinType);
        }

        [HttpGet]
        public ActionResult DeleteById(int id)
        {

            var token = HttpContext.Session.GetString("token");

            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Index");
            }

            HttpClient cliente = new HttpClient();

            /******************* HEADERS *******************/
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            /******************* END HEADERS *******************/

            Uri uri = new Uri(localhost + "/" + id);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            if (respuesta.Result.IsSuccessStatusCode)
            {
                CabinTypeModel cabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);
                return RedirectToAction("Delete", cabinType);
            }

            return RedirectToAction("Index", new { message = "No se encontraron tipos de cabaña con ese Id." });
        }

        public ActionResult DeleteByName(string name)
        {
            var token = HttpContext.Session.GetString("token");

            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Index");
            }

            HttpClient cliente = new HttpClient();

            /******************* HEADERS *******************/
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            /******************* END HEADERS *******************/

            Uri uri = new Uri(localhost + "/" + name);
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();
            if (respuesta.Result.IsSuccessStatusCode)
            {
                CabinTypeModel cabinType = JsonConvert.DeserializeObject<CabinTypeModel>(response.Result);

                return RedirectToAction("Delete", cabinType);
            }

            return RedirectToAction("Index", new { messageDelete = "No hubo coincidencias con el nombre ingresado." });

        }

        // POST: CabinTypeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(CabinTypeModel cabinTypeModel)
        {
            try
            {
                HttpClient cliente = new HttpClient();

                var token = HttpContext.Session.GetString("token");

                /******************* HEADERS *******************/
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                /******************* END HEADERS *******************/

                Uri uri = new Uri(localhost + "/" + cabinTypeModel.Id);
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Delete, uri);
                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                if (respuesta.Result.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index), new { message = "Se elimino correctamente el tipo de cabaña." });
                }
                return RedirectToAction("Index", new { message = response.Result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Delete", new { error = e.Message });
            }
        }





    }
}
