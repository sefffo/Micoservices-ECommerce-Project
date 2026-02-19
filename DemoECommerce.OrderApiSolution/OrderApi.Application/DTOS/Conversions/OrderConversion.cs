using OrderApi.Domain.Entities;

namespace OrderApi.Application.DTOS.Conversions
{
    public static class OrderConversion
    {
        public static Order ToEntity(OrderDTO orderDTO)
        {
            return new Order
            {
                Id = orderDTO.Id,
                ClientId = orderDTO.ClientId,
                OrderDate = orderDTO.OrderDate,
                ProductId = orderDTO.ProductId,
                PurchaseQuantity = orderDTO.PurchaseQuantity,
            };
        }

        public static OrderDTO ToDto(Order order)
        {
            return new OrderDTO(
                order.Id,
                order.ProductId,
                order.ClientId,
                order.PurchaseQuantity,
                order.OrderDate
            );

        }

        public static IEnumerable<Order> ToEntities(IEnumerable<OrderDTO> orderDTO)
        {
            if(orderDTO is null ) throw new ArgumentNullException(nameof(orderDTO));
            var Orders = new List<Order>();
            foreach (var order in orderDTO)
            {
                Orders.Add(ToEntity(order));
            }
            return Orders;
        }

        public static IEnumerable<OrderDTO> ToDTOs(IEnumerable<Order> orders)
        {
            //alternative approach 
            //if (orders is null) throw new ArgumentNullException(nameof(orders));
            //return orders.Select(ToDto);

            if (orders is null) throw new ArgumentNullException(nameof(orders)); // consistent with ToEntities
            var orderDtos = new List<OrderDTO>();          // ✅ correct type
            foreach (var order in orders)                  // ✅ iterate the input parameter
            {
                orderDtos.Add(ToDto(order));               // ✅ add OrderDTO to List<OrderDTO>
            }
            return orderDtos;                              // ✅ return the result
        }
    }
}
