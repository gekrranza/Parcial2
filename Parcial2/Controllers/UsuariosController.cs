using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parcial2.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Parcial2.Contexto;


namespace Parcial2.Controllers
{
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly AppicationDbContext _contexto;

        public usuariosController(AppicationDbContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo para retornar todos los reg. de la tabla de USUARIOS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/usuarios")]
        public IActionResult Get()
        {
            IEnumerable<Usuarios> equiposList = from e in _contexto.Usuarios
                                              select e;
            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para retornar UN reg. de la tabla USUARIOS filtrado por ID
        /// </summary>
        /// <param name="id">Valor Entero del campo UsuarioID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/usuarios/{id}")]
        public IActionResult getbyId(int id)
        {
            Usuarios unEquipo = (from e in _contexto.Usuarios
                               where e.UsuarioID == id //Filtro por ID
                               select e).FirstOrDefault();
            if (unEquipo != null)
            {
                return Ok(unEquipo);
            }

            return NotFound();
        }


        /// <summary>
        /// Este metodo retorna los reg. en la tabl de USUARIOS que contengan el valor dado en el parametro.
        /// </summary>
        /// <param name="buscarNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/usuarios/buscarnombre/{buscarNombre}")]
        public IActionResult obtenerNombre(string buscarNombre)
        {
            IEnumerable<Usuarios> equipoPorNombre = from e in _contexto.Usuarios
                                                  where e.Nombres.Contains(buscarNombre)
                                                  select e;
            if (equipoPorNombre.Count() > 0)
            {
                return Ok(equipoPorNombre);
            }

            return NotFound();
        }

        /// Este metodo inserta un reg. en la tabla de USUARIOS


        [HttpPost]
        [Route("api/usuarios")]
        public IActionResult guardarUsuario([FromBody] Usuarios equipoNuevo)
        {
            try
            {
                IEnumerable<Usuarios> equipoExiste = from e in _contexto.Usuarios
                                                   where e.Nombres == equipoNuevo.Nombres

                                                   select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.Usuarios.Add(equipoNuevo);
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
        [Route("api/usuarios")]
        public IActionResult updateUsuario([FromBody] Usuarios equipoAModificar)
        {
            Usuarios equipoExiste = (from e in _contexto.Usuarios
                                   where e.UsuarioID == equipoAModificar.UsuarioID
                                   select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.Nombres = equipoAModificar.Nombres;
            equipoExiste.Apellidos = equipoAModificar.Apellidos;
            equipoExiste.Dirrecion = equipoAModificar.Dirrecion;
                ;

            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);

        }

    }
}