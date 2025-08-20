using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreakingGymEN;
using BreakingGymDAL;

namespace BreakinGymBL
{
    public class ServicioBL
    {
        public List<ServicioEN> MostrarServicio()
        {
            return ServicioDAL.MostrarServicio();
        }
        public int GuardarServicio(ServicioEN pservicioEN)
        {
            return ServicioDAL.AgregarServicio(pservicioEN);
        }
        public int EliminarServicio(ServicioEN pservicioEN)
        {
            return ServicioDAL.EliminarServicio(pservicioEN);
        }
        public int ModificarServicio(ServicioEN pservicioEN)
        {
            return ServicioDAL.ModificarServicio(pservicioEN);
        }
    }
}
