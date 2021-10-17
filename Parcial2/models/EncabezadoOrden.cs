using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Parcial2.models
{
	public class EncabezadoOrden
	{
        [Key]
        public int EncabezadoOrdenID { get; set; }
        public int EmpresaID { get; set; }
        public int UsuarioID { get; set; }
        public string TipoOrden { get; set; }
        public string FechaOrden { get; set; }
        public int MesaID { get; set; }
        public string Cliente { get; set; }
        public string EstadoOrden { get; set; }
        public string TipoPago { get; set; }
        public string Estado { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
    }
}
