using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;
using UberViajesApiMano.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberViajesApiMano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        EmpleadosData cnn = new EmpleadosData();

        // GET: api/<EmpleadosController>
        [HttpGet]
        public IEnumerable<Empleado> Get()
        {
            List<Empleado> Empleado = new List<Empleado>();
            cnn.Get(out Empleado);
            return Empleado;

        }

        // GET api/<EmpleadosController>/5
        [HttpGet("{id}")]
        public Empleado Get(int id)
        {
            Empleado Empleado = new Empleado();
            cnn.Get(out Empleado, id);
            return Empleado;
        }

        // POST api/<EmpleadosController>
        [HttpPost]
        public ActionResult Post(Empleado Empleado)
        {
            try
            {
                cnn.Post(Empleado, out Empleado);
                return Ok(Empleado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<EmpleadosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Empleado Empleado)
        {
            try
            {
                cnn.Update(Empleado, out Empleado, id);
                return Ok(Empleado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EmpleadosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnn.Borrar(id);

        }
    }
}
