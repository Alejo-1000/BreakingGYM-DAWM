using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingGymEN
{
    public static class UsuarioActual
    {
        public static string Cuenta { get; set; }            // Solo la cuenta
        public static UsuarioEN UsuarioLogueado { get; set; } // También todo el usuario si lo necesitas
    }
}
