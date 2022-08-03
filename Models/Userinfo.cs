using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FC_CRUD.Models
{
    public class Userinfo
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required][EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
