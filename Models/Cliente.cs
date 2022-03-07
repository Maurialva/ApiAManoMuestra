using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class Cliente
    {
        [Key]
        public int C_id { get; set; }

        [ForeignKey("c_id_datospersonales")]
        public DatosPersonales DatosPersonales { get; set; }

    }
}
