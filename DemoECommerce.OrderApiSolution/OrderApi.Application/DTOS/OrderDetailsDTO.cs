using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DTOS
{
    public record OrderDetailsDTO(

        [Required] int OrderId,
        [Required] int ProductId,
        [Required] int ClientId,
        [Required] string Name,
        [Required, EmailAddress] string Email,
        [Required, Phone] string TelephoneNumber,
        [Required] string ProductName,
        [Required] int PurchaseQuantity,
        [Required, DataType(DataType.Currency)] decimal UnitPrice,
        [Required, DataType(DataType.Currency)] decimal TotalPrice,
        [Required] DateTime OrderDate

    );
}
