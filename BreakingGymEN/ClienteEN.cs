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
        public string Documento { get; set; }   
        public int IdTipoDocumento { get; set; }
        public int IdTarjeta { get; set; }

        public ClienteEN() : base() {
            this.Documento = Documento;
            this.IdTipoDocumento= IdTipoDocumento;
        }

        public ClienteEN(int Id, int IdRol, int IdTarjeta, int IdTipoDocumento, string Documento, string Nombre, string Apellido, string Celular)
            : base(Id, IdRol, Nombre, Apellido, Celular)
        {
            this.Documento= Documento;
            this.IdTipoDocumento = IdTipoDocumento;
            this.IdTarjeta = IdTarjeta;
        }

    }     
}
