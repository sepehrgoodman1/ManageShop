namespace ManageShop.Services.SalesInvoices.Contracts.Dtos
{
    public class AddSaleInvoiceDto
    {
        public int ProductCode { get; set; }
        public int ProductCount { get; set; }
        public double UnitPrice { get; set; }
    }
}
