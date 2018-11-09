using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ferdo.Models
{
    public class ProjectViewModel
    {
        [Required(ErrorMessage = "Please enter a value!")]
        public string Name { get; set; }

        public int Id { get; set; }

        public IEnumerable<int> UsersIds { get; set; }
    }
}