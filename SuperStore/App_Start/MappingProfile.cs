using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Models;
using SuperStore.ViewModel;
using SuperStore.Proc_ViewModel;
using SuperStore.Proc_Model;

namespace SuperStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<tblCategory, CategoryViewModel>();
            CreateMap<tblUser, UsersViewModel>();
            CreateMap<tblInterface, InterfaceViewModel>();
            CreateMap<tblProduct, ProductViewModel>();
            CreateMap<tblRole, RoleViewModel>();
            CreateMap<tblRolePermission, RolePermissionViewModel>();
            CreateMap<tblSubCategory, SubCategoryViewModel>();
            CreateMap<tblSupplier, SupplierViewModel>();
            CreateMap<tblTax, TaxViewModel>();
            CreateMap<Get_StockDetail, Get_StockDetailViewModel>();
            CreateMap<tblCustomer, CustomerViewModel>();
        }
    }
}