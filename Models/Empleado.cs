using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UberViajesApiEntity.Models
{
    public class Empleado
    {
        [Key]
        public int E_id { get; set; }
        
        [ForeignKey("e_registro_id")]
        public Registro Registro { get; set; }
        
        [ForeignKey("e_automovil_id")]
        public int E_automovil_id { get; set; }
        
        [ForeignKey("e_id_datospersonales")]
        public DatosPersonales DatosPersonales { get; set; }
    
    }
}
