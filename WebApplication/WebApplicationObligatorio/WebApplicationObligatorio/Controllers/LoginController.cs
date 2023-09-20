using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Text;
using WebApplicationObligatorio.Models;

namespace WebApplicationObligatorio.Controllers
{
    public class LoginController : Controller
    {
        private string localhost = "https://localhost:7206/";

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        // GET: LoginController
        public ActionResult Index(string error)
        {
           ViewBag.error = error;
            return View();
        }

        [HttpPost]
        public ActionResult Index(UsuarioModel usuario)
        {
            HttpClient cliente = new HttpClient();

            /******************* HEADERS *******************/
            //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1bm9AZ21haWwuY29tIiwiZXhwIjoxNjg2MzE3Mjk2fQ.pb0jKJbRKxXDdWPnoQy-RcaIB2YAV-R_5sPPnmyyRnntKW6pIioU2A_IZgo9ELIX_demBsMX_LzMWhSySIsKMA");

            Uri uri = new Uri(localhost + "api/Login");
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

            /******************* CONTENIDO O BODY ********************/
            string json = JsonConvert.SerializeObject(usuario);
            HttpContent contenido =
            new StringContent(json, Encoding.UTF8, "application/json");
            solicitud.Content = contenido;
            /*************** END CONTENIDO O BODY ********************/

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

                Console.WriteLine(respuesta.Result.StatusCode.ToString());

                Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));

            if (respuesta.Result.IsSuccessStatusCode) {
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(response.Result);
                ViewBag.Token = usuarioModel.Token;
              
                HttpContext.Session.SetString("token", usuarioModel.Token);
                return Redirect($"/Cabin/Index");
            }                    
            return RedirectToAction("Index", new { error = "Los datos de usuario y contraseña no son validos, vuelva a intentarlo."});
        }
    }
}
