using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Unit;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Units.Tests.ProductGroups
{
    public class ProductGroupTest : BusinessUnitTest
    {

        private readonly ProductGroupService _sut;

        public ProductGroupTest()
        {
            _sut = ProductGroupFactory.CreateService(SetupContext);
        }

        [Fact]
        public async Task Add_add_a_productgroup()
        {
            // Arrange
            var dto = ProductGroupFactory.CreateAddDto();

            // Act
            var productGroupId = await _sut.Add(dto);

            // Assert
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Id.Should().Be(productGroupId);
            result.Name.Should().Be(dto.Name);
        }


        [Fact]
        public async Task Add_add_productgroup_duplicate_name_exception()
        {
            // Arrange
            var productGroup = ProductGroupFactory.Create("Salam");
            DbContext.Save(productGroup);
            var dto = ProductGroupFactory.CreateAddDto("Salam");

            // Act
            var _expected = ()=> _sut.Add(dto);

            // Assert
            await _expected.Should().ThrowExactlyAsync<DuplicateProductGroupNameException>();
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Name.Should().Be(productGroup.Name);
            result.Id.Should().Be(productGroup.Id);
        }

        [Fact]
        public async Task Delete_delete_a_productgroup()
        {
            // Arrange
            var productGroupLabaniat = ProductGroupFactory.Create("Labaniat");
            var productGroupNoshidani = ProductGroupFactory.Create("Noshidani");
            DbContext.SaveRange(productGroupLabaniat, productGroupNoshidani);

            // Act
             await _sut.Delete(productGroupLabaniat.Id);

            // Assert
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Id.Should().Be(productGroupNoshidani.Id);
            result.Name.Should().Be(productGroupNoshidani.Name);
        }

        [Fact]
        public async Task Delete_delete_productgroup_when_have_product_exception()
        {
            // Arrange
            var productGroup = ProductGroupFactory.Create("Labaniat");
            var product = ProductFactory.Create(productGroup , "shir");
            DbContext.Save(product);

            // Act
            var _expected = () => _sut.Delete(productGroup.Id);

            // Assert
            await _expected.Should().ThrowExactlyAsync<ProductGroupHaveProductException>();
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Name.Should().Be(productGroup.Name);
            result.Id.Should().Be(productGroup.Id);
        }

        [Theory]
        [InlineData(0)]
        public async Task Delete_delete_productgroup_not_found_exception(int invalidId)
        {
            // Arrange
            var productGroup = ProductGroupFactory.Create("Labaniat");
            DbContext.Save(productGroup);

            // Act
            var _expected = () => _sut.Delete(invalidId);

            // Assert
            await _expected.Should().ThrowExactlyAsync<ProductGroupNotFoundException>();
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Name.Should().Be(productGroup.Name);
            result.Id.Should().Be(productGroup.Id);
        }





    }
}
