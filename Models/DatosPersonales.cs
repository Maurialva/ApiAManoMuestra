using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class DatosPersonales
    {
        [Key]
        public int D_id { get; set; }
        public string D_nombre { get; set; }
        public string D_apellido { get; set; }
        public string D_telefono{ get; set; }
        public string D_email { get; set; }

        public string D_direccion { get; set; }
        public string D_foto { get; set; }
    }
}
