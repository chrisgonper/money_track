using AutoMapper;
using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Utilities
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionViewModel, TransactionModel>();
            CreateMap<CategoryDisplayModel, CategoryModel>();
        }
    }
}
