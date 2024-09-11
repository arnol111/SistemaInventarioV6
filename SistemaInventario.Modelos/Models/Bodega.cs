using System.ComponentModel.DataAnnotations;

namespace SistemaInventario.Modelos.Models;

public class Bodega
{
   [Key]
   public int Id { get; set; }

   [Required(ErrorMessage = "El campo Nombre es requerido")]
   [MaxLength(60, ErrorMessage = "La longitud maxima de 20 caracteres")]
   public string Nombre { get; set; }

   [Required(ErrorMessage = "El campo Descripcion es requerido")]
   [MaxLength(200, ErrorMessage = "La longitud maxima de 200 caracteres")]
   public string Descripcion { get; set; }
   
   [Required(ErrorMessage = "El campo Estado es requerido")]
   public bool Estado { get; set; }
}