using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Exception;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.Products.Contracts.Dtos;
using ManageShop.Services.Products.Exception;
using Taav.Contracts.Interfaces;

namespace ManageShop.Services.Products
{
    public class ProductAppService : ProductService
    {
        private readonly ProductRepository _repository;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly UnitOfWork _unitOfWork;

        public ProductAppService(ProductRepository repository,
                                 UnitOfWork unitOfWork,
                                 ProductGroupRepository productGroupRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _productGroupRepository = productGroupRepository;
        }

        public async Task Add(AddProductDto dto)
        {
            var productGroup = await _productGroupRepository.GetById(dto.ProductGroupId);

            if(productGroup == null)
            {
                throw new ProductGroupNotFoundException();
            }

            if(await _repository.IsExistTitle(dto.Title, dto.ProductGroupId))
            {
                throw new DuplicateProductNameInProductGroupException();
            }

            if (dto.MinimumInventory < 0 )
            {
                throw new InvalidPriceOrMinimumInventoryException();
            }

            var product = new Product
            {
                Title = dto.Title,
                ProductGroupId = dto.ProductGroupId,
                MinimumInventory = dto.MinimumInventory,
            };

            await _repository.Add(product);

            await _unitOfWork.Complete();
        }

        public async Task<List<GetProductDto>> GetAll()
        {
            return (await _repository.GetAll()).Select(_ => new GetProductDto
            {
                Title = _.Title,
                Inventory = _.Inventory,
                MinimumInventory = _.MinimumInventory,
                Status = _.Status,
                ProductGroupId = _.ProductGroupId,

            }).ToList();
        }
    }
}
