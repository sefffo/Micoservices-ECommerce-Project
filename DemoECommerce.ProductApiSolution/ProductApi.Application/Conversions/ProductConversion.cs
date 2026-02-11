using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Application.Conversions
{
    public static class ProductConversion
    {
        public static Product ToEntity(ProductDto productDto)
        {
            if (productDto is null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Quantity = productDto.Quantity,
                Price = productDto.Price
            };
            return product;
        }

        public static ProductDto ToDto(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var productDto = new ProductDto
            (
                product.Id,
                product.Name,
                product.Quantity,
                product.Price
            );
            return productDto;
        }

        public static IEnumerable<ProductDto> ToDtos(IEnumerable<Product> products)
        {

            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }


            var productDtos = new List<ProductDto>();
            foreach (var product in products)
            {
                productDtos.Add(ToDto(product));
            }
            return productDtos;
        }
        public static IEnumerable<Product> ToEntities(IEnumerable<ProductDto> productDtos)
        {

            if (productDtos is null)
            {
                throw new ArgumentNullException(nameof(productDtos));
            }

            var products = new List<Product>();
            foreach (var productDto in productDtos)
            {
                products.Add(ToEntity(productDto));
            }
            return products;
        }


    }
}
