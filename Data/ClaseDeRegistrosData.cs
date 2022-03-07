using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;

namespace UberViajesApiMano.Data
{
    public class ClaseDeRegistrosData
    {
        public Conexion cnn = new Conexion();

        public void Get(out List<ClaseDeRegistro> ClaseDeRegistro)
        {
            ClaseDeRegistro = new List<ClaseDeRegistro>();
            cnn.Conectar();
            const string query = "select * from ClasesRegistros ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ClaseDeRegistro _ClaseDeRegistro = new ClaseDeRegistro
                {
                    Clase_id = Convert.ToInt32(reader["clase_id"]),
                    Clase_nombre = Convert.ToString(reader["clase_nombre"]),
                };
                ClaseDeRegistro.Add(_ClaseDeRegistro);
            }
            cnn.Desconectar();

        }
        public void Get(out ClaseDeRegistro _ClaseDeRegistro, int id)
        {
            _ClaseDeRegistro = new ClaseDeRegistro();
            cnn.Conectar();
            const string query = "select * from ClasesRegistros where clase_id =@id ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ClaseDeRegistro ClaseDeRegistro = new ClaseDeRegistro
                {
                    Clase_id = Convert.ToInt32(reader["clase_id"]),
                    Clase_nombre = Convert.ToString(reader["clase_nombre"]),
                };
                _ClaseDeRegistro = ClaseDeRegistro;
            }
            cnn.Desconectar();

        }

        internal void Post(ClaseDeRegistro ClaseDeRegistro, out ClaseDeRegistro _ClaseDeRegistro)
        {
            _ClaseDeRegistro = ClaseDeRegistro;
            cnn.Conectar();
            string query = "insert into ClasesRegistros values(@clase_nombre)";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@clase_nombre", _ClaseDeRegistro.Clase_nombre);
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            _ClaseDeRegistro = this.UltimoClaseDeRegistro();
        }
        private ClaseDeRegistro UltimoClaseDeRegistro()
        {
            ClaseDeRegistro _ClaseDeRegistro = new ClaseDeRegistro();
            cnn.Conectar();

            const string query = "select top 1* from ClasesRegistros order by clase_id desc";

            SqlCommand cmd = new SqlCommand(query, cnn.Con());

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                ClaseDeRegistro ClaseDeRegistro = new ClaseDeRegistro
                {
                    Clase_id = Convert.ToInt32(reader["clase_id"]),
                    Clase_nombre = Convert.ToString(reader["clase_nombre"]),

                };
                _ClaseDeRegistro = ClaseDeRegistro;

            }
            cnn.Desconectar();

            return _ClaseDeRegistro;
        }

        internal void Update(ClaseDeRegistro ClaseDeRegistro, out ClaseDeRegistro ClaseDeRegistrout, int id)
        {
            ClaseDeRegistrout = ClaseDeRegistro;
            cnn.Conectar();
            string query = "UPDATE ClasesRegistros SET clase_nombre = @clase_nombre WHERE clase_id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@clase_nombre", ClaseDeRegistrout.Clase_nombre);
 

            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            this.Get(out ClaseDeRegistrout, id);
        }

        internal void Borrar(int id)
        {
            try
            {
                cnn.Conectar();
                string query = "delete from ClasesRegistros where clase_id=@id";
                SqlCommand cmd = new SqlCommand(query, cnn.Con());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cnn.Desconectar();
            }
            catch (Exception ex)
            {

            }
        }
       
       

    }
}

