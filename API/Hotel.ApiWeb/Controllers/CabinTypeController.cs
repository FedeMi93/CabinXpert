using DTOs;
using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseCabinType;
using Hotel.ApplicationLogic.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
    public class CabinTypeController : Controller
    {
        private IGetAllCabinTypeDtoUC getAllCabinsTypeUC;
        private IGetByIdCabinTypeDtoUC getByIdCabinTypeDtoUC;
        private IGetByNameCabinTypeDtoUC getByNameCabinTypeUC;
        private IAddCabinTypeDtoUC addCabinTypeUC;
        private IUpdateCabinTypeUC updateCabinTypeUC;
        private IDeleteCabinTypeUC deleteCabinTypeUC;
        private IGetByIdCabinTypeUC getByIdCabinTypeUC;

        public CabinTypeController(IGetAllCabinTypeDtoUC getAllCabinsType,
          IGetByIdCabinTypeDtoUC getByIdCabinTypeDtoUC, IGetByNameCabinTypeDtoUC getByNameCabinTypeUC,
          IAddCabinTypeDtoUC addCabinTypeUC, IUpdateCabinTypeUC updateCabinTypeUC, IDeleteCabinTypeUC deleteCabinTypeUC, IGetByIdCabinTypeUC getByIdCabinTypeUC
          )
        {           
            this.getAllCabinsTypeUC = getAllCabinsType;
            this.getByIdCabinTypeDtoUC = getByIdCabinTypeDtoUC;
            this.getByNameCabinTypeUC = getByNameCabinTypeUC;
            this.addCabinTypeUC = addCabinTypeUC;
            this.updateCabinTypeUC = updateCabinTypeUC;
            this.deleteCabinTypeUC = deleteCabinTypeUC;
            this.getByIdCabinTypeUC = getByIdCabinTypeUC;
        }

        /// <summary>
        /// Retorna todos los tipos de cabaña
        /// </summary>
        /// <returns>Restorna un listado con todas las cabañas</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCabinTypes()
        {
            var cabinsType = getAllCabinsTypeUC.GetAllCabinTypeDto();
            if (cabinsType.IsNullOrEmpty()) 
            {
                return NoContent();
            }
            return Ok(cabinsType);

        }
        /// <summary>
        /// Metodo que retorna el tipo de cabaña que coincida con el id enviado
        /// </summary>
        /// <returns>CabinType</returns>
        [HttpGet("{cabinTypeid:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCabinTypesByID(int cabinTypeid)
        {
            var cabinType = getByIdCabinTypeDtoUC.GetCTDtoById(cabinTypeid);
            if (cabinType != null)
            {
                return Ok(cabinType);
            }

            return NoContent();
        }
        /// <summary>
        /// Metodo que retorna el tipo de cabaña que coincida con el nombre enviado
        /// </summary>
        /// <returns>CabinType</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCabinTypeByName(string name)
        {
            var cabinType = getByNameCabinTypeUC.GetByNameDto(name);

            if (cabinType != null)
            {
                return Ok(cabinType);
            }

            return NotFound();
        }
        /// <summary>
        /// Metodo que actualiza un tipo de  cabaña
        /// </summary>
        /// <returns>CabinType</returns>
        [HttpPut("{cabinTypeid}/{newDescription}/{newCost}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutCabinType(int cabinTypeid, string newDescription, int newCost)
        {
            try
            {
                var cabinType = getByIdCabinTypeUC.GetCTById(cabinTypeid);
                if (cabinType != null)
                {
                    updateCabinTypeUC.Update(cabinType, newDescription, newCost);
                    return Ok(cabinType);
                }

                return NoContent();

            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }
        }

        /// <summary>
        /// Creacion de un tipo de cabaña en la base de datos
        /// </summary>
        /// 
        /// <returns>Retorna el tipo de cabaña recién creado</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult CreateCabinType([FromBody] CabinTypeDto cabinType)
        {
            try
            {
                addCabinTypeUC.AddDto(cabinType);
                return Created("api/[controller]", cabinType);
            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }
        }
        /// <summary>
        /// Elimina un tipo de cabaña
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpDelete("{cabinTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCabinType(int cabinTypeId)
        {
            try
            {
                var cabinType = getByIdCabinTypeUC.GetCTById(cabinTypeId);
                if (cabinType != null)
                {
                    deleteCabinTypeUC.Delete(cabinType);
                    return Ok();
                }
                return NoContent();
            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }
        }

    }
}
