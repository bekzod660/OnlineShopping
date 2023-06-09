using Application.DTOs.Brand;
using Application.DTOs.Category;
using Application.DTOs.Permission;
using Application.DTOs.Product;
using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entites.IdentityEntities;
using Domain.Model;
using Domain.Models;

namespace Application.Mappings
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<UserGetDTO, User>();
            CreateMap<UserCreateDTO, User>()
                .ForMember(d => d.Roles, op => op.MapFrom(src => src.Roles.Select(id => new Role { Id = id })));
            ;
            CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserGetDTO>().ForMember(d => d.Id, op => op.MapFrom(src => src.Id));

            CreateMap<BrandCreateDTO, Brand>();
            CreateMap<BrandGetDTO, Brand>();
            CreateMap<Brand, BrandGetDTO>();

            CreateMap<CategoryGetDTO, Category>();
            CreateMap<Category, CategoryGetDTO>();
            CreateMap<CategoryCreateDTO, Category>();

            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductGetDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<RoleUpdateDTO, Role>()
             .ForMember(x => x.Name, t => t.MapFrom(s => s.Name))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
             .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(id => new Permission { Id = id }).ToList()));


            CreateMap<Role, RoleGetDTO>()
               .ForMember(x => x.Name, t => t.MapFrom(s => s.Name))
               .ForMember(x => x.RoleId, t => t.MapFrom(d => d.Id))
               .ForMember(x => x.Permissions, t => t.MapFrom(k => k.Permissions.Select(s => s.Id).ToList()));


            CreateMap<RoleCreateDTO, Role>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(id => new Permission() { Id = id }).ToList()))
                .ForMember(d => d.Name, o => o.MapFrom(src => src.Name));


            CreateMap<PermissionCreateDTO, Permission>();
            CreateMap<PermissionUpdateDTO, Permission>();
            CreateMap<PermissionGetDTO, Permission>();
        }
    }

}
