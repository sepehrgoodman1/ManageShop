﻿using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.Products.Exception;
using ManageShop.Services.PurchaseInvoices.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using Taav.Contracts.Interfaces;

namespace ManageShop.Services.PurchaseInvoices
{
    public class PurchaseInvoiceAppService : PurchaseInvoiceService
    {
        private readonly PurchaseInvoiceRepository _purchaseInvoiceRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductRepository _productRepository;

        public PurchaseInvoiceAppService(PurchaseInvoiceRepository purchaseInvoiceRepository,
                                         UnitOfWork unitOfWork,
                                         ProductRepository productRepository)
        {
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<int> Add(List<AddPurchaseInvoiceDto> dto)
        {

            var products = await _productRepository.FindAllByIds(dto.Select(_ => _.ProductCode).ToList());

            if(! products.Any() || products.Count != dto.Count) 
            {
                throw new InvalidProductCodeException();
            }

            products.ForEach(_ => _.Inventory += dto.Where(d => d.ProductCode == _.Id).FirstOrDefault().ProductRecivedCount);

            products.ForEach(_ =>
            {
                if(dto.Where(d => d.ProductCode == _.Id).FirstOrDefault().ProductRecivedCount <= 0)
                {
                    _.Status = ProductStatus.UnAvailable;
                }
                else if (dto.Where(d => d.ProductCode == _.Id).FirstOrDefault().ProductRecivedCount > _.MinimumInventory)
                {
                    _.Status = ProductStatus.Available;
                }
                else if(dto.Where(d => d.ProductCode == _.Id).FirstOrDefault().ProductRecivedCount <= _.MinimumInventory)
                {
                    _.Status = ProductStatus.ReadyToOrder;
                }
            }
            );

            var purchaseInvoice =  new PurchaseInvoice
            {
                Date = DateTime.Now,
                ProductPurchaseInvoices = products.Select(_ => new ProductPurchaseInvoice
                {
                    Products = _ ,
                
                }).ToHashSet(),

            };

            await _purchaseInvoiceRepository.Add(purchaseInvoice);

            await _unitOfWork.Complete();

            return purchaseInvoice.Id;
        }

        public async Task<List<GetPurchaseInvoiceDto>> GetAll()
        {
            return (await _purchaseInvoiceRepository.GetAll())
                    .SelectMany(_ => _.ProductPurchaseInvoices)
                    .Select(_=> new GetPurchaseInvoiceDto
                    {
                        ProductCode = _.Products.Id,
                        Title = _.Products.Title,
                        MinimumInventory = _.Products.MinimumInventory,
                        Inventory = _.Products.Inventory,
                        Status = _.Products.Status.ToString(),
                        ProductGroupId = _.Products.ProductGroupId,
                        ProductGroupName = _.Products.ProductGroup.Name,
                    }).DistinctBy(_ => _.ProductCode).ToList();
        }

        public async Task<List<GetPurchaseInvoiceDto>> Search(string search)
        {
            return (await _purchaseInvoiceRepository.Search(search))
                   .SelectMany(_ => _.ProductPurchaseInvoices)
                   .Select(_ =>  new GetPurchaseInvoiceDto
                   {
                       ProductCode = _.Products.Id,
                       Title = _.Products.Title,
                       MinimumInventory = _.Products.MinimumInventory,
                       Inventory = _.Products.Inventory,
                       Status = _.Products.Status.ToString(),
                       ProductGroupId = _.Products.ProductGroupId,
                       ProductGroupName = _.Products.ProductGroup.Name,
                   }).DistinctBy(_ => _.ProductCode).ToList();
        }
    }
    }

