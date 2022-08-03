using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FC_CRUD.Models
{
    public class Developers
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required][EmailAddress]
        public String Email { get; set; }
        [Required][Phone]
        public String Mobile { get; set; }
        [Required]
        public String Address { get; set; }

    }
}
