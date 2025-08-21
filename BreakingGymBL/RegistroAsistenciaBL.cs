using BreakingGymDAL;
using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakinGymBL
{
    public class RegistroAsistenciaBL
    {
        private RegistroAsistenciaDAL dal = new RegistroAsistenciaDAL();

        public List<RegistroAsistenciaEN> MostrarAsistencia(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            return dal.MostrarAsistencia(fechaDesde, fechaHasta);
        }

        public void RegistrarAsistenciaPorRFID(string numeroRFID)
        {
            dal.RegistrarAsistenciaPorRFID(numeroRFID);
        }

    }
}
