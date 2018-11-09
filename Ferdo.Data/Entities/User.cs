using System.ComponentModel.DataAnnotations.Schema;

namespace Ferdo.Data.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        
        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }
    }
}
