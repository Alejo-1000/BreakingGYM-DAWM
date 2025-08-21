using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreakingGymEN;
using Microsoft.Data.SqlClient;
namespace BreakingGymDAL
{
    public class ClienteDAL
    {
        public static List<ClienteEN> MostrarCliente() { List<ClienteEN> _Lista = new List<ClienteEN>(); 
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer)) 
            { _conn.Open(); SqlCommand _comando = new SqlCommand("MostrarCliente", _conn as SqlConnection); 
                _comando.CommandType = CommandType.StoredProcedure; IDataReader _reader = 
                    _comando.ExecuteReader(); while (_reader.Read()) { _Lista.Add(new ClienteEN
                    { Id = _reader.GetInt32(0), 
                        IdRol= _reader.GetInt32(1),
                       IdTarjetaRFID = _reader.GetInt32(2),
                        IdTipoDocumento = _reader.GetInt32(3),
                        Documento =  _reader.GetString(4), 
                        Nombre = _reader.GetString(5), 
                        Apellido = _reader.GetString(6), 
                        Celular = _reader.GetString(7), 
                        
                    }); } 
                _conn.Close(); } return _Lista; }
        public static int GuardarCliente(ClienteEN pclienteEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("GuardarCliente", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@IdRol", pclienteEN.IdRol));
                _comando.Parameters.Add(new SqlParameter("@IdTarjetaRFID", pclienteEN.IdTarjetaRFID));
                _comando.Parameters.Add(new SqlParameter("@IdTipoDocumento", pclienteEN.IdTipoDocumento));

                _comando.Parameters.Add(new SqlParameter("@Documento", pclienteEN.Documento));
                _comando.Parameters.Add(new SqlParameter("@Nombre", pclienteEN.Nombre));
                _comando.Parameters.Add(new SqlParameter("@Apellido", pclienteEN.Apellido));
                _comando.Parameters.Add(new SqlParameter("@Celular", pclienteEN.Celular));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }

        }
        public static int EliminarCliente(ClienteEN pclienteEN)
        {
            using (IDbConnection _conn =
                ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando =
                new SqlCommand("EliminarCliente", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pclienteEN.Id));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
        public static int ModificarCliente(ClienteEN pclienteEN)
        {
            using (IDbConnection _conn =
                ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando =
                new SqlCommand("ModificarCliente", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pclienteEN.Id));
                _comando.Parameters.Add(new SqlParameter("@IdRol", pclienteEN.IdRol));
                _comando.Parameters.Add(new SqlParameter("@IdTarjetaRFID,", pclienteEN.IdTarjetaRFID));
                _comando.Parameters.Add(new SqlParameter("@IdTipoDocumento", pclienteEN.IdTipoDocumento));
                _comando.Parameters.Add(new SqlParameter("@Documento", pclienteEN.Documento));
                _comando.Parameters.Add(new SqlParameter("@Nombre", pclienteEN.Nombre));
                _comando.Parameters.Add(new SqlParameter("@Apellido", pclienteEN.Apellido));
                _comando.Parameters.Add(new SqlParameter("@Celular", pclienteEN.Celular));
          
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }

        }
    }
}
