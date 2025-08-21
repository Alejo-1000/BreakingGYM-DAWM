using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingGymEN
{
    public class RegistroAsistenciaEN
    {
        public int Id { get; set; }
        public int IdAsistencia { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public string NumeroRFID { get; set; }
        public DateTime FechaAsistencia { get; set; }
        public TimeSpan HoraAsistencia { get; set; }

        public string NombreCompleto => $"{NombreCliente} {ApellidoCliente}";

    }
}
