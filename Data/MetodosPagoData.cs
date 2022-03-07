using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;

namespace UberViajesApiMano.Data
{
    public class MetodosPagoData
    {
        public Conexion cnn = new Conexion();

        public void Get(out List<MetodoDePago> MetodoDePago)
        {
            MetodoDePago = new List<MetodoDePago>();
            cnn.Conectar();
            const string query = "select * from MetodosDePago ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MetodoDePago _MetodoDePago = new MetodoDePago
                {
                    M_id = Convert.ToInt32(reader["m_id"]),
                    M_nombre = Convert.ToString(reader["m_nombre"]),
                };
                MetodoDePago.Add(_MetodoDePago);
            }
            cnn.Desconectar();

        }
        public void Get(out MetodoDePago _MetodoDePago, int id)
        {
            _MetodoDePago = new MetodoDePago();
            cnn.Conectar();
            const string query = "select * from MetodosDePago where m_id =@id ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MetodoDePago MetodoDePago = new MetodoDePago
                {
                    M_id = Convert.ToInt32(reader["m_id"]),
                    M_nombre = Convert.ToString(reader["m_nombre"]),
                };
                _MetodoDePago = MetodoDePago;
            }
            cnn.Desconectar();

        }

        internal void Post(MetodoDePago MetodoDePago, out MetodoDePago _MetodoDePago)
        {
            _MetodoDePago = MetodoDePago;
            cnn.Conectar();
            string query = "insert into MetodosDePago values(@m_nombre)";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@m_nombre", _MetodoDePago.M_nombre);
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            _MetodoDePago = this.UltimoMetodoDePago();
        }
        private MetodoDePago UltimoMetodoDePago()
        {
            MetodoDePago _MetodoDePago = new MetodoDePago();
            cnn.Conectar();

            const string query = "select top 1* from MetodosDePago order by m_id desc";

            SqlCommand cmd = new SqlCommand(query, cnn.Con());

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                MetodoDePago MetodoDePago = new MetodoDePago
                {
                    M_id = Convert.ToInt32(reader["m_id"]),
                    M_nombre = Convert.ToString(reader["m_nombre"]),

                };
                _MetodoDePago = MetodoDePago;

            }
            cnn.Desconectar();

            return _MetodoDePago;
        }

        internal void Update(MetodoDePago MetodoDePago, out MetodoDePago MetodoDePagout, int id)
        {
            MetodoDePagout = MetodoDePago;
            cnn.Conectar();
            string query = "UPDATE MetodosDePago SET m_nombre = @m_nombre WHERE m_id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@m_nombre", MetodoDePagout.M_nombre);


            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            this.Get(out MetodoDePagout, id);
        }

        internal void Borrar(int id)
        {
            try
            {
                cnn.Conectar();
                string query = "delete from MetodosDePago where m_id=@id";
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
