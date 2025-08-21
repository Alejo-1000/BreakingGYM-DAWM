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
        public List<RegistroAsistenciaEN> MostrarAsistencia(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            List<RegistroAsistenciaEN> lista = new List<RegistroAsistenciaEN>();

            using (IDbConnection con = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "MostrarAsistencia";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var paramDesde = cmd.CreateParameter();
                    paramDesde.ParameterName = "@FechaDesde";
                    paramDesde.Value = (object)fechaDesde ?? DBNull.Value;
                    cmd.Parameters.Add(paramDesde);

                    var paramHasta = cmd.CreateParameter();
                    paramHasta.ParameterName = "@FechaHasta";
                    paramHasta.Value = (object)fechaHasta ?? DBNull.Value;
                    cmd.Parameters.Add(paramHasta);

                    con.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new RegistroAsistenciaEN
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                ApellidoCliente = dr["ApellidoCliente"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NumeroRFID = dr["NumeroRFID"].ToString(),
                                FechaAsistencia = Convert.ToDateTime(dr["FechaAsistencia"]),
                                HoraAsistencia = (TimeSpan)dr["HoraAsistencia"]
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public void RegistrarAsistenciaPorRFID(string numeroRFID)
        {
            using (IDbConnection con = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "RegistrarAsistenciaPorRFID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@NumeroRFID";
                    param.Value = numeroRFID;
                    cmd.Parameters.Add(param);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
