using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts;
using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using FluentAssertions;

namespace ManageShop.Specs.Tests.ProductGroups.Delete
{
    public class DeleteProductGroup : BusinessIntegrationTest
    {
        private readonly ProductGroupService _sut;
        int productGroupId;
        private ProductGroup productGroupLabaniat;
        private ProductGroup productGroupNoshidani;

        public DeleteProductGroup()
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
        [Scenario("حذف گروه کالا ها")]

        [Given("در فهرست گروه کالا ها یک گروه با نام 'لبنیات' و یک گروه با نام 'نوشیدنی' وجود دارد")]
        [And("گروه 'لبنیات' دارای هیچ کالایی نمی باشد")]
        private void Given()
        {
            productGroupLabaniat = ProductGroupFactory.Create("لبنیات");
            productGroupNoshidani = ProductGroupFactory.Create("نوشیدنی");
            DbContext.SaveRange(productGroupLabaniat, productGroupNoshidani);
        }

        [When("گروه با نام 'لبنیات' را از فهرست گروه کالا ها حذف می کنم")]
        private async Task When()
        {
            await _sut.Delete(productGroupLabaniat.Id);
        }

        [Then("در فهرست گروه ها باید گروه 'لبنیات' وجود نداشته باشد")]
        [And("فقط گروه 'نوشیدنی' وجود داشته باشد ")]
        private void Then()
        {
           var result = ReadContext.Set<ProductGroup>().Single();
           result.Name.Should().Be(productGroupNoshidani.Name);
           result.Id.Should().Be(productGroupNoshidani.Id);
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
