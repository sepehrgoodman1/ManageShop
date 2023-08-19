using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _.Property(_ => _.NewProducts).IsRequired();
        }
    }
}
