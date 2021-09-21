using API.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember("ProductBrand", opt => opt.MapFrom(p => p.ProductBrand.Name))
                .ForMember("ProductType", opt => opt.MapFrom(p => p.ProductType.Name))
                .ForMember(d=>d.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
        }


    }
}
