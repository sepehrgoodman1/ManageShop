using ManageShop.Persistence.Ef.ProductGroups;
using ManageShop.Persistence.Ef;
using ManageShop.Services.ProductGroups;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageShop.Entities.Entities;
using ManageShop.Persistence.Ef.Productss;

namespace BluePrint.TestTools.ProductGroups
{
    public static class ProductGroupFactory
    {
        public static ProductGroup Create( string name = "dummy")
        {
            return new ProductGroup()
            {
                Name = name,
            };
        }
        public static AddProductGroupDto CreateAddDto(string name = "dummy")
        {
            return new AddProductGroupDto()
            {
                Name = name,
            };
        }

        public static ProductGroupAppService CreateService(EFDataContext context)
        {
            var productGroupRepos = new EFProductGroupRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            var productRepos = new EFProductRepository(context);

            return new ProductGroupAppService(productGroupRepos, unitOfWork, productRepos);
          
        }
    }
}
