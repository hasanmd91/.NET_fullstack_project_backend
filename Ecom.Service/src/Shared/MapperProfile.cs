using AutoMapper;
using Ecom.Core.src.Entity;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Shared
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<CategoryReadDTO, Category>();
            CreateMap<Category, CategoryReadDTO>();
            CreateMap<UserUpdateDTO, Category>();
        }



    }
}