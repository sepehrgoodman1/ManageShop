﻿using ManageShop.Entities.Entities;
using ManageShop.Persistence.Ef.ProductGroups;
using ManageShop.Persistence.Ef;
using ManageShop.Persistence.Ef.Productss;
using ManageShop.Services.Products.Contracts.Dtos;
using ManageShop.Services.Products;

namespace BluePrint.TestTools.Products
{
    public static class ProductFactory
    {
        public static Product Create(ProductGroup productGroup,
                                     string title = "shir",
                                     int inventory = 0,
                                     int minimumInventory = 10,
                                     ProductStatus productStatus = ProductStatus.Available
            )
        {
            return new Product()
            {
                ProductGroup = productGroup,
                Title = title,
                Inventory = inventory,
                MinimumInventory = minimumInventory,
                Status = productStatus,
            };
        }
        public static AddProductDto CreateAddDto(int productGroupId
             , string title = "dummy ",
            int minimumInventory = 10)
        {
            return new AddProductDto()
            {
                ProductGroupId = productGroupId,
                Title = title,
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
