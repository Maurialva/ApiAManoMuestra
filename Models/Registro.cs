using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class Registro
    {


        [Key]
        public int R_id { get; set; }
      
        [ForeignKey("r_clase_id")]
        public int R_clase_id { get; set; }
        public DateTime Vencimiento { get; set; }

    }
}

