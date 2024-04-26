using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhasHoras.API.Controllers;
using MinhasHoras.API.DTOs.Users;
using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Results.Users;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.Entities;
using Moq;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace MinhasHoras.API.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly UserController _controller;
        private readonly CreateUserDTO _createUserDTO = new(Name: "Test", Email: "test@test.com", Password: "T3$te");

        public UserControllerTests()
        {
            _controller = new UserController(_mapperMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_WhenEmailAlreadyExists_ShouldReturnBadRequest()
        {
            _userServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Throws<EmailAlreadyExistsException>();

            var result = await _controller.CreateUser(_createUserDTO);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equivalent(new { Message = "Este e-mail já está sendo utilizado por outro usuário" }, badRequestResult.Value);
        }

        [Fact]
        public async Task CreateUser_WhenUnexpectedError_ShouldReturnInternalServerError()
        {
            _userServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Throws<Exception>();

            var result = await _controller.CreateUser(_createUserDTO);

            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equivalent(new { Message = "Ocorreu um erro inesperado" }, internalServerErrorResult.Value);
        }

        [Fact]
        public async Task CreateUser_WhenUserIsCreated_ShouldReturnOk()
        {
            _userServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(new UserResult(id: "Id", name: "Test", email: "test@test.com", token: "token"));

            var result = await _controller.CreateUser(_createUserDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equivalent(new { Id = "Id", Name = "Test", Email = "test@test.com", Token = "token" }, okResult.Value);

        }
    }
}
