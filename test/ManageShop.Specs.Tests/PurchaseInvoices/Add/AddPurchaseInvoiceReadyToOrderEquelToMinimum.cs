using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using BluePrint.TestTools.PurchaseInvoices;
using ManageShop.Entities.Entities;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using ManageShop.Services.PurchaseInvoices.Contracts;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace ManageShop.Specs.Tests.PurchaseInvoices.Add
{
    public class AddPurchaseInvoiceReadyToOrderEquelToMinimum : BusinessIntegrationTest
    {
        private ProductGroup productGroup;
        private Product product;
        private List<AddPurchaseInvoiceDto> dto;
        private readonly PurchaseInvoiceService _sut;

        public AddPurchaseInvoiceReadyToOrderEquelToMinimum()
        {
            _sut = PurchaseInvoicesFactory.CreateService(SetupContext);
        }

        //User Story
        [Feature(
            AsA = "من به عنوان مسئول انبار",
            IWantTo = "می خواهم فاکتور ورود کالا ها به انبار را ثبت کنم",
            InOrderTo = "تا بتوانم انبار کالا ها را مدیریت کنم"
            )
     ]

        // scenario
        [Scenario("ثبت ورود کالا وقتی که وضعیت کالا اماده سفارش باشد برای حالتی که حداقل موجودی با موجودی برابر باشد")]

        [Given(" یک کالا با کد کالا 1 و تعداد موجودی 5 عدد و حداقل موجودی 10 و وضعیت اماده سفارش در فهرست کالاها وجود دارد")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            product = ProductFactory.Create(productGroup, "شیر", inventory: 5,
                minimumInventory: 10, productStatus: ProductStatus.ReadyToOrder);
            DbContext.Save(product);
        }

        [When(" پنج عدد کالا با کد 1 را در تاریخ 08/05/1402 به فهرست کالاهای با کد 1 اضافه می کنم")]
        private async Task When()
        {

            dto = PurchaseInvoicesFactory.CreateAddDto(product.Id, 5);

            await _sut.Add(dto);
        }

        [Then(" باید 10 عدد کالا با کد 1 و تاریخ 08/05/1402 را در فهرست کالاها داشته باشد")]
        [And(" وضعیت کالا با کد 1 اماده سفارش باشد")]
        private void Then()
        {
            var actual = ReadContext.Set<PurchaseInvoice>().Include(_ => _.ProductPurchaseInvoices)
                .ThenInclude(_ => _.Products).Single();

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Id.Should().Be(product.Id);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First()
                .Inventory.Should().Be(product.Inventory + dto.First().ProductRecivedCount);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Status.Should().Be(ProductStatus.ReadyToOrder);

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
