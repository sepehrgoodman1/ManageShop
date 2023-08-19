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
using ManageShop.Persistence.Ef.Productss;
using ManageShop.Services.Products.Contracts.Dtos;
using ManageShop.Services.Products;

namespace BluePrint.TestTools.Products
{
    public static class ProductFactory
    {
        public static Product Create(ProductGroup productGroup, string title = "shir",
            int inventory = 0, int minimumInventory = 10,
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
        public static AddProductDto CreateAddDto(int productGroupId
             , string title = "dummy ", double price = 100 ,
            int minimumInventory = 10)
        {
            return new AddProductDto()
            {
                ProductGroupId = productGroupId,
                Title = title,
                Price = price,
                MinimumInventory = minimumInventory,
            };
        }

        public static ProductAppService CreateService(EFDataContext context)
        {
            var productRepos = new EFProductRepository(context);
            var productGroupRepos = new EFProductGroupRepository(context);
            var unitOfWork = new EFUnitOfWork(context);

            return new ProductAppService(productRepos, unitOfWork, productGroupRepos);

        }
    }
}
