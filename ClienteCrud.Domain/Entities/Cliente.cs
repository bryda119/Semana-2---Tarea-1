using System.ComponentModel.DataAnnotations;

namespace ClienteCrud.Domain.Entities;

/// <summary>
/// Entidad Cliente (POO) con validaciones de datos.
/// </summary>
public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 80 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 80 caracteres.")]
    [Display(Name = "Apellido")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [StringLength(120, ErrorMessage = "El correo no puede superar 120 caracteres.")]
    [Display(Name = "Correo electrónico")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "Ingrese un teléfono válido.")]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 20 caracteres.")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 200 caracteres.")]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = string.Empty;

    [Display(Name = "Fecha de registro")]
    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string NombreCompleto => $"{Nombre} {Apellido}";
}
