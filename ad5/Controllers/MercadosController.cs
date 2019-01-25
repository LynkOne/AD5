using ad5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ad5.Controllers
{
    public class MercadosController : ApiController
    {
        // GET: api/Mercados
        public IEnumerable<Mercados> Get()
        {
            var repo = new MercadosRepository();
            List<Mercados> mercados = repo.Retrieve();
            return mercados;
        }

        // GET: api/Mercados/5
        public List<Mercados> Get(int id_partido)
        {
            var repo = new MercadosRepository();
            List<Mercados> mercados = repo.Retrieve(id_partido);
            return mercados;
        }

        // POST: api/Mercados
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mercados/5
        /* public void Put([FromBody]int id_partido, [FromBody]double cuota, [FromBody]int cantidadDinero, [FromBody]int tipo_apuesta)
         {
             Debug.WriteLine("MercadoController --> ID Partido: " + cantidadDinero + " Cuota: " + cuota + " Cantidad Dinero: " + cantidadDinero + " Tipo de apuesta: " + tipo_apuesta);
             var repo = new MercadosRepository();
             repo.RetrieveMercadoById_partido(id_partido, cuota, cantidadDinero, tipo_apuesta);

         }*/
        public void Put([FromBody]Apuesta apuesta)
        {
            Debug.WriteLine("MercadoController --> ID Partido: " + apuesta.id_partido + " Cuota: " + apuesta.cuota + " Cantidad Dinero: " + apuesta.cantidadDinero + " Tipo de apuesta: " + apuesta.tipo_apuesta);
            var repo = new MercadosRepository();
            repo.RetrieveMercadoById_partido(apuesta.id_partido, apuesta.cuota, apuesta.cantidadDinero, apuesta.tipo_apuesta);

        }

        // DELETE: api/Mercados/5
        public void Delete(int id)
        {
        }
    }
}
