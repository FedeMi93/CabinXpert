using DTOs;
using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseCabins;
using Hotel.ApplicationLogic.InterfacesUseCaseCabinType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;

namespace Hotel.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CabinController : Controller
    {
        //UC Repositories
        private IGetAllCabinsDtoUC getAllCabinsUC;
        private IGetByCapacityCabinsDtoUC getByCapacityCabinUC;
        private IGetByNameCabinDtoUC getByNameCabinUC;
        private IGetByTypeCabinDtoUC getByTypeCabinUC;
        private IGetOnlyEnableCabinDtoUC getOnlyEnableCabinUC;
        private IAddCabinDtoUC addCabinUC;
        private IGetPictureNameUC getPictureNameUC;
        private IDeleteCabinUC deleteCabinUC;
        private IGetCabinDtoByCostUC getCabinByCostUC;
        private IGetByIdCabinUC getCabinByIdUC;



        public CabinController(IGetAllCabinsDtoUC getAllCabinsUC,
            IGetByCapacityCabinsDtoUC getByCapacityCabinUC, IGetByNameCabinDtoUC getByNameCabinUC,
            IGetByTypeCabinDtoUC getByTypeCabinUC, IGetOnlyEnableCabinDtoUC getOnlyEnableCabinUC,
            IAddCabinDtoUC addCabinUC, IGetPictureNameUC getPictureNameUC,
            IDeleteCabinUC deleteCabinUC, IGetCabinDtoByCostUC getCabinByCostUC,
            IGetByIdCabinUC getByIdCabinUC)
        {
            this.getAllCabinsUC = getAllCabinsUC;
            this.getByCapacityCabinUC = getByCapacityCabinUC;
            this.getByNameCabinUC = getByNameCabinUC;
            this.getByTypeCabinUC = getByTypeCabinUC;
            this.getOnlyEnableCabinUC = getOnlyEnableCabinUC;
            this.addCabinUC = addCabinUC;
            this.getPictureNameUC = getPictureNameUC;
            this.deleteCabinUC = deleteCabinUC;
            this.getCabinByCostUC = getCabinByCostUC;
            this.getCabinByIdUC = getByIdCabinUC;


        }

        /// <summary>
        /// Este metodo devuelve todas las cabañas
        /// </summary>
        /// <returns>IEnumerable cabin</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<CabinDto> GetAllCabins()
        {
            return getAllCabinsUC.GetAllCabinDto();
        }

        /// <summary>
        /// Este metodo filtra las cabañas por su capacidad y devuelve las que sean mayores o iguales a la idicada
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns>IEnumerable cabin</returns>
        [HttpGet("{capacity:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCabinByCapacity(int capacity)
        {
            try
            {
                var cabins = getByCapacityCabinUC.GetCabinsDtoByCapacity(capacity);
                if (cabins.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(cabins);

            }
            catch (CabinException cEx)
            {
                return NotFound(cEx.Message);
            }
        }
        /// <summary>
        /// Metodo que devuelve las cabañas filtradas por nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns>IEnumerableCabin</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCabinByName(string name)
        {
            try
            {
                var cabins = getByNameCabinUC.GetCabinsDtoByName(name);
                if (cabins.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(cabins);

            }
            catch (CabinException cEx)
            {
                return NotFound(cEx.Message);
            }
        }

        /// <summary>
        /// Metodo que devuelve las cabañas filtradas por el tipo de cabaña
        /// </summary>
        /// <param name="cabinTypeId"></param>
        /// <returns>IEnumerableCabin</returns>
        [HttpGet("CabinType/{cabinTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCabinByType(string cabinTypeId)
        {
            try
            {
                var cabins = getByTypeCabinUC.GetCabinsDtoByType(cabinTypeId);
                if (cabins.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(cabins);

            }
            catch (CabinException cEx)
            {
                return NotFound(cEx.Message);
            }
        }

        /// <summary>
        /// Metodo que devuelve solo las cabañas habilitadas para reserva
        /// </summary>        
        /// <returns>IEnumerable Cabin</returns>
        [HttpGet("Enable/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOnlyEnable()
        {
            try
            {
                var cabins = getOnlyEnableCabinUC.GetCabinsDtoOnlyEnable();
                if (cabins.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(cabins);

            }
            catch (CabinException cEx)
            {
                return NotFound(cEx.Message);
            }
        }


        /// <summary>
        /// Creacion de una cabaña en la base de datos
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult CreateCabin([FromBody] CabinDto cabin)
        {
            try
            {
                addCabinUC.AddDto(cabin);
                return Created("api/[controller]", cabin);
            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }
        }
        // ver el tema de las imagenes, hay que pasarlas por tipo mime

        /// <summary>
        /// Metodo que devuelve el nombre de una imagen, dado el nombre de la cabaña
        /// </summary>        
        /// <returns>String</returns>
        [HttpGet("Picture/{cabinName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPictureName(string cabinName)
        {
            try
            {
                if (cabinName == null)
                {
                    return NoContent();
                }
                string pictureName = getPictureNameUC.GetPictureName(cabinName);
                return Ok(pictureName);

            }
            catch (CabinException cEx)
            {
                return NotFound(cEx.Message);
            }
        }

        /// <summary>
        /// Metodo que elimina una cabaña
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpDelete("{cabinId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCabinType(int cabinId)
        {
            try
            {
                var cabin = getCabinByIdUC.GetById(cabinId);
                if (cabin != null)
                {
                    deleteCabinUC.Delete(cabin);
                    return Ok();
                }
                return NoContent();

            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }
        }

        /// <summary>
        /// Metodo que devuelve las cabañas cuyo precio sea menor al indicado 
        /// </summary>
        /// <param name="cabinCost"></param>
        /// <returns>IEnumerable cabin</returns>
        [HttpGet("Cost/{cabinCost}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        public IActionResult GetCabinByCost(int cabinCost)
        {
            var cabins = getCabinByCostUC.GetCabinDtoByCostUC(cabinCost);
            if (cabins.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(cabins);
        }



    }
}
