using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using ManageShop.Services.ProductGroups.Exception;
using ManageShop.Services.Products.Contracts;
using Taav.Contracts.Interfaces;

namespace ManageShop.Services.ProductGroups
{
    public class ProductGroupAppService : ProductGroupService
    {
        private readonly ProductGroupRepository _repository;
        private readonly ProductRepository _productRepository;
        private readonly UnitOfWork _unitOfWork;

        public ProductGroupAppService(ProductGroupRepository repository,
                                      UnitOfWork unitOfWork,
                                      ProductRepository productRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<int> Add(AddProductGroupDto dto)
        {
            var productGroup = new ProductGroup
            {
                Name = dto.Name.Replace(" ",""),
            };

            if(await _repository.IsExistByName(productGroup.Name))
            {
                throw new DuplicateProductGroupNameException();
            }

            await _repository.Add(productGroup);

            await _unitOfWork.Complete();

            return productGroup.Id;
        }

        public async Task Delete(int id)
        {
            var productGroup = await _repository.GetById(id);

            if(productGroup == null)
            {
                throw new ProductGroupNotFoundException();
            }
            
            if(await _productRepository.HaveProduct(id))
            {
                throw new ProductGroupHaveProductException();
            }

            _repository.Delete(productGroup);

            await _unitOfWork.Complete();

        }

        public async Task<List<GetProductGroupDto>> GetAll()
        {
            return (await _repository.GetAll()).Select(_ => new GetProductGroupDto
            {
                Name = _.Name,
            }).ToList();
        }

        public async Task Update(int id, AddProductGroupDto dto)
        {
            var productGroup = await _repository.GetById(id);

            if (productGroup == null)
            {
                throw new ProductGroupNotFoundException();
            }

            if (await _repository.IsExistByName(dto.Name))
            {
                throw new DuplicateProductGroupNameException();
            }

            if (await _productRepository.HaveProduct(id))
            {
                throw new ProductGroupHaveProductException();
            }

            productGroup.Name = dto.Name;

            await _unitOfWork.Complete();

        }
    }
}
