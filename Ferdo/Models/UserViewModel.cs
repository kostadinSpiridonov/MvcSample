using System.ComponentModel.DataAnnotations;

namespace Ferdo.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please enter a value!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a value!")]
        [Display(Name = "Address")]
        public int AddressId { get; set; }

        public int Id { get; set; }

        public AddressViewModel Address { get; set; }
    }
}