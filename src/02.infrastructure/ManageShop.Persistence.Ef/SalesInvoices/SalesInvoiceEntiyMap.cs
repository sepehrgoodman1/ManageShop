﻿using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.SalesInvoices
{
    public class SalesInvoiceEntiyMap : IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> _)
        {
            _.ToTable("SalesInvoices");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            _.Property(_ => _.TotalSales).IsRequired();
            _.Property(_ => _.ClientName).IsRequired();
            _.Property(_ => _.TotalProductCount).IsRequired();
            _.Property(_ => _.Date).IsRequired();
        }
    }
}
