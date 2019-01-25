using ad5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ad5.Controllers
{
    public class PartidosController : ApiController
    {
        // GET: api/Partidos
        public IEnumerable<Partidos> Get()
        {
            var repo = new PartidosRepository();
            List<Partidos> partidos = repo.Retrieve();
            return partidos;
        }

        // GET: api/Partidos/nombre_equipo
        public IEnumerable<Partidos> Get(String nombre)
        {
            var repo = new PartidosRepository();
            List<Partidos> partidos = repo.Retrieve(nombre);
            return partidos;
        }

        // GET: api/Partidos/5
        public string Get(int id)
        {
            return null;
        }

        // POST: api/Partidos
        public void Post([FromBody]Partidos partido)
        {
            var repo = new PartidosRepository();
            repo.Save(partido);

        }

        // PUT: api/Partidos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Partidos/5
        public void Delete(int id)
        {
        }
    }
}
