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
        public List<RegistroAsistenciaEN> MostrarAsistencia()
        {
            return RegistroAsistenciaDAL.MostrarAsistencia();
        }
        public List<RegistroAsistenciaEN> BuscarAsistencia(DateTime fechaAsistencia)
        {
            return RegistroAsistenciaDAL.BuscarAsistencia(fechaAsistencia);
        }
        public int RegistrarAsistenciaPorTarjeta(string tarjetaRFID)
        {
            return RegistroAsistenciaDAL.RegistrarAsistenciaPorTarjeta(tarjetaRFID);
        }
    }
}
