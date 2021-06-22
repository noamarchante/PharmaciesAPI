using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tarea7.Models;

namespace Tarea7.Data
{
    public class Tarea7Context: IdentityDbContext
    {
        public Tarea7Context(DbContextOptions<Tarea7Context> options) : base(options) { }
        public DbSet<Farmacia> Farmacias { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
    }
}
