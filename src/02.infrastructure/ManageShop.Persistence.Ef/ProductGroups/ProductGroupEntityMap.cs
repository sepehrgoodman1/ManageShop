using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.ProductGroups
{
    public class ProductGroupEntityMap : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> _)
        {
            _.ToTable("ProductGroups");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            _.Property(_ => _.Name).IsRequired();
        }
    }
}
