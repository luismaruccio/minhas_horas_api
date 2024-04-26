using AutoMapper;
using MinhasHoras.API.Confs;
using MinhasHoras.API.DTOs.Users;
using MinhasHoras.Domain.Entities;

namespace MinhasHoras.API.Tests.Confs
{
    public class MappingProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_CreateUserDTO_To_User_Mapping()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO( Name: "Test", Email: "test@example.com", Password: "password" );

            // Act
            var user = _mapper.Map<User>(createUserDTO);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(createUserDTO.Email, user.Email);
            Assert.Equal(createUserDTO.Password, user.Password);
        }
    }
}
