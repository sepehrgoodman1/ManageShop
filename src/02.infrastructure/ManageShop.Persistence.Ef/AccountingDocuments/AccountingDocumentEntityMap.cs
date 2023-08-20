using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageShop.Persistence.Ef.AccountingDocuments
{
    public class ProductGroupEntityMap :IEntityTypeConfiguration<AccountingDocument>
    {
        public void Configure(EntityTypeBuilder<AccountingDocument> _)
        {
            _.ToTable("AccountingDocuments");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            _.Property(_ => _.TotalPrice).IsRequired();
            _.HasOne(_ => _.SalesInvoice).WithOne(_ => _.AccountingDocument).
                HasForeignKey<AccountingDocument>(_ => _.SalesInvoiceId);

            _.Property(_ => _.SalesInvoiceId).IsRequired();
        }
    }
}
