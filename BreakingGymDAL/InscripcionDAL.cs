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
    public class InscripcionDAL
    {
        public static List<InscripcionEN> MostrarInscripcion()
        {
            List<InscripcionEN> _Lista = new List<InscripcionEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("MostrarInscripcion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new InscripcionEN
                    {
                        Id = _reader.GetInt32(0),
                        IdCliente = _reader.GetInt32(1),
                        IdMembresia = _reader.GetInt32(2),
                        IdEstado = _reader.GetInt32(3),
                        FechaInscripcion = _reader.GetDateTime(4),
                        FechaVencimiento = _reader.GetDateTime(5)
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }
        public static List<InscripcionEN> BuscarInscripcion(String idCliente)
        {
            List<InscripcionEN> _Lista = new List<InscripcionEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("BuscarInscripcion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@IdCliente", idCliente));
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new InscripcionEN
                    {
                        Id = _reader.GetInt32(0),
                        IdCliente = _reader.GetInt32(1),
                        IdMembresia = _reader.GetInt32(2),
                        IdEstado = _reader.GetInt32(3),
                        FechaInscripcion = _reader.GetDateTime(4),
                        FechaVencimiento = _reader.GetDateTime(5)
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }
        public static int AgregarInscripcion(InscripcionEN pinscripcionEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("GuardarInscripcion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@IdCliente", pinscripcionEN.IdCliente));
                _comando.Parameters.Add(new SqlParameter("@IdMembresia", pinscripcionEN.IdMembresia));
                _comando.Parameters.Add(new SqlParameter("@IdEstado", pinscripcionEN.IdEstado));
                _comando.Parameters.Add(new SqlParameter("@FechaInscripcion", pinscripcionEN.FechaInscripcion));
                _comando.Parameters.Add(new SqlParameter("@FechaVencimiento", pinscripcionEN.FechaVencimiento));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int EliminarInscripcion(InscripcionEN pinscripcionEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("EliminarInscripcion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pinscripcionEN.Id));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int ModificarInscripcion(InscripcionEN pinscripcionEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ModificarInscripcion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pinscripcionEN.Id));
                _comando.Parameters.Add(new SqlParameter("@IdCliente", pinscripcionEN.IdCliente));
                _comando.Parameters.Add(new SqlParameter("@IdMembresia", pinscripcionEN.IdMembresia));
                _comando.Parameters.Add(new SqlParameter("@IdEstado", pinscripcionEN.IdEstado));
                _comando.Parameters.Add(new SqlParameter("@FechaInscripcion", pinscripcionEN.FechaInscripcion));
                _comando.Parameters.Add(new SqlParameter("@FechaVencimiento", pinscripcionEN.FechaVencimiento));

                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
    }
}
