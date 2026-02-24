using OrderApi.Application.DTOS;
using OrderApi.Application.DTOS.Conversions;
using OrderApi.Application.Interfaces;
using Polly.Registry;
using System.Net.Http.Json;

namespace OrderApi.Application.services
{
    //we gonna use http client because there is outside info we need to get from the request 
    //also we gonna use resilent pipeline for the retry strategy ==> from polly  
    public class OrderService(HttpClient httpClient, ResiliencePipelineProvider<string> resiliencePipeline, IOrder order) : IOrderService
    {

        //we gonna use unit testing here 

        public async Task<ProductDTO> GetProductAsync(int ProductId)
        {
            //call Product APi 
            //and we need to make it move throw teh api gateway  ==> always redirect any request to the gateway
            var getProd = await httpClient.GetAsync($"/api/products/{ProductId}");
            if (!getProd.IsSuccessStatusCode)
            {
                return null;
            }
            var product = await getProd.Content.ReadFromJsonAsync<ProductDTO>();
            return product;
        }


        //get user

        public async Task<AppUserDTO> GetUserAsync(int userId)
        {
            //call Auth APi 
            //and we need to make it move throw the api gateway  ==> always redirect any request to the gateway
            var getUser = await httpClient.GetAsync($"/api/authentication/{userId}"); //look out here ya saif 
            if (!getUser.IsSuccessStatusCode)
            {
                return null;
            }
            var product = await getUser.Content.ReadFromJsonAsync<AppUserDTO>();
            return product;
        }


        public async Task<OrderDetailsDTO> GetOrderDetailsAsync(int OrderId)
        { //we need the AUth info            and           also the product info from above     


            //prepare order 
            var Order = await order.FindByIdAsync(OrderId);
            if (Order is null || Order!.Id <= 0)
            {
                return null!;
            }
            //the retry pipeline 
            var retryPipeline = resiliencePipeline.GetPipeline($"my-retry-pipeline");

            //prepare the product ==>helper method

            //we will get that using the token 
            var productDto = await retryPipeline.ExecuteAsync(async token => await GetProductAsync(Order.ProductId));


            //we will Specify the number of retries in the app json 


            //prepare Client 

            // no need to check for the user if there is no user WTF is all that for 


            var AppUserDto = await retryPipeline.ExecuteAsync(async token => await GetUserAsync(Order.ClientId));
            if (AppUserDto is null) return null!;

            //prepare the order Details dto 

            return new OrderDetailsDTO(
                OrderId: Order.Id,
                ProductId:productDto.id,
                ClientId: AppUserDto.id,
                Name : AppUserDto.Name,
                Email: AppUserDto.Email,
                TelephoneNumber: AppUserDto.Telephone,
                ProductName: productDto.Name,
                PurchaseQuantity: Order.PurchaseQuantity,
                UnitPrice: productDto.Price,
                TotalPrice: productDto.Price * Order.PurchaseQuantity,
                OrderDate: Order.OrderDate
            );






        }


        //get order by having outside info
        public async Task<IEnumerable<OrderDTO>> GetOrderByClientIdAsync(int ClientId)
        {
            //var get client orders
            var orders = await order.GetOrdersAsync(o=>o.ClientId == ClientId);
            if (!orders.Any()) return null;


            var OrderDtos = OrderConversion.ToDTOs(orders);

            return OrderDtos;



        }


    }
}
 