

using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
        }
        public EFDataContext(
          string connectionString)
          : this(
              new DbContextOptionsBuilder<EFDataContext>()
                  .UseSqlServer(connectionString).Options)
        {

        }
        public DbSet<AccountingDocument> AccountingDocuments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductPurchaseInvoice> ProductPurchaseInvoices { get; set; }
        public DbSet<ProductSalesInvoice> ProductSalesInvoices { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<NewProduct> NewProducts { get; set; }
    }
}
