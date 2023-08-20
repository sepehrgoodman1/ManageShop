using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using ManageShop.Services.ProductGroups.Contracts;
using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using FluentAssertions;
using ManageShop.Services.ProductGroups.Exception;

namespace ManageShop.Specs.Tests.ProductGroups.Update
{
    public class FailedUpdateProductGroupDuplicateName :BusinessIntegrationTest
    {
        private readonly ProductGroupService _sut;
        private ProductGroup productGroup1;
        private ProductGroup productGroup2;
        private AddProductGroupDto dto;
        private Func<Task> _expected;

        public FailedUpdateProductGroupDuplicateName()
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
        [Scenario("ویرایش گروه کالا ها با نام تکراری")]

        [Given("در فهرست گروه کالاها یک گروه با نام 'لبنیات' و یک گروه با نام 'نوشیدنی' وجود دارد")]
        private void Given()
        {
            productGroup1 = ProductGroupFactory.Create("لبنیات");
            productGroup2 = ProductGroupFactory.Create("نوشیدنی");
            DbContext.SaveRange(productGroup1, productGroup2);
        }

        [When("نام گروه با نام 'لبنیات' را از فهرست گروه ها به 'نوشیدنی' ویرایش میکنم")]
        private void When()
        {
            dto = ProductGroupFactory.CreateAddDto("نوشیدنی");
            _expected = ()=> _sut.Update(productGroup1.Id, dto);
        }

        [Then("خطای ویرایش نام تکراری رخ دهد.")]
        [And("در فهرست گروه ها گروه با نام 'لبنیات' وجود داشته باشد")]
        [And("در فهرست گروه ها گروه با نام 'نوشیدنی' وجود داشته باشد")]
        private async Task Then()
        {
            await _expected.Should().ThrowExactlyAsync<DuplicateProductGroupNameException>();

            var result = ReadContext.Set<ProductGroup>();
            result.Where(_ => _.Id == productGroup1.Id).Should().NotBeNullOrEmpty();
            result.Where(_ => _.Id == productGroup2.Id).Should().NotBeNullOrEmpty();
            result.Where(_ => _.Name == productGroup1.Name).Should().NotBeNullOrEmpty();
            result.Where(_ => _.Name == productGroup2.Name).Should().NotBeNullOrEmpty();
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
