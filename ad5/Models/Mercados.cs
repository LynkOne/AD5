using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ad5.Models
{
    public class Mercados

    {
        public Mercados(int id_mercado, double overunder, double cuota_over, double cuota_under, double dinero_over, double dinero_under, int id_partido)
        {
            this.id_mercado = id_mercado;
            this.overunder = overunder;
            this.cuota_over = cuota_over;
            this.cuota_under = cuota_under;
            this.dinero_over = dinero_over;
            this.dinero_under = dinero_under;
            this.id_partido = id_partido;
        }

        public int id_mercado { get; set; }
        public double overunder { get; set; }
        public double cuota_over { get; set; }
        public double cuota_under { get; set; }
        public double dinero_over { get; set; }
        public double dinero_under { get; set; }
        public int id_partido { get; set; }

    }

    
}