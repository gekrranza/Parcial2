using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parcial2.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Parcial2.Contexto;


namespace Parcial2.Controllers
{
    [ApiController]
    public class detalleOrdenesController : ControllerBase
    {
        private readonly AppicationDbContext _contexto;

        public detalleOrdenesController(AppicationDbContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo para retornar todos los reg. de la tabla de EncabezadoOrden
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/detalleOrdenes")]
        public IActionResult Get()
        {
            IEnumerable<DetalleOrdenes> equiposList = from e in _contexto.DetalleOrdenes
                                                       select e;
            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para retornar UN reg. de la tabla EncabezadoOrden filtrado por ID
        /// </summary>
        /// <param name="id">Valor Entero del campo EncabezadoOrdenID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/detalleOrdenes/{id}")]
        public IActionResult getbyId(int id)
        {
            DetalleOrdenes unEquipo = (from e in _contexto.DetalleOrdenes
                                        where e.DetalleoOrdenID == id //Filtro por ID
                                        select e).FirstOrDefault();
            if (unEquipo != null)
            {
                return Ok(unEquipo);
            }

            return NotFound();
        }


        /// <summary>
        /// Este metodo retorna los reg. en la tabl de DetalleOrdenes que contengan el valor dado en el parametro.
        /// </summary>
        /// <param name="buscarNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/detalleOrdenes/buscarnombre/{buscarNombre}")]
        public IActionResult obtenerNombre(string buscarNombre)
        {
            IEnumerable<DetalleOrdenes> equipoPorNombre = from e in _contexto.DetalleOrdenes
                                                           where e.Comentarios.Contains(buscarNombre)
                                                           select e;
            if (equipoPorNombre.Count() > 0)
            {
                return Ok(equipoPorNombre);
            }

            return NotFound();
        }

        /// Este metodo inserta un reg. en la tabla DetalleOrdenes


        [HttpPost]
        [Route("api/detalleOrdenes")]
        public IActionResult guardarDetalle([FromBody] DetalleOrdenes equipoNuevo)
        {
            try
            {
                IEnumerable<DetalleOrdenes> equipoExiste = from e in _contexto.DetalleOrdenes
                                                            where e.Comentarios == equipoNuevo.Comentarios

                                                            select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.DetalleOrdenes.Add(equipoNuevo);
                    _contexto.SaveChanges();
                    return Ok(equipoNuevo);
                }
                return Ok(equipoExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// Este metodo modifica un reg. en la tabla de DetalleOrdenes

        [HttpPut]
        [Route("api/detalleOrdenes")]
        public IActionResult updateDetalle([FromBody] DetalleOrdenes equipoAModificar)
        {
            DetalleOrdenes equipoExiste = (from e in _contexto.DetalleOrdenes
                                            where e.DetalleoOrdenID == equipoAModificar.DetalleoOrdenID
                                            select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.PlatoID = equipoAModificar.PlatoID;
            equipoExiste.Cantidad = equipoAModificar.Cantidad;
            equipoExiste.Comentarios = equipoAModificar.Comentarios;
            ;

            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);

        }

    }
}
