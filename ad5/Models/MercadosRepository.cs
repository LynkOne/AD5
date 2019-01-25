﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ad5.Models
{
    public class MercadosRepository
    {
        private MySqlConnection Connect()
        {
            string connString = "Server=localhost;Port=3306;Database=ad5ab;Uid=root;password=;SslMode=none";
            MySqlConnection con = new MySqlConnection(connString);
            return con;
        }
        internal List<Mercados> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from mercados";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();
                Mercados m = null;
                List<Mercados> mercados = new List<Mercados>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetDouble(4) + " " + res.GetDouble(5) + " " + res.GetInt32(6));
                    m = new Mercados(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt32(6));
                    mercados.Add(m);
                }
                con.Close();
                return mercados;
            }
            catch
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                return null;
            }
        }
        internal List<Mercados> Retrieve(int id_partido)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from mercados where id_partido="+id_partido;

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();
                Mercados m = null;
                List<Mercados> mercados = new List<Mercados>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetDouble(4) + " " + res.GetDouble(5) + " " + res.GetInt32(6));
                    m = new Mercados(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt32(6));
                    mercados.Add(m);
                }
                con.Close();
                return mercados;
            }
            catch
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                return null;
            }
        }

        //MÉTODO PARA APOSTAR Y DONDE
        internal void RetrieveMercadoById_partido(int id_partido, double cuota, int cantidadDinero, int tipo_apuesta)
        {
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");
            culInfo.NumberFormat.NumberDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from mercados where id_partido =" + id_partido + "  AND overunder =" + cuota + ";";
           
            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                Mercados m = null;
                

                if (res.Read())
                {
                    m = new Mercados(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt32(6));
                }


                con.Close();

                switch (tipo_apuesta)
                {
                    case 1:
                        //Apostando a over
                        UpdateMercadoOver(m, cantidadDinero);
                        Debug.WriteLine("Apostando a over: " + cantidadDinero);
                        break;
                    case 2:
                        //Apostando a under
                        UpdateMercadoUnder(m, cantidadDinero);
                        Debug.WriteLine("Apostando a under: " + cantidadDinero);
                        break;
                    default:
                        Debug.WriteLine("Error de usuario gilipollas");
                        break;
                }
                
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Error: " + e.Message);
                
            }
        }
        internal void UpdateMercadoOver(Mercados mercados, int dineroOver)
        {
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");
            culInfo.NumberFormat.NumberDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;

            double ProbabilidadOver;
            double ProbabilidadUnder;
            double dineroUnder = mercados.dinero_under;
            if (dineroUnder == 0)
            {
                dineroUnder = 1;
            }
            double cuotaOver = mercados.cuota_over;
            double cuotaUnder = mercados.cuota_under;
            ProbabilidadOver = dineroOver / (dineroOver + dineroUnder);
            ProbabilidadUnder = dineroUnder / (dineroOver + dineroUnder);
            cuotaOver = (1 / ProbabilidadOver) * 0.95;
            cuotaUnder = (1 / ProbabilidadUnder) * 0.95;
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE mercados SET cuota_over =" + cuotaOver + ",cuota_under = " + cuotaUnder + ",dinero_over = " + dineroOver + " WHERE id = " + mercados.id_mercado + ";";
            Debug.WriteLine(command.CommandText);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de MySql: " + e.Message);
            }
        }

        internal void UpdateMercadoUnder(Mercados mercados, int dineroUnder)
        {
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");
            culInfo.NumberFormat.NumberDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;

            double ProbabilidadOver;
            double ProbabilidadUnder;
            double dineroOver = mercados.dinero_over;
            if (dineroOver == 0)
            {
                dineroOver = 1;
            }
            double cuotaOver = mercados.cuota_over;
            double cuotaUnder = mercados.cuota_under;
            ProbabilidadOver = dineroOver / (dineroOver + dineroUnder);
            ProbabilidadUnder = dineroUnder / (dineroOver + dineroUnder);
            cuotaUnder = (1 / ProbabilidadUnder) * 0.95;
            cuotaOver = (1 / ProbabilidadOver) * 0.95;
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE mercados SET cuota_over =" + cuotaOver + ",cuota_under = " + cuotaUnder + ",dinero_under = " + dineroUnder + " WHERE id = " + mercados.id_mercado + ";";
            Debug.WriteLine(command.CommandText);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de MySql: " + e.Message);
            }
        }




    }
}