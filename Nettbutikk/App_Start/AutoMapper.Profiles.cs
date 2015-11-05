using AutoMapper;
using Nettbutikk.Model;
using Nettbutikk.Models;

namespace Nettbutikk
{
    public static class AutoMapperProfiles
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<ProductsProfile>();
                config.AddProfile<CategoriesProfile>();
            });
        }

        private class CategoriesProfile : Profile
        {
            protected override void Configure()
            {
                Mapper.CreateMap<Category, CategoryView>()
                    .ForMember(
                        dest => dest.Name,
                        opts => opts.MapFrom(src => src.Name))
                    .ForMember(
                        dest => dest.Id,
                        opts => opts.MapFrom(src => src.CategoryId))
                    .ReverseMap();
                Mapper.CreateMap<Category, CreateCategory>()
                    .ReverseMap();
                Mapper.CreateMap<Category, EditCategory>()
                    .ReverseMap();
            }
        }

        private class ProductsProfile : Profile
        {
            protected override void Configure()
            {
                Mapper.CreateMap<Product, ProductView>()
                    .ReverseMap();
                Mapper.CreateMap<Product, CreateProduct>()
                    .ReverseMap();
                Mapper.CreateMap<Product, EditProduct>()
                    .ReverseMap();
            }
        }
    }
}