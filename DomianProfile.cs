using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingmachineCore.Model;
using VendingmachineInfastruction.Entity;
using VendingmachineInfastruction.Extend;

namespace VendingmachineCore.Mapper
{
    public class DomianProfile :Profile
    {
        public DomianProfile()
        {
            CreateMap<Product, ProductVm>().ReverseMap();
            //CreateMap<ProductVm, Product>().ReverseMap()
            //    .ForPath(dest => dest.SellerID, source => source.MapFrom(a => a.SellerID))
            //    .ForPath(dest => dest.ProductName, source => source.MapFrom(a => a.ProductName))
            //    .ForPath(dest => dest.AmountAvailable, source => source.MapFrom(a => a.AmountAvailable))
            //    .ForPath(dest => dest.Cost, source => source.MapFrom(a => a.Cost));


            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<AllProductUsers, AllProductUsersVm>().ReverseMap();



            CreateMap<RegisterVm, ApplicationUser>().ReverseMap()
                .ForPath(dest => dest.UserName, source => source.MapFrom(a => a.UserName))
                .ForPath(dest => dest.Email, source => source.MapFrom(a => a.Email))
                .ForPath(dest => dest.IsAgree, source => source.MapFrom(a => a.IsAgree));


        }
    }
}
