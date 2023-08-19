using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrint.TestTools.NewProducts
{
    public static class NewProductsFactory
    {
        public static NewProduct Create()
        {
            return new NewProduct
            {
                
            };
        }
       /* public static AddNewProductDto CreateAddDto()
        {
            return new AddNewProductDto()
            {
                ProductGroupId = productGroupId,
                Title = title,
                Price = price,
                MinimumInventory = minimumInventory,
            };
        }*/
    }
}
