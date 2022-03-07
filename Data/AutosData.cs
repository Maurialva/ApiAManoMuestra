using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;

namespace UberViajesApiMano.Data
{
    public class AutosData
    {
        public Conexion cnn = new Conexion();


        public void Get(out List<Auto>autos)
        {
            autos = new List<Auto>();
            cnn.Conectar();
            const string query = "select * from Autos ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Auto Auto = new Auto
                {
                    A_id = Convert.ToInt32(reader["a_id"]),
                    A_generacion = Convert.ToInt32(reader["a_generacion"]),
                    A_marca = Convert.ToString(reader["a_marca"]),
                    A_modelo = Convert.ToString(reader["a_modelo"]),
                    A_patente = Convert.ToString(reader["a_patente"]),
                };
                autos.Add(Auto);
            }
            cnn.Desconectar();

        }

        internal void Post(Auto Auto,out Auto auto)
        {
            auto = Auto;
            cnn.Conectar();
            string query = "insert into Autos values(@a_marca,@a_modelo,@a_generacion,@a_patente)";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@a_marca", auto.A_marca);
            cmd.Parameters.AddWithValue("@a_modelo", auto.A_modelo);
            cmd.Parameters.AddWithValue("@a_generacion", auto.A_generacion);
            cmd.Parameters.AddWithValue("@a_patente", auto.A_patente);
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            auto = this.UltimoAuto();
        }

        internal void Update(Auto auto, out Auto autout, int id)
        {
            autout = auto;
            cnn.Conectar();
            string query = "UPDATE Autos SET a_marca = @a_marca, a_modelo = @a_modelo, a_generacion=@a_generacion,a_patente=@a_patente WHERE a_id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@a_marca", auto.A_marca);
            cmd.Parameters.AddWithValue("@a_modelo", auto.A_modelo);
            cmd.Parameters.AddWithValue("@a_generacion",auto.A_generacion );
            cmd.Parameters.AddWithValue("@a_patente",auto.A_patente);

            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            this.Get(out autout, id);
        }

        internal void Borrar(int id)
        {
            try
            {
                cnn.Conectar();
                string query = "delete from Autos where a_id=@id";
                SqlCommand cmd = new SqlCommand(query, cnn.Con());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cnn.Desconectar();
            }
            catch (Exception ex)
            {

            }
        }
        private Auto UltimoAuto()
        {
            Auto _auto = new Auto();
            cnn.Conectar();

            const string query = "select top 1* from Autos order by Autos.a_id desc";

            SqlCommand cmd = new SqlCommand(query, cnn.Con());

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                Auto Auto = new Auto
                {
                    A_id = Convert.ToInt32(reader["a_id"]),
                    A_generacion = Convert.ToInt32(reader["a_generacion"]),
                    A_marca = Convert.ToString(reader["a_marca"]),
                    A_modelo = Convert.ToString(reader["a_modelo"]),
                    A_patente = Convert.ToString(reader["a_patente"]),
                };
                _auto = Auto;
                
            }
            cnn.Desconectar();

            return _auto;
        }

        public void Get(out Auto auto,int id)
        {
            auto = new Auto();
            cnn.Conectar();
            const string query = "select * from Autos where Autos.a_id =@id ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Auto Auto = new Auto
                {
                    A_id = Convert.ToInt32(reader["a_id"]),
                    A_generacion = Convert.ToInt32(reader["a_generacion"]),
                    A_marca = Convert.ToString(reader["a_marca"]),
                    A_modelo = Convert.ToString(reader["a_modelo"]),
                    A_patente = Convert.ToString(reader["a_patente"]),
                };
                auto=Auto;
            }
            cnn.Desconectar();

        }

    }
}
