using BreakingGymDAL;
using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakinGymBL
{
    public class MembresiaBL
    {
        public static MembresiaEN ObtenerMembresiaPorId(int id)
        {
            // Llama al método estático de la DAL directamente
            return MembresiaDAL.ObtenerMembresiaPorId(id);
        }
        public List<MembresiaEN> MostrarMembresia()
        {
            return MembresiaDAL.MostrarMembresia();
        }
        public static List<MembresiaEN> BuscarMembresia(string nombre)
        {
            return MembresiaDAL.BuscarMembresia(nombre);
        }
        public int GuardarMembresia(MembresiaEN pmembresiaEN)
        {
            return MembresiaDAL.AgregarMembresia(pmembresiaEN);
        }
        public int EliminarMembresia(MembresiaEN pmembresiaEN)
        {
            return MembresiaDAL.EliminarMembresia(pmembresiaEN);
        }
        public int ModificarMembresia(MembresiaEN pmembresiaEN)
        {
            return MembresiaDAL.ModificarMembresia(pmembresiaEN);
        }
    }
}
