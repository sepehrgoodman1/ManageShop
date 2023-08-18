using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Persistence.Ef;
using ManageShop.Persistence.Ef.ProductGroups;
using ManageShop.Services.ProductGroups;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using ManageShop.Services.ProductGroups.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Specs.Tests.ProductGroups.Add
{
    public class FailedAddProductGroupDuplicateName : BusinessIntegrationTest
    {
        private readonly ProductGroupService _sut;
        private AddProductGroupDto dto;
        private Func<Task<int>> _expected;
        int productGroupId;
        private ProductGroup productGroup;

        public FailedAddProductGroupDuplicateName()
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
     [Scenario("ثبت گروه کالا ها با نام تکراری")]

        [Given(".در فهرست گروه کالاها گروهی با نام 'لبنیات' وجود دارد")]
        private void Given()
        {
            productGroup = ProductGroupFactory.Create("لبنیات");
            DbContext.Save(productGroup);
        }

        [When(" گروه ی با نام 'لبنیات' به فهرست گروه ها اضافه می کنم")]
        private void When()
        {
            dto = ProductGroupFactory.CreateAddDto("لبنیات");
            _expected = () => _sut.Add(dto);
        }

        [Then("باید خطای نام گروه تکراری است رخ دهد")]
        [And("باید فقط یک گروه با نام 'لبنیات' در فهرست گروه ها وجود داشته باشد")]
        [And("'لبنیات' جدید جایگزین 'لبنیات' قبلی نشده باشد.")]
        private async Task Then()
        {
            await _expected.Should().ThrowExactlyAsync<DuplicateProductGroupNameException>();

            // And
            var result = ReadContext.Set<ProductGroup>().Single();
            result.Name.Should().Be(productGroup.Name);

            // And
            result.Id.Should().Be(productGroup.Id);
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
