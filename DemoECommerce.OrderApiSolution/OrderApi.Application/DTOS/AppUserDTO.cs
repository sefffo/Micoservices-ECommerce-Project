using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.DTOS
{
    public record AppUserDTO(
        int id,
        [Required] string Name,
        [Required, Phone] string Telephone,
        [Required] string Address,
        [Required, EmailAddress] string Email,
        [Required] string Password,
        [Required] string Role

    );
}
