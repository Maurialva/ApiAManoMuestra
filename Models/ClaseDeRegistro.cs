using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class ClaseDeRegistro
    {
        [Key]
        public int Clase_id { get; set; }
        public string Clase_nombre { get; set; }
    }
}
