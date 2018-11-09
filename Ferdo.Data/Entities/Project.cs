using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Ferdo.Data.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
