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

namespace ManageShop.Specs.Tests.PurchaseInvoices.Add
{
    public class AddPurchaseInvoice :BusinessIntegrationTest
    {
   

        public AddPurchaseInvoice()
        {
            
        }

        //User Story
        [Feature(
            AsA = "من به عنوان مسئول انبار",
            IWantTo = "می خواهم فاکتور ورود کالا ها به انبار را ثبت کنم",
            InOrderTo = "تا بتوانم انبار کالا ها را مدیریت کنم"
            )
     ]

        // scenario
        [Scenario("ثبت ورود کالا وقتی که وضعیت کاال موجود باشد")]

        [Given(" یک کالا با کد کالا 12 و تعداد موجودی 5 عدد و حداقل موجودی 5 و وضعیت موجود در فهرست کالاها وجود دارد")]
        private void Given()
        {
          
        }

        [When("بیست عدد کالا با کد 12 را در تاریخ 1402/05/08 به فهرست کالاهای ورودی اضافه می کنم")]
        private async Task When()
        {
        
        }

        [Then(" باید 20 عدد کاال با کد 12 و تاریخ 05/08 /1402 را در فهرست کالاهای ورودی داشته باشد")]
        [And(" موجودی کالا با کد 12 باید برابر با 25 شود و وضعیت آن همچنان موجود باشد.")]
        private void Then()
        {
            
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
