using BreakingGymDAL;
using BreakingGymEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakinGymBL
{
    public class ClienteBL
    {
        public List<ClienteEN> MostrarCliente()
        {
            return ClienteDAL.MostrarCliente();
        }
        public int GuardarCliente(ClienteEN pclienteEN)
        {
            return ClienteDAL.GuardarCliente(pclienteEN);
        }
        public int EliminarCliente(ClienteEN pclienteEN)
        {
            return ClienteDAL.EliminarCliente(pclienteEN);
        }
        public int ModificarCliente(ClienteEN pclienteEN)
        {
            return ClienteDAL.ModificarCliente(pclienteEN);
        }
    }
}
