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
    public class RegistroAsistenciaDAL
    {
        public static List<RegistroAsistenciaEN> MostrarAsistencia()
        {
            List<RegistroAsistenciaEN> _Lista = new List<RegistroAsistenciaEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("MostrarAsistenciasConCliente", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new RegistroAsistenciaEN
                    {
                        Id = _reader.GetInt32(0),
                        IdCliente = _reader.GetInt32(1),
                        Nombre = _reader.GetString(2),
                        Apellido = _reader.GetString(3),
                        TarjetaRFID = _reader.GetString(4),
                        FechaAsistencia = _reader.GetDateTime(5),
                        HoraEntrada = (TimeSpan)_reader.GetValue(6)  // ← cambio aquí
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }
        public static List<RegistroAsistenciaEN> BuscarAsistencia(DateTime fechaAsistencia)
        {
            List<RegistroAsistenciaEN> _Lista = new List<RegistroAsistenciaEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("BuscarAsistencia", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;

                // ✅ Ahora el parámetro se pasa como DateTime
                _comando.Parameters.Add(new SqlParameter("@fechaAsistencia", fechaAsistencia));

                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new RegistroAsistenciaEN
                    {
                        Id = _reader.GetInt32(0),
                        IdCliente = _reader.GetInt32(1),
                        Nombre = _reader.GetString(2),
                        Apellido = _reader.GetString(3),
                        TarjetaRFID = _reader.GetString(4),
                        FechaAsistencia = _reader.GetDateTime(5),
                        HoraEntrada = (TimeSpan)_reader.GetValue(6)
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }
        public static int RegistrarAsistenciaPorTarjeta(string tarjetaRFID)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("RegistrarAsistenciaPorTarjeta", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@TarjetaRFID", tarjetaRFID));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
    }
}
