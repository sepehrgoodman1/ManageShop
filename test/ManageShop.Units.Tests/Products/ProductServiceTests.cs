using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Unit;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Exception;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.Products.Exception;

namespace ManageShop.Units.Tests.Products
{
    public class ProductServiceTests : BusinessUnitTest
    {
        private readonly ProductService _sut;

        public ProductServiceTests()
        {
            _sut = ProductFactory.CreateService(SetupContext);
        }

        [Fact]
        public async Task Add_add_product()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            DbContext.Save(productGroup);
            var dto = ProductFactory.CreateAddDto(productGroup.Id, "شیر");

            await _sut.Add(dto);

            var result = ReadContext.Set<Product>().Single();
            result.Title.Should().Be(dto.Title);
            result.ProductGroupId.Should().Be(productGroup.Id);
            result.MinimumInventory.Should().Be(dto.MinimumInventory);
        }

        [Theory]
        [InlineData(0)]
        public async Task Add_add_product_productgroup_not_found_exception(int invalidId)
        {
            var dto = ProductFactory.CreateAddDto(invalidId, "شیر");

            var result = () => _sut.Add(dto);

            await result.Should().ThrowExactlyAsync<ProductGroupNotFoundException>();
            ReadContext.Set<Product>().Should().BeNullOrEmpty();

        }

        [Theory]
        [InlineData(-1 , 1)]
        [InlineData(1 , -1)]
        public async Task Add_add_product_invalid_price_minimuminventory_exception(
            int invalidPrice,int invalidMinimumInventory)
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            DbContext.Save(productGroup);
            var dto = ProductFactory.CreateAddDto(productGroup.Id, "شیر",
                invalidPrice, invalidMinimumInventory);

            var result = () => _sut.Add(dto);

            await result.Should().ThrowExactlyAsync<InvalidPriceOrMinimumInventoryException>();
            ReadContext.Set<Product>().Should().BeNullOrEmpty();

        }

        [Fact]
        public async Task Add_add_product_duplicate_name_in_group_exception()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product = ProductFactory.Create(productGroup, "شیر");
            DbContext.Save(product);
            var dto = ProductFactory.CreateAddDto(productGroup.Id, "شیر");

            var result = () => _sut.Add(dto);

            await result.Should().ThrowExactlyAsync<DuplicateProductNameInProductGroupException>();
            var actual = ReadContext.Set<Product>().Single();
            actual.Title.Should().Be(product.Title);
            actual.Id.Should().Be(product.Id);
            actual.MinimumInventory.Should().Be(product.MinimumInventory);

        }
    }
}
