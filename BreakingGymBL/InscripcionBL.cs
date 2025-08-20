using BreakingGymDAL;
using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakinGymBL
{
    public class InscripcionBL
    {
        public List<InscripcionEN> MostrarInscripcion()
        {
            return InscripcionDAL.MostrarInscripcion();
        }
        public static List<InscripcionEN> BuscarInscripcion(string idCliente)
        {
            return InscripcionDAL.BuscarInscripcion(idCliente);
        }
        public int GuardarInscripcion(InscripcionEN pinscripcionEN)
        {
            return InscripcionDAL.AgregarInscripcion(pinscripcionEN);
        }
        public int EliminarInscripcion(InscripcionEN pinscripcionEN)
        {
            return InscripcionDAL.EliminarInscripcion(pinscripcionEN);
        }
        public int ModificarInscripcion(InscripcionEN pinscripcionEN)
        {
            return InscripcionDAL.ModificarInscripcion(pinscripcionEN);
        }
    }
}
