using ManageShop.Entities.Entities;

namespace ManageShop.Services.Products.Contracts.Dtos
{
    public class GetProductDto
    {
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
        public int Inventory { get; set; } = 0;
        public ProductStatus Status { get; set; }
        public int ProductGroupId { get; set; }
    }
}
