using ManageShop.Entities.Entities;
using ManageShop.Services.AccountingDocuments.Contracts;
using ManageShop.Services.DateGenerator;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.Products.Exception;
using ManageShop.Services.SalesInvoices.Contracts;
using ManageShop.Services.SalesInvoices.Contracts.Dtos;
using ManageShop.Services.SalesInvoices.Exception;
using Taav.Contracts.Interfaces;

namespace ManageShop.Services.SalesInvoices
{
    public class SaleInvoiceAppService : SaleInvoiceService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductRepository _productRepository;
        private readonly SalesInvoiceRepository _repository;
        private readonly AccountingDocumentRepository _accountingDocumentRepository;
        private readonly DateTimeGenerator _date;

        public SaleInvoiceAppService(UnitOfWork unitOfWork,
                                    ProductRepository productRepository,
                                    SalesInvoiceRepository salesInvoiceRepository ,
                                    AccountingDocumentRepository accountingDocumentRepository
                                    /*DateTimeGenerator date*/ )
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _repository = salesInvoiceRepository;
            _accountingDocumentRepository = accountingDocumentRepository;
/*            _date = date;
*/        }

        public async Task Add(string clientName, List<AddSaleInvoiceDto> dto)
        {
            var products = await _productRepository.FindAllByIds(dto.Select(_ => _.ProductCode).ToList());

            if (!products.Any() || products.Count != dto.Count)
            {
                throw new InvalidProductCodeException();
            }

           
            products.ForEach(_ =>
            {
                int ProductCount = dto.Where(d => d.ProductCode == _.Id).First().ProductCount;

                if (_.Inventory < ProductCount)
                {
                    throw new UnauthorizedNumberOfProductsPurchasedException();
                }

                _.Inventory -= ProductCount;
            });

            var salesInvoice = new SalesInvoice
            {
                ClientName = clientName,
                Date = DateTime.Now,
                TotalSales = dto.Sum(_ => _.UnitPrice * _.ProductCount),
                TotalProductCount = dto.Sum(_ => _.ProductCount),
            };

            var document = new AccountingDocument
            {
                Date = salesInvoice.Date,
                TotalPrice = salesInvoice.TotalSales,
                SalesInvoice = salesInvoice,
            };

            await _accountingDocumentRepository.Add(document);
            await _repository.Add(salesInvoice);


            await _unitOfWork.Complete();
        }
    }
}
