using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_1.Interfaces;
using Obligatorio_1.Entidades;
using System.Net;
using Obligatorio_1.Exceptions;
using Hotel.ApplicationLogic.InterfacesUseCaseUser;

namespace Hotel.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository repository;
        //UC Repositories
        private ILoginUC loginUC;

        public UserController(IUserRepository repository, ILoginUC loginUC)
        {
            this.repository = repository;
            this.loginUC = loginUC;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            //return View(repository.GetAll());
            return RedirectToAction("Login");
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            //return View();
            return RedirectToAction("Login");
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            //return View();
            return RedirectToAction("Login");
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                repository.Add(user);
                HttpContext.Session.SetString("Rol", "Funcionario");
                return Redirect($"/Cabin/Index");

            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            //return View();
            return RedirectToAction("Login");
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            //return View();
            return RedirectToAction("Login");
        }

        // POST: UsuarioController/Delete/5
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
        public IActionResult Login(string message)
        {
            if (HttpContext.Session.GetString("Rol") == "Funcionario")
            {
                return Redirect($"/Cabin/Index");
            }
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("Login", new { message = "Complete todos los campos" });
                }
                if (loginUC.Login(email, password))
                {
                    HttpContext.Session.SetString("Rol", "Funcionario");
                    return Redirect($"/Cabin/Index");
                }
                return RedirectToAction("Login", new { message = "Datos incorrectos" });

            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View();
            }
        }
        //Enlace para que envie un alert antes de salir
        //<ion-button alain="button" style="position: fixed; bottom: 0;" shape="round" color="danger" fill="outline" onclick="CerrarMenu(), presentAlertLogout()">Logout</ion-button>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
