using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Parcial2.models
{
	public class DetalleOrdenes
	{
        [Key]
        public int DetalleoOrdenID { get; set; }
        public int EncabezadoOrdenID { get; set; }
        public int EmpresaID { get; set; }
        public int PlatoID { get; set; }
        public int Cantidad { get; set; }
        public string Comentarios { get; set; }
        public decimal DescuentoEspecial { get; set; }
        public int RecargoOrden { get; set; }
        public string Estado { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
    }
}
