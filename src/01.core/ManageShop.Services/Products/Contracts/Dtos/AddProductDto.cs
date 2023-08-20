namespace ManageShop.Services.Products.Contracts.Dtos
{
    public class AddProductDto
    {
        public int ProductGroupId { get; set; }
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
    }
}