using Hotel.ApplicationLogic.InterfacesUseCaseUser;
using Hotel.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_1.Exceptions;

namespace Hotel.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LoginController : Controller
    {
        private IConfiguration configuration { get; set; }
        private ILoginDtoUC loginDtoUC;
        public LoginController(IConfiguration configuration, ILoginDtoUC loginDtoUC) 
        {
            this.configuration = configuration;
            this.loginDtoUC = loginDtoUC;
        }

        /// <summary>
        /// Metodo que recibe un usuario y devuelve el token
        /// </summary>
        /// <param name="user">Credenciales del usuario que desea iniciar sesion</param>
        /// <returns>Token generada</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] UserDto actualUser)
        {
            try
            {
                var user = loginDtoUC.LoginDto(actualUser);
             
                
                var token = JwtManager.CreateToken(user, configuration);

                return Ok(new
                {
                    Token = token,
                    User = user
                });
            }
            catch (CabinException cEx)
            {
                return BadRequest(cEx.Message);
            }            
        }
    }
}
