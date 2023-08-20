using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.PurchaseInvoics
{
    public class PurchaseInvoiceEntityMap : IEntityTypeConfiguration<PurchaseInvoice>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoice> _)
        {
            _.ToTable("PurchaseInvoices");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            _.Property(_ => _.Date).IsRequired();
        }
    }
}
