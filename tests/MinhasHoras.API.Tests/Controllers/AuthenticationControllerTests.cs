using Microsoft.AspNetCore.Mvc;
using MinhasHoras.API.Controllers;
using MinhasHoras.API.DTOs.Users;
using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Results.Users;
using MinhasHoras.Application.Services.Interfaces;
using Moq;

namespace MinhasHoras.API.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IAuthenticationService> _authenticationServiceMock = new();
        private readonly AuthenticationController _controller;
        private readonly AuthenticateDTO _authenticateDTO = new(Email: "test@test.com", Password: "T3$t3");

        public AuthenticationControllerTests()
        {
            _controller = new AuthenticationController(_authenticationServiceMock.Object);
        }

        [Fact]
        public async Task Authenticate_WhenInvalidCredentials_ShouldReturnUnauthorized()
        {
            _authenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).Throws<InvalidCredentialsException>();

            var result = await _controller.Authenticate(_authenticateDTO);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equivalent(new { Message = "E-mail ou senha inválidos. Verifique suas credenciais e tente novamente." }, unauthorizedResult.Value);
        }

        [Fact]
        public async Task Authenticate_WhenUnexpectedError_ShouldReturnInternalServerError()
        {
            _authenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            var result = await _controller.Authenticate(_authenticateDTO);

            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equivalent(new { Message = "Ocorreu um erro inesperado" }, internalServerErrorResult.Value);
        }

        [Fact]
        public async Task Authenticate_WhenUserIsAuthenticated_ShouldReturnOk()
        {
            _authenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new UserResult(id: "Id", name: "Test", email: "test@test.com", token: "token"));

            var result = await _controller.Authenticate(_authenticateDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equivalent(new { Id = "Id", Name = "Test", Email = "test@test.com", Token = "token" }, okResult.Value);
        }
    }
}
