using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.Productss
{
    public class EFProductRepository : ProductRepository
    {
        private readonly DbSet<Product> _products;

        public EFProductRepository(EFDataContext context)
        {
            _products = context.Products;
        }

        public async Task Add(Product product)
        {
            await _products.AddAsync(product);
        }

        public  async Task<List<Product>> FindAllByIds(List<int> enumerable)
        {
            return await _products.Where(_ => enumerable.Contains(_.Id)).ToListAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _products.FindAsync(id);
        }

        public async Task<bool> HaveProduct(int productGroupId)
        {
            return await _products.AnyAsync(_=> _.ProductGroupId == productGroupId);
        }

        public async Task<bool> IsExistTitle(string title, int productGroupId)
        {
            return await _products.AnyAsync(_ => _.Title == title &&
                                            _.ProductGroupId == productGroupId);
        }

        public void Update(List<Product> products)
        {
            _products.UpdateRange(products);
        }
    }
}
