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
    public class AutosController : ControllerBase
    {
        AutosData cnn = new  AutosData(); 

        // GET: api/<AutosController>
        [HttpGet]
        public IEnumerable<Auto> Get()
        {
            List<Auto> autos = new List<Auto>();
            cnn.Get(out autos);
            return autos;
        
        }

        // GET api/<AutosController>/5
        [HttpGet("{id}")]
        public Auto Get(int id)
        {
            Auto auto = new Auto();
            cnn.Get(out auto, id);
            return auto;
        }

        // POST api/<AutosController>
        [HttpPost]
        public ActionResult Post(Auto auto)
        {
            try
            {
                cnn.Post(auto,out auto);
                return Ok(auto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<AutosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id,Auto Auto)
        {
            try
            {
                cnn.Update(Auto, out Auto, id);
                return Ok(Auto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<AutosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnn.Borrar(id);

        }
    }
}
