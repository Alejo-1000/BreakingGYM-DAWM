using BreakingGymDAL;
using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingGymBL
{
   public class TarjetaBL
    {
        public List<TarjetaEN> MostrarTarjetaRFID()
        {
            return TarjetaDAL.MostrarTarjetas();
        }

        public void GuardarTarjetaRFID(TarjetaEN tarjeta)
        {
            TarjetaDAL.AgregarTarjeta(tarjeta);
        }

        public void EliminarTarjetaRFID(TarjetaEN tarjeta)
        {
            TarjetaDAL.EliminarTarjeta(tarjeta);
        }

        public void ModificarTarjetaRFID(TarjetaEN tarjeta)
        {
            TarjetaDAL.ModificarTarjeta(tarjeta);
        }
    }
}
