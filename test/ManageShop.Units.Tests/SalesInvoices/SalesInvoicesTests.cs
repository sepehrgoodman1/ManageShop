using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Unit;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using BluePrint.TestTools.SaleInvoices;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.SalesInvoices.Contracts;
using ManageShop.Services.SalesInvoices.Exception;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Units.Tests.SalesInvoices
{
    public class SalesInvoicesTests : BusinessUnitTest
    {
        private readonly SaleInvoiceService _sut;
        public SalesInvoicesTests()
        {
            _sut = SaleInvoiceFactory.CreateService(SetupContext);
        }

        [Fact]
        public async Task Add_add_salesinvoice_test_salesinvoice()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 30, minimumInventory: 5);
            var product2 = ProductFactory.Create(productGroup, "ماست", inventory: 35, minimumInventory: 5);
            DbContext.SaveRange(product1, product2);
            var dto = SaleInvoiceFactory.CreateAddDtoTwo(product1.Id, 20, 100, product2.Id, 10, 50);

            await _sut.Add("سپهر", dto);

            var actual = ReadContext.Set<SalesInvoice>().Single();
            actual.ClientName.Should().Be("سپهر");
            actual.TotalProductCount.Should().Be(dto.Sum(_ => _.ProductCount));
            actual.TotalSales.Should().Be(dto.Sum(_ => _.UnitPrice * _.ProductCount));
        }

        [Fact]
        public async Task Add_add_salesinvoice_test_products()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 30, minimumInventory: 5);
            var product2 = ProductFactory.Create(productGroup, "ماست", inventory: 35, minimumInventory: 5);
            DbContext.SaveRange(product1, product2);
            var dto = SaleInvoiceFactory.CreateAddDtoTwo(product1.Id, 20, 100, product2.Id, 10, 50);

            await _sut.Add("سپهر", dto);

            var actual = ReadContext.Set<Product>();
            actual.Find(product1.Id).Inventory.Should().Be(10);
            actual.Find(product2.Id).Inventory.Should().Be(25);
        }

        [Fact]
        public async Task Add_add_salesinvoice_test_account_document()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 30, minimumInventory: 5);
            var product2 = ProductFactory.Create(productGroup, "ماست", inventory: 35, minimumInventory: 5);
            DbContext.SaveRange(product1, product2);
            var dto = SaleInvoiceFactory.CreateAddDtoTwo(product1.Id, 20, 100, product2.Id, 10, 50);

            await _sut.Add("سپهر", dto);

            var actual = ReadContext.Set<AccountingDocument>().Include(_=>_.SalesInvoice).Single();
            var actualSaleInvoice = ReadContext.Set<SalesInvoice>().Single();
            actual.SalesInvoice.Id.Should().Be(actualSaleInvoice.Id);
            actual.TotalPrice.Should().Be(dto.Sum(_ => _.UnitPrice * _.ProductCount));

            // Date
        }

        [Fact]
        public async Task Add_add_salesinvoice_value_more_than_inventory_exception_test_saleinvoice()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 10, minimumInventory: 5);
            DbContext.Save(product1);
            var dto = SaleInvoiceFactory.CreateAddDtoOne(product1.Id, 20, 100);

            var _expedted = () => _sut.Add("سپهر", dto);

            await _expedted.Should().ThrowExactlyAsync<UnauthorizedNumberOfProductsPurchasedException>();
            ReadContext.Set<SalesInvoice>().Should().BeNullOrEmpty();
        }


        [Fact]
        public async Task Add_add_salesinvoice_value_more_than_inventory_exception_test_document()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 10, minimumInventory: 5);
            DbContext.Save(product1);
            var dto = SaleInvoiceFactory.CreateAddDtoOne(product1.Id, 20, 100);

            var _expedted = () => _sut.Add("سپهر", dto);

            await _expedted.Should().ThrowExactlyAsync<UnauthorizedNumberOfProductsPurchasedException>();
            ReadContext.Set<AccountingDocument>().Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Add_add_salesinvoice_value_more_than_inventory_exception_test_product_inventory()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 10, minimumInventory: 5);
            DbContext.Save(product1);
            var dto = SaleInvoiceFactory.CreateAddDtoOne(product1.Id, 20, 100);

            var _expedted = () => _sut.Add("سپهر", dto);

            await _expedted.Should().ThrowExactlyAsync<UnauthorizedNumberOfProductsPurchasedException>();
            ReadContext.Set<Product>().Single().Inventory.Should().Be(product1.Inventory);
        }


    }
}
