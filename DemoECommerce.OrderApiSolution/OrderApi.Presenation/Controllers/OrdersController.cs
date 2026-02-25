using ECommerce.SharedLibirary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.DTOS;
using OrderApi.Application.DTOS.Conversions;
using OrderApi.Application.Interfaces;
using OrderApi.Application.services;

namespace OrderApi.Presenation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrder orderInterface, IOrderService orderService) : ControllerBase
    {

        [HttpGet]

        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await orderInterface.GetAllAsync();
            if (!orders.Any())
            {
                return NotFound("No Orders Found in the Database");
            }

            var mappedorders = OrderConversion.ToDTOs(orders);




            return Ok(mappedorders);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await orderInterface.FindByIdAsync(id);
            if (order == null)
            {
                return NotFound($"No Order Found with the Id {id}");
            }
            var mappedorder = OrderConversion.ToDto(order);
            return Ok(mappedorder);

        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDTO orderDTO)
        {
            var order = OrderConversion.ToEntity(orderDTO);
            if (order == null)
            {
                return BadRequest("There was Error Creating your Order");
            }
            await orderInterface.CreateAsync(order);
            return Ok("Order Created Successfully");

        }
        //[HttpPost("{id}")]

        //public async Task<ActionResult<OrderDTO>> GetOrderBy(int id)
        //{
        //    var order = await orderInterface.FindByIdAsync(id);

        //    if (order == null)
        //    {
        //        return NotFound($"No Order Found with the Id {id}");
        //    }

        //    var mappedorder = OrderConversion.ToDto(order);

        //    return Ok(mappedorder);
        //}





        [HttpPut]
        public async Task<ActionResult<Response>> UpdateOrder(OrderDTO Dto)
        {
            var order = await orderInterface.FindByIdAsync(Dto.Id);
            if (order == null)
            {
                return NotFound("No Order Found with the Id {Dto.Id}");
            }

            var mappedOrder = OrderConversion.ToEntity(Dto);
            //await orderInterface.UpdateAsync(mappedOrder);

            if (order == null)
            {
                return BadRequest("There was Error Updating your Order");
            }
            await orderInterface.UpdateAsync(mappedOrder);
            return Ok("Order Updated Successfully");

        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteOrder(OrderDTO Dto)
        {
            var order = await orderInterface.FindByIdAsync(Dto.Id);
            if (order == null)
            {
                return NotFound($"No Order Found with the Id {Dto.Id}");
            }

            await orderInterface.DeleteAsync(order);
            return Ok("Order Deleted Successfully");
        }

        [HttpGet("client/{clientId:int}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderByClientId(int clientId)
        {
            if (clientId <= 0)
                return BadRequest("Invalid client Id");

            var orders = await orderInterface.GetOrdersAsync(o => o.ClientId == clientId);
            if (!orders.Any())
                return NotFound($"No orders found for client with Id : {clientId}");

            var mappedOrders = OrderConversion.ToDTOs(orders);
            return Ok(mappedOrders);
        }
        [HttpGet("details/{orderId:int}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int orderId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order Id");

            var orderDetails = await orderService.GetOrderDetailsAsync(orderId);

            if (orderDetails == null)
                return NotFound($"No order details found for order with Id : {orderId}");


            return Ok(orderDetails);

        }
    }
}
