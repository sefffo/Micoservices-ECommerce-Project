using ECommerce.SharedLibirary.Logs;
using ECommerce.SharedLibirary.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;
using System.Linq.Expressions;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProductRepository(ProductDbContext context) : IProduct
    {

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await context.products.AsNoTracking().ToListAsync();

            return (products is not null) ? products : null; ;

        }

        public Task<Product> GetBy(Expression<Func<Product, bool>> Predicate)
        {

            try
            {
                var product = context.products.AsNoTracking().FirstOrDefault(Predicate);

                return product is not null ? Task.FromResult(product) : Task.FromResult<Product>(null);
            }
            catch (Exception ex)
            {
                //log the exceptions 
                LogExceptions.LogException(ex);

                //return a response with the error message

                throw new Exception($"An error occurred while finding the product with the specified condition");
            }


        }



        public async Task<Response> CreateAsync(Product Entity)
        {
            try
            {

                var getProduct = await GetBy(p => p.Name!.Equals(Entity.Name));
                if (getProduct != null && !string.IsNullOrEmpty(getProduct.Name))
                {
                    return new Response
                    {
                        flag = false,
                        Message = $"Product with name '{Entity.Name}' already exists"
                    };
                }
                //add the product to the database
                var product = await context.products.AddAsync(Entity);
                await context.SaveChangesAsync();
                return new Response
                {
                    flag = true,
                    Message = $"Product '{Entity.Name}' created successfully"
                };

            }
            catch (Exception ex)
            {
                //log the exceptions 
                LogExceptions.LogException(ex);

                //return a response with the error message

                return new Response
                {
                    flag = false,
                    Message = $"An error occurred while creating the product"
                };
            }


        }


        public async Task<Response> UpdateAsync(Product Entity)
        {
            try
            {

                var getProduct = await context.products.FirstOrDefaultAsync(p => p.Id == Entity.Id);
                if (getProduct == null || string.IsNullOrEmpty(getProduct.Name))
                {
                    return new Response
                    {
                        flag = false,
                        Message = $"Product with id '{Entity.Id}' does not exist"
                    };
                }
                else
                {
                    getProduct.Name = Entity.Name;
                    getProduct.Price = Entity.Price;
                    getProduct.Quantity = Entity.Quantity;

                    context.products.Update(getProduct);
                    await context.SaveChangesAsync();
                    return new Response
                    {
                        flag = true,
                        Message = $"Product with id '{Entity.Id}' and Name '{Entity.Name}' updated successfully"
                    };
                }




            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                return new Response
                {
                    flag = false,
                    Message = $"An error occurred while updating the product"
                };
            }
        }

        //public Task<Response> UpdateAsync(int Id, Product Entity)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<Response> DeleteAsync(Product Entity)
        {
            try
            {

                var getProduct = await FindByIdAsync(Entity.Id);
                if (getProduct == null || string.IsNullOrEmpty(getProduct.Name))
                {
                    return new Response
                    {
                        flag = false,
                        Message = $"Product with id '{Entity.Id}' does not exist"
                    };
                }
                else
                {
                    context.products.Remove(getProduct);
                    await context.SaveChangesAsync();
                    return new Response
                    {
                        flag = true,
                        Message = $"Product with id '{Entity.Id}' deleted successfully"
                    };
                }


            }
            catch (Exception ex)
            {
                //log the exceptions 
                LogExceptions.LogException(ex);

                //return a response with the error message

                return new Response
                {
                    flag = false,
                    Message = $"An error occurred while Deleting the product"
                };
            }
        }

        public async Task<Product> FindByIdAsync(int Id)
        {

            try
            {
                var product = await context.products.FirstOrDefaultAsync(p => p.Id == Id);

                Response response = (product is not null) ? new Response
                {
                    flag = true,
                    Message = $"Product with id '{Id}' found successfully"
                } : new Response
                {
                    flag = false,
                    Message = $"Product with id '{Id}' does not exist"
                };

                return (product is not null) ? product : null;



            }
            catch (Exception ex)
            {
                //log the exceptions 
                LogExceptions.LogException(ex);

                throw new Exception($"An error occurred while finding the product with id '{Id}'");

            }

        }





    }
}
