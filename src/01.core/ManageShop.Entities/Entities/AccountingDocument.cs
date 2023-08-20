namespace ManageShop.Entities.Entities
{
    public class AccountingDocument
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoice { get; set; }
        public double TotalPrice { get; set; }
    }
}
