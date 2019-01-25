using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ad5.Models
{
    public class Apuesta
    {
       
        public int id_partido { get; set; }
        public double cuota { get; set; }
        public int cantidadDinero { get; set; }
        public int tipo_apuesta { get; set; }

        public Apuesta(int id_partido, double cuota, int cantidadDinero, int tipo_apuesta)
        {
            this.id_partido = id_partido;
            this.cuota = cuota;
            this.cantidadDinero = cantidadDinero;
            this.tipo_apuesta = tipo_apuesta;
        }
    }
}