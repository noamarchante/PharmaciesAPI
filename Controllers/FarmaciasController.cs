using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tarea7.Data;
using Tarea7.Models;

namespace Tarea7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FarmaciasController : Controller
    {
        private readonly Tarea7Context _context;

        public FarmaciasController(Tarea7Context context)
        {
            _context = context;
        }

        // GET: api/Farmacias
        [HttpGet]
        public ActionResult<IEnumerable<Farmacia>> GetFarmacias()
        {
            return _context.Farmacias.Where(farmacia => farmacia.UserId.Equals(GetUserId())).ToList();
        }

        // GET: api/Farmacias/5
        [HttpGet("{id}")]
        public ActionResult<Farmacia> GetFarmacia(int id)
        {
            var farmacia = _context.Farmacias.Find(id);
            if (farmacia == null)
            {
                return NotFound();
            }

            if (!farmacia.UserId.Equals(GetUserId())) { 
                return Unauthorized(); 
            }

            return farmacia;
        }

        // POST: api/Farmacias
        [HttpPost]
        public ActionResult<Farmacia> Create([FromBody][Bind("ID,Nombre,Direccion")] Farmacia farmacia)
        {
            if (!farmacia.UserId.Equals(GetUserId()))
            {
                return Unauthorized();
            }
            _context.Farmacias.Add(farmacia);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetFarmacia), new { id = farmacia.ID }, farmacia);
        }

        // PUT: api/Farmacias/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [Bind("ID,Nombre,Direccion")] Farmacia farmacia)
        {
            if (id != farmacia.ID)
            {
                return BadRequest();
            }

            if (!farmacia.UserId.Equals(GetUserId()))
            {
                return Unauthorized();
            }

            _context.Entry(farmacia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
          
            return NoContent();
        }

        // DELETE: api/Farmacias/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var farmacia = _context.Farmacias.FirstOrDefault(f => f.ID == id);

            if (farmacia == null)
            {
                return NotFound();
            }

            if (!farmacia.UserId.Equals(GetUserId()))
            {
                return Unauthorized();
            }

            _context.Farmacias.Remove(farmacia);
            _context.SaveChanges();
            return NoContent();
        }

        private string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
