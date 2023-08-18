using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Persistence.Ef;
using ManageShop.Persistence.Ef.ProductGroups;
using ManageShop.Services.ProductGroups;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Specs.Tests.ProductGroups.Add
{
    public class AddProductGroup : BusinessIntegrationTest
    {
        private readonly ProductGroupService _sut;
        private AddProductGroupDto dto;
        int productGroupId;
        public AddProductGroup()
        {
            _sut = ProductGroupFactory.CreateService(SetupContext);
        }

        //User Story
        [Feature(
            AsA = "من به عنوان ادمین سایت فروشگاهی",
            IWantTo ="می خواهم گروه کالا هارا ثبت کنم",
            InOrderTo = "تا بتوانم کاالهای فروشگاهم را بهتر مدیریت کنم"
            )
     ]

     // scenario
     [Scenario("ثبت گروه کالا ها")]

        [Given("فهرست گروه کالا ها خالیست")]
        private void Given()
        {

        }

        [When("گروه با نام 'لبنیات' به فهرست گروه ها اضافه می کنم")]
        private async Task When()
        {
            dto = ProductGroupFactory.CreateAddDto();
            productGroupId = await _sut.Add(dto);
        }

        [Then("باید یک گروه با نام 'لبنیات' در فهرست گروه ها وجود داشته باشد")]
        private void Then()
        {
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Id.Should().Be(productGroupId);
            result.Name.Should().Be(dto.Name);
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
