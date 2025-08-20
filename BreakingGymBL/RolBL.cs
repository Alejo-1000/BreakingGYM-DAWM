using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreakingGymDAL;
using BreakingGymEN;

namespace BreakinGymBL
{
    public class RolBL
    {
        public List<RolEN> MostrarRol()
        {
            return RolDAL.MostrarRol();
        }
        public int GuardarRol(RolEN prolEN)
        {
            return RolDAL.AgregarRol(prolEN);
        }
        public int EliminarRol(RolEN prolEN)
        {
            return RolDAL.EliminarRol(prolEN);
        }
        public int ModificarRol(RolEN prolEN)
        {
            return RolDAL.ModificarRol(prolEN);
        }
    }
}
