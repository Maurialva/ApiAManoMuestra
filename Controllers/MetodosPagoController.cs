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
    public class MetodosPagoController : ControllerBase
    {
        MetodosPagoData cnn = new MetodosPagoData();

        // GET: api/<MetodoDePagosController>
        [HttpGet]
        public IEnumerable<MetodoDePago> Get()
        {
            List<MetodoDePago> MetodoDePago = new List<MetodoDePago>();
            cnn.Get(out MetodoDePago);
            return MetodoDePago;

        }

        // GET api/<MetodoDePagosController>/5
        [HttpGet("{id}")]
        public MetodoDePago Get(int id)
        {
            MetodoDePago MetodoDePago = new MetodoDePago();
            cnn.Get(out MetodoDePago, id);
            return MetodoDePago;
        }

        // POST api/<MetodoDePagosController>
        [HttpPost]
        public ActionResult Post(MetodoDePago MetodoDePago)
        {
            try
            {
                cnn.Post(MetodoDePago, out MetodoDePago);
                return Ok(MetodoDePago);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<MetodoDePagosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, MetodoDePago MetodoDePago)
        {
            try
            {
                cnn.Update(MetodoDePago, out MetodoDePago, id);
                return Ok(MetodoDePago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<MetodoDePagosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnn.Borrar(id);

        }
    }
}
