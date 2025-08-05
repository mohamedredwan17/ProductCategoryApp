using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Common;
using ProductCategoryApp.Application.Products.DTOs;
using ProductCategoryApp.Domain.Products.ValueObjects;
using ProductCategoryApp.Domain.Products;

namespace ProductCategoryApp.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {request.Id} not found");

            var price = Price.Create(request.Price, request.Currency);
            var newCategoryId = CategoryId.Create(request.CategoryId);

            product.UpdateDetails(request.Name, price);

            if (product.CategoryId.Value != request.CategoryId)
                product.UpdateCategory(newCategoryId);

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductDto(
                product.Id,
                product.Name,
                product.Price.Value,
                product.Price.Currency,
                product.CategoryId.Value
            );
        }
    }
}
