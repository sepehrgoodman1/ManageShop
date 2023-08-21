using ManageShop.Persistence.Ef.Productss;
using ManageShop.Persistence.Ef;
using ManageShop.Services.PurchaseInvoices;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using ManageShop.Persistence.Ef.PurchaseInvoics;
using ManageShop.Entities.Entities;

namespace BluePrint.TestTools.PurchaseInvoices
{
    public static class PurchaseInvoicesFactory
    {

        public static PurchaseInvoice Create(Product product)
        {
            var productPurchaseInvoices = new List<ProductPurchaseInvoice>
            {
                new ProductPurchaseInvoice
                {
                    Products = product
                }
            };
            return new PurchaseInvoice
            {
                ProductPurchaseInvoices = productPurchaseInvoices.ToHashSet()
            };
        }

        public static List<AddPurchaseInvoiceDto> CreateAddDto(int productCode = 1,
                                                               int productRecivedCount = 20)
        {
            return new List<AddPurchaseInvoiceDto>
            {
                new AddPurchaseInvoiceDto
                {
                     ProductCode = productCode,
                     ProductRecivedCount = productRecivedCount,
                },

            };
        }
    

        public static PurchaseInvoiceAppService CreateService(EFDataContext context)
        {
            var productRepos = new EFProductRepository(context);
            var purchaseInvoiceRepos = new EFPurchaseInvoiceRepository(context);
            var unitOfWork = new EFUnitOfWork(context);

            return new PurchaseInvoiceAppService( purchaseInvoiceRepos, unitOfWork, productRepos);

        }
    }
}
