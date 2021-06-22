using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class MedicamentosController : Controller
    {
        private readonly Tarea7Context _context;
        public MedicamentosController(Tarea7Context context)
        {
            _context = context;
        }

        // GET: api/Medicamentos
        [HttpGet]
        public ActionResult<IEnumerable<Medicamento>> GetMedicamentos()
        {
            return _context.Medicamentos.Where(medicamento =>GetFarmacias().Contains(medicamento.FarmaciaId)).ToList();
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public ActionResult<Medicamento> GetMedicamento(int id)
        {
            var medicamento = _context.Medicamentos.Find(id);
            if (medicamento == null)
            {
                return NotFound();
            }
            if (!GetFarmacias().Contains(medicamento.FarmaciaId))
            {
                return Unauthorized();
            }

            return medicamento;
        }

        // POST: api/Medicamentos
        [HttpPost]
        public ActionResult<Medicamento> Create([FromBody][Bind("ID,Nombre,Descripcion,Laboratorio,Proveedor,Cantidad,FechaCaducidad,TipoMedicamento,FarmaciaId")] Medicamento medicamento)
        {
            if (!GetFarmacias().Contains(medicamento.FarmaciaId))
            {
                return Unauthorized();
            }
            _context.Medicamentos.Add(medicamento);
            _context.SaveChanges();
            ViewData["FarmaciaId"] = new SelectList(_context.Farmacias, "ID", "Direccion", medicamento.FarmaciaId);
            return CreatedAtAction(nameof(GetMedicamento), new { id = medicamento.ID }, medicamento);
        }

        // PUT: Medicamentos/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [Bind("ID,Nombre,Descripcion,Laboratorio,Proveedor,Cantidad,FechaCaducidad,TipoMedicamento,FarmaciaId")] Medicamento medicamento)
        {
            if (id != medicamento.ID)
            {
                return BadRequest();
            }

            if (!GetFarmacias().Contains(medicamento.FarmaciaId))
            {
                return Unauthorized();
            }

            _context.Entry(medicamento).State = EntityState.Modified;
            _context.SaveChanges();
            
            ViewData["FarmaciaId"] = new SelectList(_context.Farmacias, "ID", "Direccion", medicamento.FarmaciaId);
            return NoContent();
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var medicamento = _context.Medicamentos
                .Include(m => m.Farmacia)
                .FirstOrDefault(m => m.ID == id);
            
            if (medicamento == null)
            {
                return NotFound();
            }

            if (!GetFarmacias().Contains(medicamento.FarmaciaId))
            {
                return Unauthorized();
            }

            _context.Medicamentos.Remove(medicamento);
            _context.SaveChanges();
            return NoContent();
        }

        private int[] GetFarmacias()
        {
            var farmacias = _context.Farmacias.Where(farmacia => farmacia.UserId.Equals(GetUserId())).ToList();
            int[] farmaciasIds = new int[farmacias.Count()];
            for (int i = 0; i < farmacias.Count(); i++)
            {
                farmaciasIds[i] = farmacias[i].ID;
            }
            return farmaciasIds;
        }

        private string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        }
    }
}
