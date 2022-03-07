using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UberViajesApiEntity.Models;

namespace UberViajesApiMano.Data
{
    public class EmpleadosData
    {
        public Conexion cnn = new Conexion();

        public void Get(out List<Empleado> Empleado)
        {
            Empleado = new List<Empleado>();
            cnn.Conectar();
            const string query = "select * from Empleados inner join DatosPersonales on Empleados.e_id_datospersonales=DatosPersonales.d_id inner join Registros on Empleados.e_registro_id=Registros.r_id inner join Autos on Empleados.e_automovil_id = Autos.a_id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Empleado _Empleado = new Empleado
                {
                    E_id = Convert.ToInt32(reader["e_id"]),
                    E_automovil_id= Convert.ToInt32(reader["e_automovil_id"]),
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
                Registro rg = new Registro
                {
                    R_id= Convert.ToInt32(reader["r_id"]),
                    R_clase_id= Convert.ToInt32(reader["r_clase_id"]),
                    Vencimiento= Convert.ToDateTime(reader["vencimiento"]),
                };
                
                _Empleado.DatosPersonales = dp;
                _Empleado.Registro = rg;
                Empleado.Add(_Empleado);
            }
            cnn.Desconectar();

        }
        public void Get(out Empleado _Empleado, int id)
        {
            _Empleado = new Empleado();
            cnn.Conectar();
            const string query = "select * from Empleados inner join DatosPersonales on Empleados.e_id_datospersonales=DatosPersonales.d_id inner join Registros on Empleados.e_registro_id=Registros.r_id where Empleados.e_id =@id ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Empleado Empleado = new Empleado
                {
                    E_id = Convert.ToInt32(reader["e_id"]),
                    E_automovil_id = Convert.ToInt32(reader["e_automovil_id"]),
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
                Registro rg = new Registro
                {
                    R_id = Convert.ToInt32(reader["r_id"]),
                    R_clase_id = Convert.ToInt32(reader["r_clase_id"]),
                    Vencimiento = Convert.ToDateTime(reader["vencimiento"]),
                };

                Empleado.DatosPersonales = dp;
                Empleado.Registro = rg;
                _Empleado = Empleado;
            }
            cnn.Desconectar();

        }

        internal void Post(Empleado Empleado, out Empleado _Empleado)
        {
            _Empleado = Empleado;
            cnn.Conectar();
            string query = "insert into DatosPersonales values(@d_nombre,@d_apellido,@d_telefono,@d_email,@d_direccion,@d_foto)";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@d_nombre", _Empleado.DatosPersonales.D_nombre);
            cmd.Parameters.AddWithValue("@d_apellido", _Empleado.DatosPersonales.D_apellido);
            cmd.Parameters.AddWithValue("@d_telefono", _Empleado.DatosPersonales.D_telefono);
            cmd.Parameters.AddWithValue("@d_email", _Empleado.DatosPersonales.D_email);
            cmd.Parameters.AddWithValue("@d_direccion", _Empleado.DatosPersonales.D_direccion);
            cmd.Parameters.AddWithValue("@d_foto", _Empleado.DatosPersonales.D_foto);
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            int ultimoagregado = this.Id_Ultimo();
            cnn.Conectar();
            string query2 = "insert into Registros values(@r_clase_id,@vencimiento)";
            SqlCommand cmd2 = new SqlCommand(query2, cnn.Con());
            cmd2.Parameters.AddWithValue("@r_clase_id", _Empleado.Registro.R_clase_id);
            cmd2.Parameters.AddWithValue("@vencimiento", _Empleado.Registro.Vencimiento);
            cmd2.ExecuteNonQuery();
            cnn.Desconectar();
            int Reg_Ultimo = this.Reg_Ultimo();
            cnn.Conectar();
            string query3 = "insert into Empleados values(@e_registro_id,@e_automovil_id,@e_id_datospersonales)";
            SqlCommand cmd3 = new SqlCommand(query3, cnn.Con());
            cmd3.Parameters.AddWithValue("@e_id_datospersonales", ultimoagregado);
            cmd3.Parameters.AddWithValue("@e_registro_id", Reg_Ultimo);
            cmd3.Parameters.AddWithValue("@e_automovil_id", _Empleado.E_automovil_id);
            cmd3.ExecuteNonQuery();
            cnn.Desconectar();
            _Empleado = this.UltimoEmpleado();
        }

        private int Reg_Ultimo()
        {
            int id = 0;
            cnn.Conectar();
            string query = "select top 1 r_id from Registros order by r_id desc ";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["r_id"]);
            }
            cnn.Desconectar();
            return id;
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

        private Empleado UltimoEmpleado()
        {
            Empleado _Empleado = new Empleado();
            cnn.Conectar();

            const string query = "select top 1 * from Empleados inner join DatosPersonales on Empleados.e_id_datospersonales=DatosPersonales.d_id inner join Registros on Empleados.e_registro_id=Registros.r_id  order by Empleados.e_id desc";

            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Empleado Empleado = new Empleado
                {
                    E_id = Convert.ToInt32(reader["e_id"]),
                    E_automovil_id = Convert.ToInt32(reader["e_automovil_id"]),
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
                Registro rg = new Registro
                {
                    R_id = Convert.ToInt32(reader["r_id"]),
                    R_clase_id = Convert.ToInt32(reader["r_clase_id"]),
                    Vencimiento = Convert.ToDateTime(reader["vencimiento"]),
                };

                Empleado.DatosPersonales = dp;
                Empleado.Registro = rg;
                _Empleado = Empleado;
            }
            cnn.Desconectar();

            return _Empleado;
        }

        internal void Update(Empleado Empleado, out Empleado Empleadoout, int id)
        {
            Empleadoout = Empleado;
            cnn.Conectar();
            string query = "UPDATE DatosPersonales SET d_nombre = @d_nombre,d_apellido = @d_apellido,d_telefono = @d_telefono,d_email = @d_email,d_direccion = @d_direccion,d_foto = @d_foto WHERE DatosPersonales.d_id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn.Con());
            cmd.Parameters.AddWithValue("@id", Empleadoout.DatosPersonales.D_id);
            cmd.Parameters.AddWithValue("@d_nombre", Empleadoout.DatosPersonales.D_nombre);
            cmd.Parameters.AddWithValue("@d_apellido", Empleadoout.DatosPersonales.D_apellido);
            cmd.Parameters.AddWithValue("@d_telefono", Empleadoout.DatosPersonales.D_telefono);
            cmd.Parameters.AddWithValue("@d_email", Empleadoout.DatosPersonales.D_email);
            cmd.Parameters.AddWithValue("@d_direccion", Empleadoout.DatosPersonales.D_direccion);
            cmd.Parameters.AddWithValue("@d_foto", Empleadoout.DatosPersonales.D_foto);

            string query2 = "UPDATE Registros SET vencimiento = @vencimiento,r_clase_id = @r_clase_id WHERE Registros.r_id=@id";
            SqlCommand cmd2 = new SqlCommand(query2, cnn.Con());
            cmd2.Parameters.AddWithValue("@id", Empleadoout.Registro.R_id);
            cmd2.Parameters.AddWithValue("@vencimiento", Empleadoout.Registro.Vencimiento);
            cmd2.Parameters.AddWithValue("@r_clase_id", Empleadoout.Registro.R_clase_id);


            string query3 = "UPDATE Empleados SET e_automovil_id = @e_automovil_id WHERE Empleados.e_id=@id";
            SqlCommand cmd3 = new SqlCommand(query3, cnn.Con());
            cmd3.Parameters.AddWithValue("@id", Empleadoout.E_id);
            cmd3.Parameters.AddWithValue("@e_automovil_id", Empleadoout.E_automovil_id);

            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cnn.Desconectar();
            this.Get(out Empleadoout, id);
        }

        internal void Borrar(int id)
        {
            try
            {
                cnn.Conectar();
                string query = "delete from Empleados where e_id=@id";
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
