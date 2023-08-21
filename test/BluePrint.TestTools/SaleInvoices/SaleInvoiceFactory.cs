using ManageShop.Persistence.Ef.Productss;
using ManageShop.Persistence.Ef;
using ManageShop.Services.SalesInvoices;
using ManageShop.Services.SalesInvoices.Contracts.Dtos;
using ManageShop.Persistence.Ef.SalesInvoices;
using ManageShop.Persistence.Ef.AccountingDocuments;
using ManageShop.Services.DateGenerator;

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

            return new SaleInvoiceAppService(unitOfWork, productRepos,
                saleInvoiceRep, AccountDocumentRepos);

        }
        public static List<AddSaleInvoiceDto> CreateAddDtoTwo(int ProductCode1,
                                                              int ProductCount1,
                                                              double UnitPrice1,
                                                              int ProductCode2 = 2,
                                                              int ProductCount2 = 20,
                                                              double UnitPrice2 = 50)
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

        public static List<AddSaleInvoiceDto> CreateAddDtoOne(int ProductCode1,
                                                              int ProductCount1,
                                                              double UnitPrice1)
        {
            return new List<AddSaleInvoiceDto>
            {
                new AddSaleInvoiceDto
                {
                     ProductCode = ProductCode1,
                     ProductCount = ProductCount1,
                     UnitPrice = UnitPrice1,
                },
               

            };
        }
    }
}
