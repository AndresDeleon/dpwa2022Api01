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
        [Route("api/equipos/{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            IEnumerable<equipos> equiposList = from e in _contexto.equipos
                                               where e.id_equipos == idUsuario
                                               select e;

            IEnumerable<equipos> equiposListado = _contexto.equipos;

            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }
    }
}
