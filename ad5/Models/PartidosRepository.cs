using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ad5.Models
{
    public class PartidosRepository
    {
        Random r = new Random();
        private MySqlConnection Connect()
        {
            string connString = "Server=localhost;Port=3306;Database=ad5ab;Uid=root;password=;SslMode=none";
            MySqlConnection con = new MySqlConnection(connString);
            return con;
        }
        internal List<Partidos> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from partidos";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();
                Partidos p = null;
                List<Partidos> partidos = new List<Partidos>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetString(2));
                    p = new Partidos(res.GetInt32(0), res.GetString(1), res.GetString(2));
                    partidos.Add(p);
                }
                con.Close();
                return partidos;
            }
            catch
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                return null;
            }
        }
        internal List<Partidos> Retrieve(String nombre)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM partidos WHERE equipo_local LIKE '"+nombre+"%' OR equipo_visitante LIKE '"+nombre+"%'";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();
                Partidos p = null;
                List<Partidos> partidos = new List<Partidos>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetString(2));
                    p = new Partidos(res.GetInt32(0), res.GetString(1), res.GetString(2));
                    partidos.Add(p);
                }
                con.Close();
                return partidos;
            }
            catch
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                return null;
            }
        }
        internal void Save(Partidos partido)
        {
            

            double rand1 = generarRandom();
            double rand2 = generarRandom();
            double rand3 = generarRandom();
            double rand4 = generarRandom();
            double rand5 = generarRandom();
            double rand6 = generarRandom();


            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO partidos(equipo_local, equipo_visitante) VALUES ('"+partido.equipo_local+"','"+partido.equipo_visitante+"');";

            Debug.WriteLine("comando " + command.CommandText);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión "+e.Message);
            }
            command.CommandText = "INSERT INTO mercados(overunder, cuota_over, cuota_under, dinero_over, dinero_under, id_partido) VALUES (1.5, "+ rand1 + ", " + rand2 + ", 0, 0, (SELECT id FROM partidos WHERE equipo_local='" + partido.equipo_local+ "' AND equipo_visitante='" + partido.equipo_visitante +"')); ";
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
            }
            command.CommandText = "INSERT INTO mercados(overunder, cuota_over, cuota_under, dinero_over, dinero_under, id_partido) VALUES (2.5, " + rand3 + ", " + rand4 + ", 0, 0, (SELECT id FROM partidos WHERE equipo_local='" + partido.equipo_local + "' AND equipo_visitante='" + partido.equipo_visitante + "')); ";
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
            }
            command.CommandText = "INSERT INTO mercados(overunder, cuota_over, cuota_under, dinero_over, dinero_under, id_partido) VALUES (3.5, " + rand5 + ", " + rand6 + ", 0, 0, (SELECT id FROM partidos WHERE equipo_local='" + partido.equipo_local + "' AND equipo_visitante='" + partido.equipo_visitante + "')); ";
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
            }
        }

        internal double generarRandom()
        {
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");
            culInfo.NumberFormat.NumberDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;

            int ra = r.Next(120, 430);
            Debug.WriteLine("Random: "+ra);
            double rand = ((double)ra / (double)100);
            Debug.WriteLine("Random dividido: "+rand);
            return rand;
        }




    }




}