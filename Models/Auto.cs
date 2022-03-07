using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class Auto
    {
        [Key]
        public int A_id { get; set; }
        public string A_marca { get; set; }
        public string A_modelo{ get; set; }
        public int A_generacion { get; set; }
        public string A_patente { get; set; }

    }
}
