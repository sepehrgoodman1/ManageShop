namespace ManageShop.Entities.Entities
{
    public class SalesInvoice
    {
        public SalesInvoice()
        {
            ProductSalesInvoices = new();
        }
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public double TotalSales { get; set; }
        public int TotalProductCount { get; set; }
        public AccountingDocument AccountingDocument { get; set; }
        public HashSet<ProductSalesInvoice> ProductSalesInvoices { get; set; }

    }
}
