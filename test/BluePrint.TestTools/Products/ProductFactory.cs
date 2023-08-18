using ManageShop.Entities.Entities;
using ManageShop.Persistence.Ef.ProductGroups;
using ManageShop.Persistence.Ef;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using ManageShop.Services.ProductGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrint.TestTools.Products
{
    public static class ProductFactory
    {
        public static Product Create(ProductGroup productGroup, string title = "shir",
            int inventory = 12, int minimumInventory = 10,
            double price = 100 , ProductStatus productStatus = ProductStatus.Available
            )
        {
            return new Product()
            {
                ProductGroup = productGroup,
                Title = title,
                Inventory = inventory,
                Price = price,
                MinimumInventory = minimumInventory,
                Status = productStatus,
            };
        }
    /*    public static AddProductGroupDto CreateAddDto(string name = "dummy")
        {
            return new AddProductGroupDto()
            {
                Name = name,
            };
        }*/

        /*public static ProductGroupAppService CreateService(EFDataContext context)
        {
            var productGroupRepos = new EFProductGroupRepository(context);
            var unitOfWork = new EFUnitOfWork(context);

            return new ProductGroupAppService(productGroupRepos, unitOfWork);

        }*/
    }
}
