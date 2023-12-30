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
            CreateMap<UserUpdateDTO, User>()
            .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));


            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>()
            .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));


            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>()
            .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));


            CreateMap<Review, ReviewReadDTO>();
            CreateMap<ReviewCreateDTO, Review>();
            CreateMap<ReviewUpdateDTO, Review>()
            .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));


            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderCreateDTO, Order>();

        }
    }
}