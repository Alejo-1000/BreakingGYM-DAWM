using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingGymEN
{
    public class ClienteEN : Persona
    {
        public int IdTarjetaRFID { get; set; }

        public ClienteEN() : base() { }

        public ClienteEN(int Id, int IdRol, int IdTarjetaRFID, int IdTipoDocumento, string Documento, string Nombre, string Apellido, string Celular)
            : base(Id, IdRol, IdTipoDocumento, Documento, Nombre, Apellido, Celular)
        {
            this.IdTarjetaRFID = IdTarjetaRFID;
        }

    }     
}
