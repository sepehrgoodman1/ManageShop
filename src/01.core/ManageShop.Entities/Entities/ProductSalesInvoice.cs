namespace ManageShop.Entities.Entities
{
    public class ProductSalesInvoice
    {
       
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SalesInvoicesId { get; set; }
        public SalesInvoice SalesInvoice { get; set; }
    }
}
