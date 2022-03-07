using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class MetodoDePago
    {
        [Key]
        public int M_id { get; set; }
        public string M_nombre { get; set; }


    }
}
