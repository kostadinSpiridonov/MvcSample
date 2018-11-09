using System.ComponentModel.DataAnnotations;

namespace Ferdo.Models
{
    public class AddressViewModel
    {
        [Required(ErrorMessage = "Please enter a value!")]
        public string Country { get; set; }

        public string City { get; set; }

        public int Id { get; set; }

        public string FullAddress => $"{Country} {City}";
    }
}