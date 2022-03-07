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
    public class EntregasController : ControllerBase
    {
        EntregasData cnn = new EntregasData();

        // GET: api/<EntregasController>
        [HttpGet]
        public IEnumerable<Entrega> Get()
        {
            List<Entrega> Entrega = new List<Entrega>();
            cnn.Get(out Entrega);
            return Entrega;

        }

        // GET api/<EntregasController>/5
        [HttpGet("{id}")]
        public Entrega Get(int id)
        {
            Entrega Entrega = new Entrega();
            cnn.Get(out Entrega, id);
            return Entrega;
        }

        // POST api/<EntregasController>
        [HttpPost]
        public ActionResult Post(Entrega Entrega)
        {
            try
            {
                cnn.Post(Entrega, out Entrega);
                return Ok(Entrega);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<EntregasController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Entrega Entrega)
        {
            try
            {
                cnn.Update(Entrega, out Entrega, id);
                return Ok(Entrega);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EntregasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnn.Borrar(id);

        }
    }
}
