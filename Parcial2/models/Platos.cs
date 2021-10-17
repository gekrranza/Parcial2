using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Parcial2.models
{
	public class Platos
	{
        [Key]
        public int PlatosID { get; set; }
        public int EmpresaID { get; set; }
        public int GrupoID { get; set; }
        public string NombrePlato { get; set; }
        public string DescripcionPlato { get; set; }
        public decimal Precio { get; set; }
        public string TiempoPreparacion { get; set; }
        public string Imagen { get; set; }
        public string AplicaPropina { get; set; }
        public string Lunes { get; set; }
        public string Martes { get; set; }
        public string Miercoles { get; set; }
        public string Jueves { get; set; }
        public string Viernes { get; set; }
        public string Sabado { get; set; }
        public string Domingo { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
    }
}
