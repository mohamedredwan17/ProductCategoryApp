using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Common;
using ProductCategoryApp.Application.Products.DTOs;
using ProductCategoryApp.Domain.Products.Entities;
using ProductCategoryApp.Domain.Products.ValueObjects;
using ProductCategoryApp.Domain.Products;

namespace ProductCategoryApp.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var price = Price.Create(request.Price, request.Currency);
            var categoryId = CategoryId.Create(request.CategoryId);

            var product = Product.Create(request.Name, price, categoryId);

            await _productRepository.AddAsync(product, cancellationToken);
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
