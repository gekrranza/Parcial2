using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parcial2.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Parcial2.Contexto;


namespace Parcial2.Controllers
{
    [ApiController]
    public class encabezadoOrdenController : ControllerBase
    {
        private readonly AppicationDbContext _contexto;

        public encabezadoOrdenController(AppicationDbContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo para retornar todos los reg. de la tabla de EncabezadoOrden
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/encabezadoOrden")]
        public IActionResult Get()
        {
            IEnumerable<EncabezadoOrden> equiposList = from e in _contexto.EncabezadoOrden
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
        [Route("api/encabezadoOrden/{id}")]
        public IActionResult getbyId(int id)
        {
            EncabezadoOrden unEquipo = (from e in _contexto.EncabezadoOrden
                                 where e.EncabezadoOrdenID == id //Filtro por ID
                                 select e).FirstOrDefault();
            if (unEquipo != null)
            {
                return Ok(unEquipo);
            }

            return NotFound();
        }


        /// <summary>
        /// Este metodo retorna los reg. en la tabl de EncabezadoOrden que contengan el valor dado en el parametro.
        /// </summary>
        /// <param name="buscarNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/encabezadoOrden/buscarnombre/{buscarNombre}")]
        public IActionResult obtenerNombre(string buscarNombre)
        {
            IEnumerable<EncabezadoOrden> equipoPorNombre = from e in _contexto.EncabezadoOrden
                                                    where e.FechaOrden.Contains(buscarNombre)
                                                    select e;
            if (equipoPorNombre.Count() > 0)
            {
                return Ok(equipoPorNombre);
            }

            return NotFound();
        }

        /// Este metodo inserta un reg. en la tabla de EncabezadoOrden


        [HttpPost]
        [Route("api/encabezadoOrden")]
        public IActionResult guardarEncabezado([FromBody] EncabezadoOrden equipoNuevo)
        {
            try
            {
                IEnumerable<EncabezadoOrden> equipoExiste = from e in _contexto.EncabezadoOrden
                                                     where e.FechaOrden == equipoNuevo.FechaOrden

                                                     select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.EncabezadoOrden.Add(equipoNuevo);
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

        /// Este metodo modifica un reg. en la tabla de EncabezadoOrden

        [HttpPut]
        [Route("api/encabezadoOrden")]
        public IActionResult updateEncabezado([FromBody] EncabezadoOrden equipoAModificar)
        {
            EncabezadoOrden equipoExiste = (from e in _contexto.EncabezadoOrden
                                     where e.EncabezadoOrdenID == equipoAModificar.EncabezadoOrdenID
                                     select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.MesaID = equipoAModificar.MesaID;
            equipoExiste.Cliente = equipoAModificar.Cliente;
            equipoExiste.TipoPago = equipoAModificar.TipoPago;
            ;

            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);

        }

    }
}