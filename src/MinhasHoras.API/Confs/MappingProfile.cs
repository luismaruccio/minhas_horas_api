using AutoMapper;
using MinhasHoras.API.DTOs.Users;
using MinhasHoras.Domain.Entities;

namespace MinhasHoras.API.Confs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
