namespace ManageShop.Entities.Entities
{
    public class PurchaseInvoice //رسید خرید و ورود به انبار
    {
        public PurchaseInvoice()
        {
            ProductPurchaseInvoices = new();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public HashSet<ProductPurchaseInvoice> ProductPurchaseInvoices { get; set; }

    }
   
}
