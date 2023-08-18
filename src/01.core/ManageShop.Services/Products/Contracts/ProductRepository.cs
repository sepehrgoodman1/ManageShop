namespace ManageShop.Services.Products.Contracts
{
    public interface ProductRepository
    {
        Task<bool> HaveProduct(int productGroupId);
    }
}
