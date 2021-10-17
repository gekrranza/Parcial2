using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Parcial2.models;

namespace Parcial2.Contexto
{
	public class AppicationDbContext: DbContext
	{
		public AppicationDbContext(DbContextOptions<AppicationDbContext> option) : base(option)
		{

		}
		public DbSet<Usuarios> Usuarios { get; set; }
		public DbSet<Platos> Platos { get; set; }
		public DbSet<EncabezadoOrden> EncabezadoOrden { get; set; }
		public DbSet<DetalleOrdenes> DetalleOrdenes { get; set; }

	}
}
