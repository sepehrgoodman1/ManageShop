using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Specs.Tests.ProductGroups.Update
{
    public class UpdateProduct: BusinessIntegrationTest
    {
        private readonly ProductGroupService _sut;
        private ProductGroup productGroup1;
        private AddProductGroupDto dto;

        public UpdateProduct()
        {
            _sut = ProductGroupFactory.CreateService(SetupContext);
        }

        //User Story
        [Feature(
            AsA = "من به عنوان ادمین سایت فروشگاهی",
            IWantTo = "می خواهم گروه کالا هارا ثبت کنم",
            InOrderTo = "تا بتوانم کاالهای فروشگاهم را بهتر مدیریت کنم"
            )
     ]

        // scenario
        [Scenario("ویرایش گروه کالا ها ")]

        [Given("در فهرست گروه کالاها یک گروه با نام 'لبنیات' وجود دارد")]
        private void Given()
        {
            productGroup1 = ProductGroupFactory.Create("لبنیات");
            DbContext.SaveRange(productGroup1);
        }

        [When("نام گروه با نام 'لبنیات' را از فهرست گروه ها به 'نوشیدنی' ویرایش میکنم")]
        private async Task When()
        {
            dto = ProductGroupFactory.CreateAddDto("نوشیدنی");
            await _sut.Update(productGroup1.Id, dto);
        }

        [Then("در فهرست گروه ها گروه با نام 'نوشیدنی' وجود داشته باشد")]
        [And("در فهرست گروه ها گروه با نام 'لبنیات' وجود نداشته باشد")]
        private void Then()
        {
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Name.Should().Be(dto.Name);
            result.Id.Should().Be(productGroup1.Id);
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
