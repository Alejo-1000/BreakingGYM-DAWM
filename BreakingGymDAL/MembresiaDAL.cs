using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BreakingGymDAL
{
    public class MembresiaDAL
    {
        public static MembresiaEN ObtenerMembresiaPorId(int id)
        {
            MembresiaEN membresia = null;
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ObtenerMembresiaPorId", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", id));

                IDataReader _reader = _comando.ExecuteReader();
                if (_reader.Read())
                {
                    membresia = new MembresiaEN
                    {
                        Id = _reader.GetInt32(0),
                        IdServicio = _reader.GetInt32(1),
                        Nombre = _reader.GetString(2),
                        Duracion = _reader.GetString(3),
                        Precio = _reader.GetInt32(4),
                        Descripcion = _reader.GetString(5)
                    };
                }

                _conn.Close();
            }

            return membresia;
        }
        public static List<MembresiaEN> MostrarMembresia()
        {
            List<MembresiaEN> _Lista = new List<MembresiaEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("MostrarMembresia", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new MembresiaEN
                    {
                        Id = _reader.GetInt32(0),
                        Nombre = _reader.GetString(1),
                        IdServicio = _reader.GetInt32(2),
                        Precio = _reader.GetInt32(3),
                        Duracion = _reader.GetString(4),
                        Descripcion = _reader.GetString(5)
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }
        public static List<MembresiaEN> BuscarMembresia(String Nombre)
        {
            List<MembresiaEN> _Lista = new List<MembresiaEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("BuscarMembresia", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new MembresiaEN
                    {
                        Id = _reader.GetInt32(0),
                        Nombre = _reader.GetString(1),
                        IdServicio = _reader.GetInt32(2),
                        Precio = _reader.GetInt32(3),
                        Duracion = _reader.GetString(4),
                        Descripcion = _reader.GetString(5)
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }

        public static int AgregarMembresia(MembresiaEN pmembresiaEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("GuardarMembresia", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;

                _comando.Parameters.Add(new SqlParameter("@Nombre", pmembresiaEN.Nombre));
                _comando.Parameters.Add(new SqlParameter("@IdServicio", pmembresiaEN.IdServicio));
                _comando.Parameters.Add(new SqlParameter("@Precio", pmembresiaEN.Precio));
                _comando.Parameters.Add(new SqlParameter("@Duracion", pmembresiaEN.Duracion));
                _comando.Parameters.Add(new SqlParameter("@Descripcion", pmembresiaEN.Descripcion));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int EliminarMembresia(MembresiaEN pmembresiaEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("EliminarMembresia", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pmembresiaEN.Id));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int ModificarMembresia(MembresiaEN pmembresiaEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ModificarMembresia", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pmembresiaEN.Id));
                _comando.Parameters.Add(new SqlParameter("@Nombre", pmembresiaEN.Nombre));
                _comando.Parameters.Add(new SqlParameter("@IdServicio", pmembresiaEN.IdServicio));
                _comando.Parameters.Add(new SqlParameter("@Precio", pmembresiaEN.Precio));
                _comando.Parameters.Add(new SqlParameter("@Duracion", pmembresiaEN.Duracion));
                _comando.Parameters.Add(new SqlParameter("@Descripcion", pmembresiaEN.Descripcion));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
    }
}

