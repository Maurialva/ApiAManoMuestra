using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;

namespace UberViajesApiMano.Data
{
    public class EntregasData
    {
        public Conexion cnn = new Conexion();

        public void Get(out List<Entrega> Entrega)
        {
            Entrega = new List<Entrega>();
            cnn.Conectar();
            const string query = "select* from entregas";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entrega _Entrega = new Entrega
                {
                    Ent_id = Convert.ToInt32(reader["ent_id"]),
                    Ent_fecha = Convert.ToDateTime(reader["ent_fecha"]),
                    Ent_id_cliente = Convert.ToInt32(reader["ent_id_cliente"]),
                    Ent_id_empleado = Convert.ToInt32(reader["ent_id_empleado"]),
                    Ent_id_metododepago = Convert.ToInt32(reader["ent_id_metododepago"]),
                };
                Entrega.Add(_Entrega);
            }
            cnn.Desconectar();

        }
        public void Get(out Entrega _Entrega, int id)
        {
            _Entrega = new Entrega();
            cnn.Conectar();
            const string query = "select * from Entregas where Entregas.ent_id =@id ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Entrega Entrega = new Entrega
                {
                    Ent_id = Convert.ToInt32(reader["ent_id"]),
                    Ent_fecha = Convert.ToDateTime(reader["ent_fecha"]),
                    Ent_id_cliente = Convert.ToInt32(reader["ent_id_cliente"]),
                    Ent_id_empleado = Convert.ToInt32(reader["ent_id_empleado"]),
                    Ent_id_metododepago = Convert.ToInt32(reader["ent_id_metododepago"]),
                };
                _Entrega = Entrega;
            }
            cnn.Desconectar();

        }

        internal void Post(Entrega Entrega, out Entrega _Entrega)
        {
            _Entrega = Entrega;
            cnn.Conectar();
            string query = "insert into entregas values(@ent_fecha,@ent_id_cliente,@ent_id_empleado,@ent_id_metododepago)";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@ent_fecha", _Entrega.Ent_fecha);
            cmd.Parameters.AddWithValue("@ent_id_cliente", _Entrega.Ent_id_cliente);
            cmd.Parameters.AddWithValue("@ent_id_empleado", _Entrega.Ent_id_empleado);
            cmd.Parameters.AddWithValue("@ent_id_metododepago", _Entrega.Ent_id_metododepago);
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            _Entrega = this.UltimaEntrega();
        }

        private Entrega UltimaEntrega()
        {
            Entrega _Entrega = new Entrega();
            cnn.Conectar();

            const string query = "select top 1 * from Entregas  order by Entregas.ent_id desc";

            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Entrega Entrega = new Entrega
                {
                    Ent_id = Convert.ToInt32(reader["ent_id"]),
                    Ent_fecha = Convert.ToDateTime(reader["ent_fecha"]),
                    Ent_id_cliente = Convert.ToInt32(reader["ent_id_cliente"]),
                    Ent_id_empleado = Convert.ToInt32(reader["ent_id_empleado"]),
                    Ent_id_metododepago = Convert.ToInt32(reader["ent_id_metododepago"]),
                };
                _Entrega = Entrega;
            }
            cnn.Desconectar();

            return _Entrega;
        }

        internal void Update(Entrega Entrega, out Entrega Entregaout, int id)
        {
            Entregaout = Entrega;
            cnn.Conectar();
            string query = "UPDATE entregas SET ent_fecha = @ent_fecha,ent_id_cliente = @ent_id_cliente,ent_id_empleado = @ent_id_empleado,ent_id_metododepago = @ent_id_metododepago WHERE entregas.ent_id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ent_fecha", Entregaout.Ent_fecha);
            cmd.Parameters.AddWithValue("@ent_id_cliente", Entregaout.Ent_id_cliente);
            cmd.Parameters.AddWithValue("@ent_id_empleado", Entregaout.Ent_id_empleado);
            cmd.Parameters.AddWithValue("@ent_id_metododepago", Entregaout.Ent_id_metododepago);

            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            this.Get(out Entregaout, id);
        }

        internal void Borrar(int id)
        {
            try
            {
                cnn.Conectar();
                string query = "delete from entregas where ent_id=@id";
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
