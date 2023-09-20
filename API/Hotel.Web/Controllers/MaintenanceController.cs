using Hotel.ApplicationLogic.InterfacesUseCaseMaintenance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;

namespace Hotel.Web.Controllers
{
    public class MaintenanceController : Controller
    {
        //private IMaintenanceRepository repository;
        private IGetCabinByIdMaintenanceUC getCabinByIdMaintenanceUC;
        private IAddMaintenanceUC addMaintenanceUC;
        private IGetMaintenanceByDateUC getMaintenanceByDateUC;

        public MaintenanceController(IMaintenanceRepository repository, IGetCabinByIdMaintenanceUC getCabinByIdMaintenanceUC,
            IAddMaintenanceUC addMaintenanceUC, IGetMaintenanceByDateUC getMaintenanceByDateUC
            )
        {
            //this.repository = repository;
            this.getCabinByIdMaintenanceUC = getCabinByIdMaintenanceUC;
            this.addMaintenanceUC = addMaintenanceUC;
            this.getMaintenanceByDateUC = getMaintenanceByDateUC;
        }

        // GET: MaintenanceController
        public ActionResult Index(int cabinId, IEnumerable<Maintenance> maintenances)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            ViewBag.CabinId = cabinId;
            return View(maintenances);
        }
        [HttpPost]
        public ActionResult Index(int cabinId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                ViewBag.CabinId = cabinId;
                DateTime currentDate = DateTime.Now;
                var EmptyMaintenances = new List<Maintenance>();
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


                var maintenances = getMaintenanceByDateUC.GetMaintenancesByDate(cabinId, fromDate, toDate);
                if (maintenances.Count() <= 0)
                {
                    ViewBag.Message = "No se encontraron mantenimientos entre las fechas indicadas.";
                }
                return View(maintenances);
            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View();
            }
        }

        // GET: MaintenanceController/Details/5
        public ActionResult Details(int id)
        {
            //    if (HttpContext.Session.GetString("Rol") != "Funcionario")
            //    {
            //        return Redirect($"/User/Login");
            //    }
            //    return View();
            return Redirect($"/User/Login");
        }

        // GET: MaintenanceController/Create
        public ActionResult Create(int cabinId)
        {
            if (HttpContext.Session.GetString("Rol") != "Funcionario")
            {
                return Redirect($"/User/Login");
            }
            try
            {
                var cabin = getCabinByIdMaintenanceUC.GetCabinById(cabinId);
                ViewBag.CabinType = cabin.Type.Name;
                ViewBag.CabinRoom = cabin.NuRoom;
                ViewBag.CabinName = cabin.Name;
                ViewBag.CabinDescription = cabin.Description;
                ViewBag.CabinCapacity = cabin.Capacity;
                ViewBag.CabinJacuzzi = cabin.JacuzziPriv;
                ViewBag.CabinEnable = cabin.EnabledReservation;
                ViewBag.CabinId = cabin.Id;
                if (TempData["CorrectMesssage"] != null)
                {
                    ViewBag.CorrectMessage = TempData["CorrectMesssage"];

                }
                else if (TempData["ErrorMesssage"] != null)
                {
                    ViewBag.ExceptionMessage = TempData["ErrorMesssage"];
                }

                return View();

            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View();
            }



        }

        // POST: MaintenanceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Maintenance maintenance)
        {
            int cabinId = maintenance.CabinId;
            try
            {
                addMaintenanceUC.Add(maintenance);
                TempData["CorrectMesssage"] = "Se agrego correctamente el mantenimiento.";
                return RedirectToAction(nameof(Create), new { cabinId });
            }
            catch (CabinException ce)
            {
                TempData["ErrorMesssage"] = ce.Message;
                return RedirectToAction(nameof(Create), new { cabinId });

            }
        }

        // GET: MaintenanceController/Edit/5
        public ActionResult Edit(int id)
        {
            //if (HttpContext.Session.GetString("Rol") != "Funcionario")
            //{
            //    return Redirect($"/User/Login");
            //}
            //return View();
            return Redirect($"/User/Login");
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
            //return View();
            return Redirect($"/User/Login");
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
