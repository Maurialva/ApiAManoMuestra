using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class Entrega
    {
        [Key]
        public int Ent_id { get; set; }

        public DateTime Ent_fecha { get; set; }
        
        [ForeignKey("ent_id_cliente")]
        public int Ent_id_cliente { get; set; }
        
        [ForeignKey("ent_id_empleado")]
        public int Ent_id_empleado { get; set; }
        
        [ForeignKey("ent_id_metododepago")]
        public int Ent_id_metododepago { get; set; }

    }
}
