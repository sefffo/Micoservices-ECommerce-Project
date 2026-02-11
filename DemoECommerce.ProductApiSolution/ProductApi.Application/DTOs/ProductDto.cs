using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductApi.Application.DTOs
{
    public record ProductDto
    (
        int Id,
        [Required] string Name,
        [Required, Range(1, int.MaxValue)] int Quantity,
        [Required, DataType(DataType.Currency)] decimal Price
    );//it gonana be unit tested so we made it as a record type and we added the data annotations to make sure that the data is valid when we create a new product

}
