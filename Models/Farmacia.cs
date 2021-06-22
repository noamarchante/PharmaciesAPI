using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tarea7.Models
{
    public class Farmacia
    {
        public int ID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se debe introducir el nombre de la farmacia")]
        public string Nombre { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Se debe introducir la dirección de la farmacia")]
        public string Direccion { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
        public string UserId { get; set; }
    }
}
