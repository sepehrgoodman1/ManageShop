using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Persistence.Ef.Productss
{
    public class ProductEntityMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> _)
        {
            _.ToTable("Products");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            _.Property(_ => _.Title).IsRequired();
            _.Property(_ => _.MinimumInventory).IsRequired();
            _.Property(_ => _.Status).IsRequired();
            _.HasOne(_ => _.ProductGroup).WithMany(_ => _.Products).HasForeignKey(x => x.ProductGroupId);
        }
    }
}
