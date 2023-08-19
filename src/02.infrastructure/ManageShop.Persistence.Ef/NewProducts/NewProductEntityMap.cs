using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Persistence.Ef.NewProducts
{
    public class NewProductEntityMap : IEntityTypeConfiguration<NewProduct>
    {
        public void Configure(EntityTypeBuilder<NewProduct> _)
        {
            _.ToTable("NewProducts");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            _.Property(_ => _.ProductCode).IsRequired();
            _.Property(_ => _.ProductTitle).IsRequired();
            _.Property(_ => _.ProductRecivedCount).IsRequired();
            _.HasOne(_ => _.PurchaseInvoice).WithMany(_ => _.NewProducts)
                .HasForeignKey(x => x.PurchaseInvoiceId);
        }
    }
}
