using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhasHoras.API.DTOs.Users;
using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.Entities;

namespace MinhasHoras.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IMapper mapper, IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var createdUser = await _userService.CreateUser(_mapper.Map<User>(createUserDTO));
                return Ok(createdUser);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro inesperado" });
            }

        }
    }
}
