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
    public class ClientesController : ControllerBase
    {
        ClientesData cnn = new ClientesData();

        // GET: api/<ClientesController>
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            List<Cliente> Cliente = new List<Cliente>();
            cnn.Get(out Cliente);
            return Cliente;

        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public Cliente Get(int id)
        {
            Cliente Cliente = new Cliente();
            cnn.Get(out Cliente, id);
            return Cliente;
        }

        // POST api/<ClientesController>
        [HttpPost]
        public ActionResult Post(Cliente Cliente)
        {
            try
            {
                cnn.Post(Cliente, out Cliente);
                return Ok(Cliente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Cliente Cliente)
        {
            try
            {
                cnn.Update(Cliente, out Cliente, id);
                return Ok(Cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnn.Borrar(id);

        }
    }
}
