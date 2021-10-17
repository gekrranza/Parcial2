using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parcial2.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Parcial2.Contexto;


namespace Parcial2.Controllers
{
    [ApiController]
    public class platosController : ControllerBase
{
        private readonly AppicationDbContext _contexto;

        public platosController(AppicationDbContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo para retornar todos los reg. de la tabla de EQUIPOS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/platos")]
        public IActionResult Get()
        {
            IEnumerable<Platos> equiposList = from e in _contexto.Platos
                                               select e;
            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para retornar UN reg. de la tabla EQUIPOS filtrado por ID
        /// </summary>
        /// <param name="id">Valor Entero del campo id_equipos</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/platos/{id}")]
        public IActionResult getbyId(int id)
        {
            Platos unEquipo = (from e in _contexto.Platos
                                where e.PlatosID == id //Filtro por ID
                                select e).FirstOrDefault();
            if (unEquipo != null)
            {
                return Ok(unEquipo);
            }

            return NotFound();
        }


        /// <summary>
        /// Este metodo retorna los reg. en la tabl de EQUIPOS que contengan el valor dado en el parametro.
        /// </summary>
        /// <param name="buscarNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/platos/buscarnombre/{buscarNombre}")]
        public IActionResult obtenerNombre(string buscarNombre)
        {
            IEnumerable<Platos> equipoPorNombre = from e in _contexto.Platos
                                                   where e.NombrePlato.Contains(buscarNombre)
                                                   select e;
            if (equipoPorNombre.Count() > 0)
            {
                return Ok(equipoPorNombre);
            }

            return NotFound();
        }

        /// Este metodo inserta un reg. en la tabla de EQUIPOS.


        [HttpPost]
        [Route("api/platos")]
        public IActionResult guardarEquipo([FromBody] Platos equipoNuevo)
        {
            try
            {
                IEnumerable<Platos> equipoExiste = from e in _contexto.Platos
                                                    where e.NombrePlato == equipoNuevo.NombrePlato

                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.Platos.Add(equipoNuevo);
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

        /// Este metodo modifica un reg. en la tabla de EQUIPOS.

        [HttpPut]
        [Route("api/platos")]
        public IActionResult updateEquipo([FromBody] Platos equipoAModificar)
        {
            Platos equipoExiste = (from e in _contexto.Platos
                                    where e.PlatosID == equipoAModificar.PlatosID
                                    select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.NombrePlato = equipoAModificar.NombrePlato;
            equipoExiste.DescripcionPlato = equipoAModificar.DescripcionPlato;
            equipoExiste.Precio = equipoAModificar.Precio;

            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);

        }

    }
}
