using ECommerce.SharedLibirary.Logs;
using ECommerce.SharedLibirary.Responses;
using Microsoft.EntityFrameworkCore;
using OrderApi.Application.Interfaces;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Repositories
{
    public class OrderRepository(OrderDbContext context) : IOrder
    //we done that to use the DIP and to make the code more testable and maintainable
    //we will implement the methods of the IOrder interface in this class, and we will use the Order entity to perform the operations on the database 
    {
        public async Task<Response> CreateAsync(Order Entity)
        {
            try
            {
                var order = context.Orders.Add(Entity).Entity;


                await context.SaveChangesAsync();

                if (order.Id > 1)
                {
                    return new Response(true, "Order placed successfully");
                }
                else
                {
                    return new Response(false, "Failed to place order");
                }




            }
            catch (Exception ex)
            {
                //log the original exception here using a logging framework  Serilog


                LogExceptions.LogException(ex); // Assuming LogExceptions is a static class with a method to log exceptions

                // display message to the user in a friendly way 

                return new Response(false, "Error occurred while placing order");

            }
        }

        public async Task<Response> DeleteAsync(Order Entity)
        {
            try
            {
                var order = await FindByIdAsync(Entity.Id);

                if (order is null)
                {
                    return new Response(false, "Order not found");
                }

                else
                {
                    await context.SaveChangesAsync();
                    return new Response(true, "Order deleted successfully");
                }
            }
            catch (Exception ex)
            {
                //log the original exception here using a logging framework  Serilog


                LogExceptions.LogException(ex); // Assuming LogExceptions is a static class with a method to log exceptions

                // display message to the user in a friendly way 

                return new Response(false, "Error occurred while Deleting order");

            }
        }

        public async Task<Order> FindByIdAsync(int Id)
        {
            try
            {
                var order = await context.Orders.FindAsync(Id);
                if (order is null)
                {
                    return null;
                }
                return order;

            }
            catch (Exception ex)
            {
                //log the original exception here using a logging framework  Serilog


                LogExceptions.LogException(ex); // Assuming LogExceptions is a static class with a method to log exceptions

                // display message to the user in a friendly way 

                throw new Exception("Error Occurred while retrieving order ");

            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {


            try
            {
                var orders = await context.Orders.AsNoTracking().ToListAsync();


                if (orders is null)
                {
                    return null;
                }

                return orders;
            }
            catch (Exception ex)
            {
                //log the original exception here using a logging framework  Serilog


                LogExceptions.LogException(ex); // Assuming LogExceptions is a static class with a method to log exceptions

                // display message to the user in a friendly way 

                throw new Exception("Error Occurred while retrieving orders ");

            }


        }

        public async Task<Order> GetBy(Expression<Func<Order, bool>> Predicate)
        {
            try
            {
                var order = await context.Orders.FirstOrDefaultAsync(Predicate);

                if (order is null)
                {
                    return null;
                }
                return order;
            }
            catch (Exception ex)
            {

                LogExceptions.LogException(ex); // Assuming LogExceptions is a static class with a method to log exceptions

                // display message to the user in a friendly way 

                throw new Exception($"Error Occurred while retrieving order by {Predicate.Body} ");
            }

        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> Predicate) //specifications here 
        {
            try
            {
                var order = await context.Orders.Where(Predicate).ToListAsync();

                if (order is null) return null;

                return order;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error Occurred while retrieving order by {Predicate.Body} ");
            }
        }

        public async Task<Response> UpdateAsync(Order Entity)
        {
            try
            {
                var order = await FindByIdAsync(Entity.Id);

                if (order is null)
                {
                    return new Response(false, "Order not found");
                }
                else
                {


                    context.Orders.Update(Entity);


                    await context.SaveChangesAsync();

                    return new Response(true, "Order updated successfully");
                }


            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                throw new Exception($"Error Occurred while Finding order with id : {Entity.Id} ");
            }
        }
    }
}
