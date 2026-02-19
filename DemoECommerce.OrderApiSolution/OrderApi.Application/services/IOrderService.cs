using OrderApi.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.services
{
    public  interface IOrderService 
    {
        Task<IEnumerable<OrderDTO>> GetOrderByClientIdAsync(int ClientId);
        Task <OrderDetailsDTO> GetOrderDetailsAsync(int OrderId);


    }
}
