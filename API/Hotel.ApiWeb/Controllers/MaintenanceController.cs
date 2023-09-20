using DTOs;
using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseMaintenance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MaintenanceController : Controller
    {

        private IAddMaintenanceDtoUC addMaintenanceDtoUC;
        private IGetMaintenancesDtoByDateUC getMaintenanceByDateUC;
        private IGetMaintenancesByCabinCapacityUC getMaintenancesByCabinCapacityUC;

        public MaintenanceController(IAddMaintenanceDtoUC addMaintenanceDtoUC, IGetMaintenancesDtoByDateUC getMaintenanceByDateUC, IGetMaintenancesByCabinCapacityUC getMaintenancesByCabinCapacityUC)
        {
            this.addMaintenanceDtoUC = addMaintenanceDtoUC;
            this.getMaintenanceByDateUC = getMaintenanceByDateUC;
            this.getMaintenancesByCabinCapacityUC = getMaintenancesByCabinCapacityUC;
        }

        /// <summary>
        /// Retorna todos los mantenimientos de una cabaña entre fechas determinadas
        /// </summary>
        /// <returns>IEnumerableMaintenance</returns>
        [HttpGet("{CabinId}/{dateFrom}/{dateTo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMaintenance(int CabinId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var maintenances = getMaintenanceByDateUC.GetMaintenancesDtoByDate(CabinId, dateFrom, dateTo);
                if (maintenances.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(maintenances);
            }
            catch (CabinException cEx)
            {
                return NotFound(cEx.Message);
            }
        }

        /// <summary>
        /// Creacion de un mantenimiento para una cabaña
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult PostMaintenance([FromBody] MaintenanceDto maintenance)
        {
            try
            {
                addMaintenanceDtoUC.AddMaintenanceDto(maintenance);
                return Created("api/[controller]", maintenance);
            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }
        }

        /// <summary>
        /// Metodo que dadas dos capacidades de cabaña, devuelve todos los mantenimientos realizados, agrupados por trabajador
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>IEnumerable Maintenance</returns>
        [HttpGet("{value1}/{value2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMaintenancesByCabinCapacity(int value1, int value2) 
        {
            try 
            {
                var maintenances = getMaintenancesByCabinCapacityUC.GetMaintenancesByCabinCapacity(value1, value2);
                if (maintenances.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(maintenances);
            } catch (CabinException cEx) 
            {
                return NotFound(cEx.Message);
            }
        }

    }
}
