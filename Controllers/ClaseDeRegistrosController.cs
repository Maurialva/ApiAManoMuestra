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
    public class ClaseDeRegistrosController : ControllerBase
    {
        ClaseDeRegistrosData cnn = new ClaseDeRegistrosData();

        // GET: api/<ClaseDeRegistrosController>
        [HttpGet]
        public IEnumerable<ClaseDeRegistro> Get()
        {
            List<ClaseDeRegistro> ClaseDeRegistro = new List<ClaseDeRegistro>();
            cnn.Get(out ClaseDeRegistro);
            return ClaseDeRegistro;

        }

        // GET api/<ClaseDeRegistrosController>/5
        [HttpGet("{id}")]
        public ClaseDeRegistro Get(int id)
        {
            ClaseDeRegistro ClaseDeRegistro = new ClaseDeRegistro();
            cnn.Get(out ClaseDeRegistro, id);
            return ClaseDeRegistro;
        }

        // POST api/<ClaseDeRegistrosController>
        [HttpPost]
        public ActionResult Post(ClaseDeRegistro ClaseDeRegistro)
        {
            try
            {
                cnn.Post(ClaseDeRegistro, out ClaseDeRegistro);
                return Ok(ClaseDeRegistro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ClaseDeRegistrosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, ClaseDeRegistro ClaseDeRegistro)
        {
            try
            {
                cnn.Update(ClaseDeRegistro, out ClaseDeRegistro, id);
                return Ok(ClaseDeRegistro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ClaseDeRegistrosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnn.Borrar(id);

        }
    }
}
