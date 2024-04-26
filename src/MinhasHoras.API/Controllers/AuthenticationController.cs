using Microsoft.AspNetCore.Mvc;
using MinhasHoras.API.DTOs.Users;
using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Services.Interfaces;

namespace MinhasHoras.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDTO authenticateDTO)
        {
            try
            {
                var authenticatedUser = await _authenticationService.AuthenticateAsync(authenticateDTO.Email, authenticateDTO.Password);
                return Ok(authenticatedUser);
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro inesperado" });
            }
        }

    }
}
