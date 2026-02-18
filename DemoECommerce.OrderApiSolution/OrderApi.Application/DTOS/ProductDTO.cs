using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.DTOS
{
    public record ProductDTO(

        int id,
        [Required] string Name,
        [Range(1, int.MaxValue)] int ProductQuantity,
        [Required, DataType(DataType.Currency)] decimal Price


        //used in Auth APi
    );
}
