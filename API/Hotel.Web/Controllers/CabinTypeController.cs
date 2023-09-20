using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseCabinType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;

namespace Hotel.Web.Controllers
{
    public class CabinTypeController : Controller
    {
        //private ICabinTypeRepository repository;

        private IGetAllCabinsTypeUC getAllCabinsTypeUC;
        private IGetByIdCabinTypeUC getByIdCabinTypeUC;
        private IGetByNameCabinTypeUC getByNameCabinTypeUC;
        private IAddCabinTypeUC addCabinTypeUC;
        private IUpdateCabinTypeUC updateCabinTypeUC;
        private IDeleteCabinTypeUC deleteCabinTypeUC;

        public CabinTypeController(ICabinTypeRepository repository, IGetAllCabinsTypeUC getAllCabinsType,
          IGetByIdCabinTypeUC getByIdCabinTypeUC, IGetByNameCabinTypeUC getByNameCabinTypeUC,
          IAddCabinTypeUC addCabinTypeUC, IUpdateCabinTypeUC updateCabinTypeUC, IDeleteCabinTypeUC deleteCabinTypeUC
          )
        {
            //this.repository = repository;
            this.getAllCabinsTypeUC = getAllCabinsType;
            this.getByIdCabinTypeUC = getByIdCabinTypeUC;
            this.getByNameCabinTypeUC = getByNameCabinTypeUC;
            this.addCabinTypeUC = addCabinTypeUC;
            this.updateCabinTypeUC= updateCabinTypeUC;
            this.deleteCabinTypeUC = deleteCabinTypeUC;
        }
        // GET: CabinTypeController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            try
            {
                if (TempData["message"] != null)
                {
                    ViewBag.Message = TempData["message"];
                }
                if (TempData["messageDelete"] != null)
                {
                    ViewBag.MessageDelete = TempData["messageDelete"];
                }
                if (TempData["messageSearch"] != null)
                {
                    ViewBag.MessageSearch = TempData["messageSearch"];
                }
                var types = getAllCabinsTypeUC.GetAllCabinsType();

                if (types.Count() == 0)
                {
                    ViewBag.Message = "Aun no se ha cargado ningun tipo de cabaña.";
                }
                return View(types);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: CabinTypeController/Details/5
        public ActionResult Details(CabinType type)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            return View(type);
        }
        public ActionResult DetailsById(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            var ty = getByIdCabinTypeUC.GetCTById(id);
            return RedirectToAction(nameof(Details), ty);
        }
        public ActionResult DetailsByName(string name)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            var filtred = getByNameCabinTypeUC.GetByName(name);
            if (filtred != null)
            {
                return RedirectToAction(nameof(Details), filtred);
            }
            else
            {
                TempData["messageSearch"] = "La busqueda no coincide con ningun tipo de cabaña.";

                return RedirectToAction(nameof(Index));
            }

        }

        // GET: CabinTypeController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            return View();
        }

        // POST: CabinTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabinType cabinType)
        {
            try
            {
                addCabinTypeUC.Add(cabinType);
                return RedirectToAction(nameof(Index));
            }
            catch (CabinException e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: CabinTypeController/Edit/5
        public ActionResult Edit(CabinType cabinType)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            return View();
        }
        public ActionResult EditByName(string name)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            var filtred = getByNameCabinTypeUC.GetByName(name);
            if (filtred != null)
            {
                return RedirectToAction(nameof(Edit), filtred);
            }
            else
            {
                TempData["message"] = "La busqueda no coincide con ningun tipo de cabaña.";
                //ViewBag.Message = "La busqueda no coincide con ningun tipo de cabaña.";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: CabinTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var cabinType = getByIdCabinTypeUC.GetCTById(id);
            try
            {
                string desccription = collection["Description"];
                int costPerson = int.Parse(collection["costPerson"]);
                updateCabinTypeUC.Update(cabinType, desccription, costPerson);
                return RedirectToAction(nameof(Index));
            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View(cabinType);
            }
        }

        // GET: CabinTypeController/Delete/5

        public ActionResult Delete(CabinType ty)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            try
            {
                return View(ty);

            }
            catch (CabinException e)
            {
                ViewBag.Message = e.Message;
                return View(ty);
            }
        }
        public ActionResult DeleteById(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            var ty = getByIdCabinTypeUC.GetCTById(id);

            return RedirectToAction(nameof(Delete), ty);
        }
        public ActionResult DeleteByName(string name)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            var filtred = getByNameCabinTypeUC.GetByName(name);
            if (filtred != null)
            {
                return RedirectToAction(nameof(Delete), filtred);
            }
            else
            {
                TempData["messageDelete"] = "La busqueda no coincide con ningun tipo de cabaña.";
                //ViewBag.Message = "La busqueda no coincide con ningun tipo de cabaña.";
                return RedirectToAction(nameof(Index));
            }

        }

        //POST: CabinTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var cabinType = getByIdCabinTypeUC.GetCTById(id);
            try
            {
                deleteCabinTypeUC.Delete(cabinType);
                return RedirectToAction(nameof(Index));
            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View(cabinType);
            }
        }
    }
}
