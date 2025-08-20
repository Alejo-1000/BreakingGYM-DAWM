using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingGymEN
{
    public class InscripcionEN
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdMembresia { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
