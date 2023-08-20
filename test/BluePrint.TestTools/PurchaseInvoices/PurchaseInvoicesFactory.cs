using ManageShop.Persistence.Ef.Productss;
using ManageShop.Persistence.Ef;
using ManageShop.Services.PurchaseInvoices;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using ManageShop.Persistence.Ef.PurchaseInvoics;

namespace BluePrint.TestTools.PurchaseInvoices
{
    public static class PurchaseInvoicesFactory
    {

        /*   public static Product Create(ProductGroup productGroup, string title = "shir",
               int inventory = 0, int minimumInventory = 10,
               double price = 100, ProductStatus productStatus = ProductStatus.Available
               )
           {
               return new Product()
               {
                   ProductGroup = productGroup,
                   Title = title,
                   Inventory = inventory,
                   Price = price,
                   MinimumInventory = minimumInventory,
                   Status = productStatus,
               };
           }*/
        public static List<AddPurchaseInvoiceDto> CreateAddDto(int productCode = 12 , int productRecivedCount = 20)
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
