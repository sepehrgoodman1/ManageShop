using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using ManageShop.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using ManageShop.Services.SalesInvoices.Contracts;
using ManageShop.Services.SalesInvoices.Contracts.Dtos;
using BluePrint.TestTools.SaleInvoices;

namespace ManageShop.Specs.Tests.SaleInvoices.Add
{
    public class AddSaleInvoice : BusinessIntegrationTest
    {
        private ProductGroup productGroup;
        private Product product1;
        private Product product2;
        private List<AddSaleInvoiceDto> dto;
        private readonly SaleInvoiceService _sut;

        public AddSaleInvoice()
        {
            _sut = SaleInvoiceFactory.CreateService(SetupContext);
        }

     

        //User Story
        [Feature(
            AsA = "من به عنوان مسئول فروشکاه",
            IWantTo = "می خواهم فاکتور فروش کالا ها به مشتری را ثبت کنم",
            InOrderTo = "تا بتوانم فروش کالا ها را مدیریت کنم"
            )
     ]

        // scenario
        [Scenario("ثبت فروش کالا")]

        [Given("کالا با کد 1 و تعداد 30 عدد  و کالا با کد 2 و تعداد 35 عدد در فهرست کالاها وجود دارد")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            product1 = ProductFactory.Create(productGroup, "شیر", inventory: 30, minimumInventory: 5);

            product2 = ProductFactory.Create(productGroup, "ماست", inventory: 35, minimumInventory: 5);
            DbContext.SaveRange(product1, product2);
        }

        [When("مشتری با نام 'سپهر' کالا با کد 1 و تعداد 20 عدد را در تاریخ 08/05/1402 با قیمت هر واحد 100 هزار تومن و" +
            " کالا با کد 2 و تعداد 10 عدد را در تاریخ 08/05/1402 با قیمت هر واحد 50 هزار تومن  خریداری میکند")]
        private async Task When()
        {
            dto = SaleInvoiceFactory.CreateAddDtoTwo(product1.Id, 20, 100, product2.Id, 10, 50);

            await _sut.Add("سپهر", dto);
        
        }

        [Then("باید فاکتور فروش با کد و نام مشتری 'سپهر' و کد کالا 12 و تعداد مجموع 30 و در تاریخ 08/05/1402 با قیمت  مجموع 2.5 میلبون تومان ثبت شود")]
        [And(" موجودی کالا با کد 1 برابر با 10 و موجودی کالا با کد 2 برابر با 25 شود")]
        [And(" یک سند حسابداری با تاریخ 08/05/1402 و مبلغ 2.5 میلیون تومان و کد فاکتور فروش  ثبت شود")]
        private void Then()
        {
            var actualSaleInvoice = ReadContext.Set<SalesInvoice>().Single();
            actualSaleInvoice.ClientName.Should().Be("سپهر");
            actualSaleInvoice.TotalProductCount.Should().Be(dto.Sum(_=> _.ProductCount));
            actualSaleInvoice.TotalSales.Should().Be(dto.Sum(_=> _.UnitPrice * _.ProductCount));

            var actualProduct = ReadContext.Set<Product>();
            actualProduct.Find(product1.Id).Inventory.Should().Be(10);
            actualProduct.Find(product2.Id).Inventory.Should().Be(25);

            var actualDocument = ReadContext.Set<AccountingDocument>().Include(_ => _.SalesInvoice).Single();
            actualDocument.SalesInvoice.Id.Should().Be(actualSaleInvoice.Id);
            actualDocument.TotalPrice.Should().Be(dto.Sum(_ => _.UnitPrice * _.ProductCount));

            // Date
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then());
        }
    }
}
