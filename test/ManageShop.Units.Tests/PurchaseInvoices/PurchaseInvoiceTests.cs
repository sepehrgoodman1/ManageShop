using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Unit;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using BluePrint.TestTools.PurchaseInvoices;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Exception;
using ManageShop.Services.PurchaseInvoices.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Units.Tests.PurchaseInvoices
{
    public class PurchaseInvoiceTests : BusinessUnitTest
    {
        private readonly PurchaseInvoiceService _sut;

        public PurchaseInvoiceTests()
        {
            _sut = PurchaseInvoicesFactory.CreateService(SetupContext);
        }


        [Fact]
        public async Task Add_add_a_purachase_invoice()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product = ProductFactory.Create(productGroup, "شیر",
                inventory: 5, minimumInventory: 5, productStatus: ProductStatus.ReadyToOrder);
            DbContext.Save(product);
            var dto = PurchaseInvoicesFactory.CreateAddDto(product.Id, 20);

            await _sut.Add(dto);

            var actual = ReadContext.Set<PurchaseInvoice>().Include(_ => _.ProductPurchaseInvoices)
                .ThenInclude(_ => _.Products).Single();

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Id
                .Should().Be(product.Id);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First()
                .Inventory.Should().Be(product.Inventory + dto.First().ProductRecivedCount);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Status.Should()
                .Be(ProductStatus.Available);
        }

        [Theory]
        [InlineData(0)]
        public async Task Add_add_a_purachase_invoice_invalid_product_code_exception(int invalidId)
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product = ProductFactory.Create(productGroup, "شیر", inventory: 5, minimumInventory: 5);
            DbContext.Save(product);
            var dto = PurchaseInvoicesFactory.CreateAddDto(invalidId, 20);

            var _expected = ()=> _sut.Add(dto);

            await _expected.Should().ThrowExactlyAsync<InvalidProductCodeException>();
            var actualProduct = ReadContext.Set<Product>().Single();
            actualProduct.Inventory.Should().Be(product.Inventory);
            actualProduct.Status.Should().Be(product.Status);
           
        }

        [Fact]
        public async Task Add_add_a_purachase_invoice_ready_to_order_status()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product = ProductFactory.Create(productGroup, "شیر",
                inventory: 2, minimumInventory: 10, productStatus: ProductStatus.ReadyToOrder);
            DbContext.Save(product);
            var dto = PurchaseInvoicesFactory.CreateAddDto(product.Id, 5);

            await _sut.Add(dto);

            var actual = ReadContext.Set<PurchaseInvoice>().Include(_ => _.ProductPurchaseInvoices)
                .ThenInclude(_ => _.Products).Single();

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Id
                .Should().Be(product.Id);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First()
                .Inventory.Should().Be(product.Inventory + dto.First().ProductRecivedCount);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Status.Should()
                .Be(ProductStatus.ReadyToOrder);
        }
        [Fact]
        public async Task Add_add_a_purachase_invoice_ready_to_order_status_equel_minimum()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product = ProductFactory.Create(productGroup, "شیر",
                inventory: 5, minimumInventory: 10, productStatus: ProductStatus.ReadyToOrder);
            DbContext.Save(product);
            var dto = PurchaseInvoicesFactory.CreateAddDto(product.Id, 5);

            await _sut.Add(dto);

            var actual = ReadContext.Set<PurchaseInvoice>().Include(_ => _.ProductPurchaseInvoices)
                .ThenInclude(_ => _.Products).Single();

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Id
                .Should().Be(product.Id);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First()
                .Inventory.Should().Be(product.Inventory + dto.First().ProductRecivedCount);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Status.Should()
                .Be(ProductStatus.ReadyToOrder);
        }

        [Fact]
        public async Task Add_add_a_purachase_invoice_ready_to_order_status_unavailable()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product = ProductFactory.Create(productGroup, "شیر",
                inventory: 0, minimumInventory: 10);
            DbContext.Save(product);
            var dto = PurchaseInvoicesFactory.CreateAddDto(product.Id, 0);

            await _sut.Add(dto);

            var actual = ReadContext.Set<PurchaseInvoice>().Include(_ => _.ProductPurchaseInvoices)
                .ThenInclude(_ => _.Products).Single();

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Id
                .Should().Be(product.Id);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First()
                .Inventory.Should().Be(product.Inventory + dto.First().ProductRecivedCount);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Status.Should()
                .Be(ProductStatus.UnAvailable);
        }
    }
}
