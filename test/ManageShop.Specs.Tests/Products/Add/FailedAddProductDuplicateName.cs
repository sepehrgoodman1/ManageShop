using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Contracts.Dtos;
using ManageShop.Services.Products.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ManageShop.Services.Products.Exception;

namespace ManageShop.Specs.Tests.Products.Add
{
    public class FailedAddProductDuplicateName: BusinessIntegrationTest
    {
        private readonly ProductService _sut;
        private AddProductDto dto;
        private Func<Task> _expected;
        private ProductGroup productGroup;
        private Product product;

        public FailedAddProductDuplicateName()
        {
            _sut = ProductFactory.CreateService(SetupContext);
        }

        //User Story
        [Feature(
            AsA = "من به عنوان مسئول انبار",
            IWantTo = "می خواهم ثبت ورود کاال به انبار را ثبت کنم",
            InOrderTo = "تا بتوانم انبار کاالها را مدیریت کنم"
            )
     ]

        // scenario
        [Scenario(" ثبت کالا با نام تکراری در یک گروه")]

        [Given(" یک گروه با نام 'لبنیات' وجود دارد")]
        [And("کالا با نام 'شیر' در گروه 'لبنیات' وجود دارد")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            product = ProductFactory.Create(productGroup, "شیر");
            DbContext.Save(product);
        }

        [When("کالایی با نام 'شیر' به گروه 'لبنیات' اضافه می کنم")]
        private void When()
        {
            dto = ProductFactory.CreateAddDto(productGroup.Id, "شیر");
            _expected = ()=> _sut.Add(dto);
        }

        [Then(" باید خطای نام کالا در گروه کالا تکراری است رخ دهد")]
        [And(" باید فقط یک کالا با نام 'شیر' در گروه 'لبنیات' وجود داشته باشد")]
        private async Task Then()
        {
            await _expected.Should().ThrowExactlyAsync<DuplicateProductNameInProductGroupException>();
            var actual = ReadContext.Set<Product>().Single();
            actual.Title.Should().Be(product.Title);
            actual.Id.Should().Be(product.Id);
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
