using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using ManageShop.Entities.Entities;
using BluePrint.TestTools.Products;
using FluentAssertions;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.Products.Contracts.Dtos;
using BluePrint.TestTools.Infrastructure.DataBaseConfig;

namespace ManageShop.Specs.Tests.Products.Add
{
    public class AddProduct : BusinessIntegrationTest
    {
        private readonly ProductService _sut;
        private AddProductDto dto;
        private ProductGroup productGroup;

        public AddProduct()
        {
            _sut = ProductFactory.CreateService(SetupContext);
        }

        //User Story
        [Feature(
            AsA = "من به عنوان مسئول فروشگاه",
            IWantTo = "می خواهم ورود کالاها به گروه کالاها را ثبت کنم",
            InOrderTo = "تا بتوانم کالاها را مدیریت کنم"
            )
     ]

        // scenario
        [Scenario(" ثبت کالا")]

        [Given(" یک گروه با نام 'لبنیات' وجود دارد")]
        [And(" فهرست کالاهای گروه 'لبنیات' خالیست")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            DbContext.Save(productGroup);
        }

        [When("کالا با نام 'شیر' به گروه کاالی لبنیات اضافه می کنم")]
        private async Task When()
        {
            dto = ProductFactory.CreateAddDto(productGroup.Id, "شیر");
            await _sut.Add(dto);
        }

        [Then(" باید یک کالا با نام 'شیر' در گروه 'لبنیات' وجود داشته باشد")]
        private void Then()
        {
            var result = ReadContext.Set<Product>().Single();
            result.Title.Should().Be(dto.Title);
            result.ProductGroupId.Should().Be(productGroup.Id);
            result.MinimumInventory.Should().Be(dto.MinimumInventory);
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
