using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;

namespace UberViajesApiMano.Data
{
    public class ClientesData
    {
        public Conexion cnn = new Conexion();

        public void Get(out List<Cliente> Cliente)
        {
            Cliente = new List<Cliente>();
            cnn.Conectar();
            const string query = "select * from Clientes inner join DatosPersonales on Clientes.c_id_datospersonales=DatosPersonales.d_id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cliente _Cliente = new Cliente
                {
                    C_id = Convert.ToInt32(reader["c_id"]),
                };

                DatosPersonales dp = new DatosPersonales
                {
                     D_id= Convert.ToInt32(reader["d_id"]),
                     D_nombre= Convert.ToString(reader["d_nombre"]),
                     D_apellido= Convert.ToString(reader["d_apellido"]),
                     D_direccion= Convert.ToString(reader["d_direccion"]),
                     D_email= Convert.ToString(reader["d_email"]),
                     D_foto= Convert.ToString(reader["d_foto"]),
                     D_telefono= Convert.ToString(reader["d_telefono"]),
                };
                _Cliente.DatosPersonales = dp;
                Cliente.Add(_Cliente);
            }
            cnn.Desconectar();

        }
        public void Get(out Cliente _Cliente, int id)
        {
            _Cliente = new Cliente();
            cnn.Conectar();
            const string query = "select * from Clientes inner join DatosPersonales on Clientes.c_id_datospersonales=DatosPersonales.d_id where Clientes.c_id =@id ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cliente Cliente = new Cliente
                {
                    C_id = Convert.ToInt32(reader["c_id"]),
                };

                DatosPersonales dp = new DatosPersonales
                {
                    D_id = Convert.ToInt32(reader["d_id"]),
                    D_nombre = Convert.ToString(reader["d_nombre"]),
                    D_apellido = Convert.ToString(reader["d_apellido"]),
                    D_direccion = Convert.ToString(reader["d_direccion"]),
                    D_email = Convert.ToString(reader["d_email"]),
                    D_foto = Convert.ToString(reader["d_foto"]),
                    D_telefono = Convert.ToString(reader["d_telefono"]),
                };
                Cliente.DatosPersonales = dp;
                _Cliente=Cliente;
            }
            cnn.Desconectar();

        }

        internal void Post(Cliente Cliente, out Cliente _Cliente)
        {
            _Cliente = Cliente;
            cnn.Conectar();
            string query = "insert into DatosPersonales values(@d_nombre,@d_apellido,@d_telefono,@d_email,@d_direccion,@d_foto)";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@d_nombre", _Cliente.DatosPersonales.D_nombre);
            cmd.Parameters.AddWithValue("@d_apellido", _Cliente.DatosPersonales.D_apellido);
            cmd.Parameters.AddWithValue("@d_telefono", _Cliente.DatosPersonales.D_telefono);
            cmd.Parameters.AddWithValue("@d_email", _Cliente.DatosPersonales.D_email );
            cmd.Parameters.AddWithValue("@d_direccion", _Cliente.DatosPersonales.D_direccion);
            cmd.Parameters.AddWithValue("@d_foto", _Cliente.DatosPersonales.D_foto);
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            int ultimoagregado = this.Id_Ultimo();
            cnn.Conectar();
            string query2 = "insert into Clientes values(@c_id_datospersonales)";
            SqlCommand cmd2 = new SqlCommand(query2, cnn.Con());
            cmd2.Parameters.AddWithValue("@c_id_datospersonales", ultimoagregado);
            cmd2.ExecuteNonQuery();
            cnn.Desconectar();
            _Cliente = this.UltimoCliente();
        }

        private int Id_Ultimo()
        {
            int id = 0;
            cnn.Conectar();
            string query = "select top 1 d_id from DatosPersonales order by d_id desc ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["d_id"]);
            }
            cnn.Desconectar();
            return id;
        }

        private Cliente UltimoCliente()
        {
            Cliente _Cliente = new Cliente();
            cnn.Conectar();

            const string query = "select top 1 * from Clientes inner join DatosPersonales on Clientes.c_id_datospersonales=DatosPersonales.d_id order by Clientes.c_id desc";

            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cliente Cliente = new Cliente
                {
                    C_id = Convert.ToInt32(reader["c_id"]),
                };

                DatosPersonales dp = new DatosPersonales
                {
                    D_id = Convert.ToInt32(reader["d_id"]),
                    D_nombre = Convert.ToString(reader["d_nombre"]),
                    D_apellido = Convert.ToString(reader["d_apellido"]),
                    D_direccion = Convert.ToString(reader["d_direccion"]),
                    D_email = Convert.ToString(reader["d_email"]),
                    D_foto = Convert.ToString(reader["d_foto"]),
                    D_telefono = Convert.ToString(reader["d_telefono"]),
                };
                Cliente.DatosPersonales = dp;
                _Cliente = Cliente;
            }
            cnn.Desconectar();

            return _Cliente;
        }

        internal void Update(Cliente Cliente, out Cliente Clienteout, int id)
        {
            Clienteout = Cliente;
            cnn.Conectar();
            string query = "UPDATE DatosPersonales SET d_nombre = @d_nombre,d_apellido = @d_apellido,d_telefono = @d_telefono,d_email = @d_email,d_direccion = @d_direccion,d_foto = @d_foto WHERE DatosPersonales.d_id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", Clienteout.DatosPersonales.D_id);
            cmd.Parameters.AddWithValue("@d_nombre", Clienteout.DatosPersonales.D_nombre );
            cmd.Parameters.AddWithValue("@d_apellido", Clienteout.DatosPersonales.D_apellido);
            cmd.Parameters.AddWithValue("@d_telefono", Clienteout.DatosPersonales.D_telefono);
            cmd.Parameters.AddWithValue("@d_email", Clienteout.DatosPersonales.D_email);
            cmd.Parameters.AddWithValue("@d_direccion", Clienteout.DatosPersonales.D_direccion);
            cmd.Parameters.AddWithValue("@d_foto", Clienteout.DatosPersonales.D_foto);


            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            this.Get(out Clienteout, id);
        }

        internal void Borrar(int id)
        {
            try
            {
                cnn.Conectar();
                string query = "delete from Clientes where c_id=@id";
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
