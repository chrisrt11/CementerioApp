using System.ComponentModel.DataAnnotations;

namespace CementerioApp.Models;

public enum EstadoLote
{
    Disponible,
    Ocupado
}

public class Lote
{
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Sector { get; set; } = string.Empty;

    public int Fila { get; set; }

    public int Columna { get; set; }

    public double Latitud { get; set; }

    public double Longitud { get; set; }

    public EstadoLote Estado { get; set; } = EstadoLote.Disponible;

    [MaxLength(500)]
    public string? Descripcion { get; set; }

    public Difunto? Difunto { get; set; }

    public string Identificador => $"{Sector}-F{Fila}-C{Columna}";
}
