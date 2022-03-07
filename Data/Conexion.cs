using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace UberViajesApiMano.Data
{
    public class Conexion
    {
        private readonly SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-D8TPKQ6\\SQLEXPRESS;Initial Catalog=uberfinal;Integrated Security=True");

        public void Conectar()
        {
            cnn.Open();
        }

        public void Desconectar()
        {
            cnn.Close();
        }

        public SqlConnection Con()
        {
            return cnn;
        }

    }
}
