namespace ManageShop.Entities.Entities
{
    public class ProductPurchaseInvoice
    {
        public ProductPurchaseInvoice()
        {
            Products = new();
            PurchaseInvoices = new();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoices { get; set; }
    }
}
