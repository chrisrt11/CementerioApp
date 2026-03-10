using System.ComponentModel.DataAnnotations;

namespace CementerioApp.Models;

public class Difunto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Cedula { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public DateTime FechaFallecimiento { get; set; }

    [MaxLength(500)]
    public string? Observaciones { get; set; }

    public int? LoteId { get; set; }
    public Lote? Lote { get; set; }

    public string NombreCompleto => $"{Nombre} {Apellido}";
}
