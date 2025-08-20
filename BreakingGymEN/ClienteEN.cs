using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingGymEN
{ 
        public class ClienteEN : Persona
        {
        public String TarjetaRFID { get; set; }
        public ClienteEN() : base()
        {
        }
        public ClienteEN(int Id, int IdRol, int IdTipoDocumento, string Documento, string Nombre, string Apelllido, string Celular,string TarjetaRFID) : base(Id, IdRol, IdTipoDocumento, Documento, Nombre, Apelllido, Celular)
            {
                this.TarjetaRFID = TarjetaRFID;
        }
            
        }
}
