using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Unit;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using BluePrint.TestTools.PurchaseInvoices;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Specs.Tests.PurchaseInvoices.Add
{
    public class AddPurchaseInvoiceAvailable : BusinessIntegrationTest
    {
        private ProductGroup productGroup;
        private Product product;
        private List<AddPurchaseInvoiceDto> dto;
        private readonly PurchaseInvoiceService _sut;

        public AddPurchaseInvoiceAvailable()
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
        [Scenario("ثبت ورود کالا وقتی که وضعیت کالا موجود باشد")]

        [Given(" یک کالا با کد کالا 1 و تعداد موجودی 7 عدد و حداقل موجودی 5 و وضعیت موجود در فهرست کالاها وجود دارد")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            product = ProductFactory.Create(productGroup, "شیر", inventory: 7,
                minimumInventory: 5, productStatus: ProductStatus.Available);
            DbContext.Save(product);
        }

        [When("بیست عدد کالا با کد 1 را در تاریخ 1402/05/08 به فهرست کالاهای با کد 1 اضافه می کنم")]
        private async Task When()
        {
            
            dto = PurchaseInvoicesFactory.CreateAddDto(product.Id, 20);

            await _sut.Add(dto);
        }

        [Then(" باید 27 عدد کالا با کد 1 و تاریخ 05/08 /1402 را در فهرست کالاها داشته باشد")]
        [And(" وضعیت کالا با کد 1 موجود باشد")]
        private void Then()
        {
            var actual = ReadContext.Set<PurchaseInvoice>().Include(_ => _.ProductPurchaseInvoices)
                .ThenInclude(_ => _.Products).Single();

            actual.ProductPurchaseInvoices.Select(_=>_.Products).First().Id.Should().Be(product.Id);

            actual.ProductPurchaseInvoices.Select(_=>_.Products).First()
                .Inventory.Should().Be(product.Inventory + dto.First().ProductRecivedCount);

            actual.ProductPurchaseInvoices.Select(_ => _.Products).First().Status.Should().Be(ProductStatus.Available);

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
