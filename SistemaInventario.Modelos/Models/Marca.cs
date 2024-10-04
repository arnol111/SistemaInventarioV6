using System.ComponentModel.DataAnnotations;

namespace SistemaInventario.Modelos.Models;

public class Marca
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [MaxLength(60, ErrorMessage = "La longitud maxima del campo debe ser {1} caracteres")]
    public string Nombre{ get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [MaxLength(200, ErrorMessage = "La longitud maxima del campo debe ser {1} caracteres")]
    public string Descripcion { get; set; }

    [Required()]
    public bool Estado { get; set; }
}