using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Hotel.ApplicationLogic.InterfacesUseCaseCabins;
using Hotel.ApplicationLogic.InterfacesUseCabinType;

namespace Hotel.Web.Controllers
{
    public class CabinController : Controller
    {
        //private ICabinRepository repository;
        private IWebHostEnvironment _environment;
        //UC Repositories
        private IGetAllCabinsUC getAllCabinsUC;
        private IGetByCapacityCabinUC getByCapacityCabinUC;
        private IGetByNameCabinUC getByNameCabinUC;
        private IGetByTypeCabinUC getByTypeCabinUC;
        private IGetOnlyEnableCabinUC getOnlyEnableCabinUC;
        private IAddCabinUC addCabinUC;
        private IGetAllCabinsTypeUC getAllCabinsTypeUC;
        private IGetPictureNameUC getPictureNameUC;
        private IDeleteCabinUC deleteCabinUC;
        

        public CabinController(IWebHostEnvironment environment, ICabinRepository repository, IGetAllCabinsUC getAllCabinsUC, IGetByCapacityCabinUC getByCapacityCabinUC, IGetByNameCabinUC getByNameCabinUC, IGetByTypeCabinUC getByTypeCabinUC, IGetOnlyEnableCabinUC getOnlyEnableCabinUC, IAddCabinUC addCabinUC, IGetAllCabinsTypeUC getAllCabinsTypeUC, IGetPictureNameUC getPictureNameUC, IDeleteCabinUC deleteCabinUC)
        {
            this.getAllCabinsUC = getAllCabinsUC;
            this.getByCapacityCabinUC = getByCapacityCabinUC;
            this.getByNameCabinUC = getByNameCabinUC;
            this.getByTypeCabinUC = getByTypeCabinUC;
            this.getOnlyEnableCabinUC = getOnlyEnableCabinUC;
            this.addCabinUC = addCabinUC;
            this.getAllCabinsTypeUC = getAllCabinsTypeUC;
            this.getPictureNameUC = getPictureNameUC;
            this.deleteCabinUC = deleteCabinUC;
            //this.repository = repository;
            _environment = environment;
        }
        //GET: CabinController
        public ActionResult Index(IEnumerable<Cabin> cabins)
        {
            if (HttpContext.Session.GetString("Rol") == "Funcionario")
            {
                try
                {
                    LoadCabinTypes();
                    return View(getAllCabinsUC.GetAllCabins());
                }
                catch (CabinException ce)
                {
                    ViewBag.Message = ce.Message;
                    return View();
                }
            }
            return Redirect($"/User/Login");
        }

        [HttpPost]
        public ActionResult Index(string? name, string? cabinType, int? capacity, bool? enabledReservation)
        {
            LoadCabinTypes();
            IEnumerable<Cabin> cabins = new List<Cabin>();
            if (name != null)
            {
                cabins = getByNameCabinUC.GetCabinsByName(name);

            }
            else if (cabinType != null)
            {
                cabins = getByTypeCabinUC.GetCabinsByType(cabinType);

            }
            else if (capacity != null)
            {
                cabins = getByCapacityCabinUC.GetCabinsByCapacity(capacity);

            }
            else if (enabledReservation == true)
            {
                cabins = getOnlyEnableCabinUC.GetCabinsOnlyEnable();

            }
            if (cabins.Count() == 0)
            {
                ViewBag.Message = "No se encontraron resultados para el filtro indicado";
            }
            return View(cabins);

        }


        // GET: CabinController/Details/5
        public ActionResult Details(int id)
        {
            //if (HttpContext.Session.GetString("Rol") == "Funcionario")
            //{
            //    return View(repository.GetById(id));
            //}
            return Redirect($"/User/Login");
        }

        // GET: CabinController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") == "Funcionario")
            {
                LoadCabinTypes();
                return View();
            }
            return Redirect($"/User/Login");
        }

        // POST: CabinController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cabin cabin, IFormFile image)
        {
            try
            {
                SaveImage(image, cabin);
                addCabinUC.Add(cabin);
                return RedirectToAction(nameof(Index));
            }
            catch (CabinException cs)
            {
                LoadCabinTypes();
                ViewBag.Message = cs.Message;
                return View();
            }
        }
        private void SaveImage(IFormFile image, Cabin cabin)
        {
            if (image == null || cabin == null) { throw new CabinException("No se ingresaron todos los datos con la foto."); }
            string phyisicalRoutWwwRoot = _environment.WebRootPath;
            string fileExtension = Path.GetExtension(image.FileName);
            string imageName = getPictureNameUC.GetPictureName(cabin) + fileExtension;
            string physicalRoutPicture = Path.Combine(phyisicalRoutWwwRoot, "images", "pictures", imageName);

            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar
                using (FileStream f = new FileStream(physicalRoutPicture, FileMode.Create))
                {
                    //Para archivos grandes o varios archivos usar la versión
                    //asincrónica de CopyTo. Sería: await imagen.CopyToAsync (f);
                    image.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO
                cabin.Picture = imageName;
            }
            catch (Exception ex)
            {
                throw new CabinException(ex.Message);
            }
        }
        private void LoadCabinTypes()
        {
            var cabinTypes = getAllCabinsTypeUC.GetAllCabinsType();
            var cabinItems = cabinTypes.Select(CabinType => new SelectListItem
            {
                Value = CabinType.Id.ToString(),
                Text = CabinType.Name
            });
            ViewBag.CabinTypes = cabinItems;

        }

        // GET: CabinController/Edit/5
        public ActionResult Edit(int id)
        {
            //if (HttpContext.Session.GetString("Rol") == "Funcionario")
            //{
            //    return View();
            //}
            return Redirect($"/User/Login");
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
            //if (HttpContext.Session.GetString("Rol") == "Funcionario") 
            //{
            //    return View(repository.GetById(id));
            //}
            return Redirect($"/User/Login");
        }

        // POST: CabinController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Cabin cabin)
        {
            try
            {
                deleteCabinUC.Delete(cabin);
                return RedirectToAction(nameof(Index));
            }
            catch (CabinException ce)
            {
                ViewBag.Message = ce.Message;
                return View();
            }
        }
    }
}
