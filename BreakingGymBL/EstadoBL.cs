using BreakingGymDAL;
using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakinGymBL
{
    public class EstadoBL
    {
        EstadoDAL estadoDAL = new EstadoDAL();
        public List<EstadoEN> MostrarEstado()
        {
            return estadoDAL.MostrarEstado();
        }
        public int GuardarEstado(EstadoEN pestadoEN)
        {
            return estadoDAL.AgregarEstado(pestadoEN);
        }
        public int EliminarEstado(EstadoEN pestadoEN)
        {
            return estadoDAL.EliminarEstado(pestadoEN);
        }
        public int ModificarEstado(EstadoEN pestadoEN)
        {
            return estadoDAL.ModificarEstado(pestadoEN);
        }
    }
}
