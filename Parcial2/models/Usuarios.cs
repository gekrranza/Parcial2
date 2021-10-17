using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Parcial2.models
{
	public class Usuarios
	{
        [Key]
        public int EmpresaID { get; set; }
        public int UsuarioID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefonos { get; set; }
        public string Dirrecion { get; set; }
        public int PuestoID { get; set; }
        public string DUI { get; set; }
        public string NIT { get; set; }
        public string FechaContratacion { get; set; }
        public string FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string FechaBaja { get; set; }
        public string FechaModificacion { get; set; }
        public string foto { get; set; }
        public string token { get; set; }
        public string clave { get; set; }


    }
}
