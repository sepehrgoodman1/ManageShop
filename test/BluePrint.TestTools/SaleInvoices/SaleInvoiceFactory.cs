using ManageShop.Persistence.Ef.Productss;
using ManageShop.Persistence.Ef.PurchaseInvoics;
using ManageShop.Persistence.Ef;
using ManageShop.Services.PurchaseInvoices;
using ManageShop.Services.SalesInvoices;
using ManageShop.Services.SalesInvoices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageShop.Services.SalesInvoices.Contracts.Dtos;
using ManageShop.Entities.Entities;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using ManageShop.Persistence.Ef.SalesInvoices;
using ManageShop.Persistence.Ef.AccountingDocuments;

namespace BluePrint.TestTools.SaleInvoices
{
    public static class SaleInvoiceFactory
    {
        public static SaleInvoiceAppService CreateService(EFDataContext context)
        {
            var productRepos = new EFProductRepository(context);
            var saleInvoiceRep = new EFSalesInvoiceRepository(context);
            var AccountDocumentRepos = new EFAccountingDocumentRepository(context);
            var unitOfWork = new EFUnitOfWork(context);

            return new SaleInvoiceAppService(unitOfWork, productRepos, saleInvoiceRep, AccountDocumentRepos);

        }
        public static List<AddSaleInvoiceDto> CreateAddDto(int ProductCode1, int ProductCount1,
            double UnitPrice1, int ProductCode2 = 2, int ProductCount2 = 20, double UnitPrice2 = 50)
        {
            return new List<AddSaleInvoiceDto>
            {
                new AddSaleInvoiceDto
                {
                     ProductCode = ProductCode1,
                     ProductCount = ProductCount1,
                     UnitPrice = UnitPrice1,
                },
                 new AddSaleInvoiceDto
                {
                     ProductCode = ProductCode2,
                     ProductCount = ProductCount2,
                     UnitPrice = UnitPrice2,
                },

            };
        }
    }
}
