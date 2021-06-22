using System.ComponentModel.DataAnnotations;

namespace Tarea7.Models
{
   public enum TiposMedicamento
    {
        [Display(Name = "Analgésico")]
        Analgesico,
        [Display(Name = "Antiácido")]
        Antiacido,
        [Display(Name = "Antiulceroso")]
        Antiulceroso,
        [Display(Name = "Antialérgico")]
        Antialergico,
        [Display(Name = "Antidiarreico")]
        Antidiarreico,
        [Display(Name = "Laxante")]
        Laxante,
        [Display(Name = "Antiinfeccioso")]
        Antiinfeccioso,
        [Display(Name = "Antiinflamatorio")]
        Antiinflamatorio,
        [Display(Name = "Antipirético")]
        Antipiretico,
        [Display(Name = "Antitusivo")]
        Antitusivo,
        [Display(Name = "Mucolítico")]
        Mucolitico

    }
}
