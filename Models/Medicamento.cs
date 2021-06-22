using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tarea7.Models
{
    public class Medicamento
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Debe introducir el nombre del medicamento")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        public String Descripcion { get; set; }
        [Display(Name = "Laboratorio")]
        public String Laboratorio { get; set; }
        [Display(Name = "Proveedor")]
        public String Proveedor { get; set; }
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}",
        ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de caducidad")]
        public DateTime? FechaCaducidad { get; set; }
        [Display(Name = "Tipo de medicamento")]
        [EnumDataType(typeof(TiposMedicamento))]
        public TiposMedicamento TipoMedicamento { get; set; }
        [Display(Name = "Farmacia")]
        public Farmacia Farmacia { get; set; }
        public int FarmaciaId { get; set; }
    }
}