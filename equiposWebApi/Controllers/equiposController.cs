using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using equiposWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace equiposWebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly prestamosContext _contexto;

        public equiposController(prestamosContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get()
        {
            IEnumerable<equipos> equiposList = from e in _contexto.equipos
                                               select e;

            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/equipos/{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            IEnumerable<equipos> equipos = from e in _contexto.equipos
                                               where e.id_equipos == idUsuario
                                               select e;


            if (equipos.Count() > 0)
            {
                return Ok(equipos);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/equipos")]
        public IActionResult guardarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                _contexto.equipos.Add(equipoNuevo);
                _contexto.SaveChanges();
                return Ok(equipoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/equipos")]
        public IActionResult updateEquipo([FromBody] equipos equipoModificar)
        {
            try
            {
                equipos equipoExiste = (from e in _contexto.equipos
                                        where e.id_equipos == equipoModificar.id_equipos
                                        select e).FirstOrDefault();

                if (equipoExiste is null)
                {
                    return NotFound();
                }

                equipoExiste.nombre = equipoModificar.nombre;
                equipoExiste.descripcion = equipoModificar.descripcion;

                _contexto.Entry(equipoExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(equipoExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
