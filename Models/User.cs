using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharp.Models
{
    public class User
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string? Password { get; set; }
        //public string? AccountType { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }

        public ICollection<Measurement>? Measurements { get; set; }

    }
}
