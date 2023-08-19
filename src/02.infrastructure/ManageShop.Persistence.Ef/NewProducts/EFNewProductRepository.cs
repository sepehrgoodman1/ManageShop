using ManageShop.Entities.Entities;
using ManageShop.Persistence.Ef.Productss;
using ManageShop.Services.NewProducts.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Persistence.Ef.NewProducts
{
    public class EFNewProductRepository : NewProductRepository
    {
        private readonly DbSet<NewProduct> _newProducts;

        public EFNewProductRepository(EFDataContext context)
        {
            _newProducts = context.NewProducts;
        }

        public async Task Add(List<NewProduct> newProducts)
        {
            await _newProducts.AddRangeAsync(newProducts);
        }
    }
}
