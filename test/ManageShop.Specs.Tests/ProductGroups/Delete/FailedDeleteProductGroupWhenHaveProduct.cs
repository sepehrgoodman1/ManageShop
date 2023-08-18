using BluePrint.TestTools.Infrastructure.DataBaseConfig;
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration;
using BluePrint.TestTools.ProductGroups;
using BluePrint.TestTools.Products;
using FluentAssertions;
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Specs.Tests.ProductGroups.Delete
{
    public class FailedDeleteProductGroupWhenHaveProduct : BusinessIntegrationTest
    {
      
            private readonly ProductGroupService _sut;
            int productGroupId;
            private ProductGroup productGroup;
            private Product product;
        private Func<Task> _expected;

        public FailedDeleteProductGroupWhenHaveProduct()
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

            [Given("در فهرست گروه کالا ها یک گروه با نام 'لبنیات' وجود دارد")]
            [And("گروه 'لبنیات' دارای یک محصول با نام 'شیر' میباشد")]
            private void Given()
            {
                productGroup = ProductGroupFactory.Create("لبنیات");
                product = ProductFactory.Create(productGroup, "شیر");
                DbContext.Save(product);
            }

            [When("گروه با نام 'لبنیات' را از فهرست گروه کالا ها حذف می کنم")]
            private void When()
            {
                 _expected = ()=> _sut.Delete(productGroup.Id);
            }

            [Then("خطای حذف گروه رخ دهد")]
            [And("در فهرست گروه ها گروه 'لبنیات' وجود داشته باشد ")]
            private async Task Then()
            {
                await _expected.Should().ThrowExactlyAsync<ProductGroupHaveProductException>();

                var result = ReadContext.Set<ProductGroup>().Single();
                result.Name.Should().Be(productGroup.Name);
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
