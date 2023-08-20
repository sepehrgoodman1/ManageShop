using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.ProductPurchaseInvoices
{
    public class ProductPurchaseInvoicesEntityMap : IEntityTypeConfiguration<ProductPurchaseInvoice>
    {
        public void Configure(EntityTypeBuilder<ProductPurchaseInvoice> _)
        {
            _.ToTable("ProductPurchaseInvoices");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.ProductId).IsRequired();
            _.Property(_ => _.PurchaseInvoiceId).IsRequired();
            _.HasOne(_ => _.Products).WithMany(_ => _.ProductPurchaseInvoices).HasForeignKey(x => x.ProductId);
            _.HasOne(_ => _.PurchaseInvoices).WithMany(_ => _.ProductPurchaseInvoices).HasForeignKey(x => x.PurchaseInvoiceId);
        }
    }
}