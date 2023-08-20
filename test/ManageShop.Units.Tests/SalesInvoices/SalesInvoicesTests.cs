using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Unit;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using BluePrint.TestTools.SaleInvoices;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.SalesInvoices.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var dto = SaleInvoiceFactory.CreateAddDto(product1.Id, 20, 100, product2.Id, 10, 50);

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
            var dto = SaleInvoiceFactory.CreateAddDto(product1.Id, 20, 100, product2.Id, 10, 50);

            await _sut.Add("سپهر", dto);

            var actual = ReadContext.Set<Product>();
            actual.Find(product1.Id).Inventory.Should()
                .Be(10);
            actual.Find(product2.Id).Inventory.Should()
               .Be(25);
        }

        [Fact]
        public async Task Add_add_salesinvoice_test_account_document()
        {
            var productGroup = ProductGroupFactory.Create("لبنیات");
            var product1 = ProductFactory.Create(productGroup, "شیر", inventory: 30, minimumInventory: 5);
            var product2 = ProductFactory.Create(productGroup, "ماست", inventory: 35, minimumInventory: 5);
            DbContext.SaveRange(product1, product2);
            var dto = SaleInvoiceFactory.CreateAddDto(product1.Id, 20, 100, product2.Id, 10, 50);

            await _sut.Add("سپهر", dto);

            var actual = ReadContext.Set<AccountingDocument>().Include(_=>_.SalesInvoice).Single();
            var actualSaleInvoice = ReadContext.Set<SalesInvoice>().Single();
            actual.SalesInvoice.Id.Should().Be(actualSaleInvoice.Id);
            actual.TotalPrice.Should().Be(dto.Sum(_ => _.UnitPrice * _.ProductCount));

            // Date
        }


    }
}
