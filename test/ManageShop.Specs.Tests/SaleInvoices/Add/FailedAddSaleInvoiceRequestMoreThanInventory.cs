using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using BluePrint.TestTools.SaleInvoices;
using ManageShop.Entities.Entities;
using ManageShop.Services.SalesInvoices.Contracts.Dtos;
using ManageShop.Services.SalesInvoices.Contracts;
using FluentAssertions;
using ManageShop.Services.SalesInvoices.Exception;

namespace ManageShop.Specs.Tests.SaleInvoices.Add
{
    public class FailedAddSaleInvoiceRequestMoreThanInventory :BusinessIntegrationTest
    {
        private ProductGroup productGroup;
        private Product product1;
        private Product product2;
        private List<AddSaleInvoiceDto> dto;
        private Func<Task> _expedted;
        private readonly SaleInvoiceService _sut;
        public FailedAddSaleInvoiceRequestMoreThanInventory()
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
        [Scenario("ثبت فروش کالا با تعداد بالاتر از موجودی کالا")]

        [Given("کالا با کد 1 و تعداد 10 عدد در فهرست کالاها وجود دارد")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            product1 = ProductFactory.Create(productGroup, "شیر", inventory: 10, minimumInventory: 5);
            DbContext.Save(product1);
        }

        [When("مشتری با نام 'سپهر' کالا با کد 1 و تعداد 20 عدد را در تاریخ 08/05/1402 به قیمت 100 هزار تومن خریداری میکند")]
        private void When()
        {
            dto = SaleInvoiceFactory.CreateAddDtoOne(product1.Id, 20, 100);

            _expedted = ()=> _sut.Add("سپهر", dto);

        }

        [Then("باید خطای تعداد غیر مجاز خرید کالا رخ دهد")]
        [And("فاکتور فروش ثبت نشده باشد")]
        [And("سند حسابداری برای خرید ثبت نشده باشد")]
        [And("کد کالای 1 دارای موجودی 20 عدد باشد")]
        private async Task Then()
        {
            await _expedted.Should().ThrowExactlyAsync<UnauthorizedNumberOfProductsPurchasedException>();

            ReadContext.Set<SalesInvoice>().Should().BeNullOrEmpty();

            ReadContext.Set<AccountingDocument>().Should().BeNullOrEmpty();

            ReadContext.Set<Product>().Single().Inventory.Should().Be(product1.Inventory);

            // Date
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then().Wait());
        }
    }
}
