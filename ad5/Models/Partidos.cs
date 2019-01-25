using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ad5.Models
{
    public class Partidos
    {
        public int id { get; set; }
        public String equipo_local { get; set; }
        public String equipo_visitante { get; set; }

        public Partidos(int id, string equipo_local, string equipo_visitante)
        {
            this.id = id;
            this.equipo_local = equipo_local;
            this.equipo_visitante = equipo_visitante;
        }
    }
}