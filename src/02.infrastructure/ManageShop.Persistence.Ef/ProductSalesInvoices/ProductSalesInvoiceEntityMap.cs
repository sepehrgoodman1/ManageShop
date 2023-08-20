using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.ProductSalesInvoices
{
    public class ProductSalesInvoiceEntityMap : IEntityTypeConfiguration<ProductSalesInvoice>
    {
        public void Configure(EntityTypeBuilder<ProductSalesInvoice> _)
        {
            _.ToTable("ProductSalesInvoices");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.ProductId).IsRequired();
            _.Property(_ => _.SalesInvoicesId).IsRequired();
            _.HasOne(_ => _.SalesInvoice).WithMany(_ => _.ProductSalesInvoices).HasForeignKey(x => x.SalesInvoicesId);
            _.HasOne(_ => _.Product).WithMany(_ => _.ProductSalesInvoices).HasForeignKey(x => x.ProductId);
        }
    }
}
