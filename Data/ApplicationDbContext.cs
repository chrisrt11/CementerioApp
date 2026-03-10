using Microsoft.EntityFrameworkCore;
using CementerioApp.Models;

namespace CementerioApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lote> Lotes { get; set; }
    public DbSet<Difunto> Difuntos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lote>().HasOne(l => l.Difunto)
            .WithOne(d => d.Lote)
            .HasForeignKey<Difunto>(d => d.LoteId);

        // Seed lotes - 3 sectores (A, B, C) con 3 lotes cada uno
        modelBuilder.Entity<Lote>().HasData(
            // Sector A
            new Lote { Id = 1, Sector = "A", Fila = 1, Columna = 1, Latitud = -34.6037, Longitud = -58.3816, Estado = EstadoLote.Ocupado, Descripcion = "Lote sector A, fila 1, columna 1" },
            new Lote { Id = 2, Sector = "A", Fila = 1, Columna = 2, Latitud = -34.6038, Longitud = -58.3817, Estado = EstadoLote.Ocupado, Descripcion = "Lote sector A, fila 1, columna 2" },
            new Lote { Id = 3, Sector = "A", Fila = 1, Columna = 3, Latitud = -34.6039, Longitud = -58.3818, Estado = EstadoLote.Disponible, Descripcion = "Lote sector A, fila 1, columna 3" },
            // Sector B
            new Lote { Id = 4, Sector = "B", Fila = 1, Columna = 1, Latitud = -34.6040, Longitud = -58.3819, Estado = EstadoLote.Disponible, Descripcion = "Lote sector B, fila 1, columna 1" },
            new Lote { Id = 5, Sector = "B", Fila = 1, Columna = 2, Latitud = -34.6041, Longitud = -58.3820, Estado = EstadoLote.Ocupado, Descripcion = "Lote sector B, fila 1, columna 2" },
            new Lote { Id = 6, Sector = "B", Fila = 1, Columna = 3, Latitud = -34.6042, Longitud = -58.3821, Estado = EstadoLote.Disponible, Descripcion = "Lote sector B, fila 1, columna 3" },
            // Sector C
            new Lote { Id = 7, Sector = "C", Fila = 1, Columna = 1, Latitud = -34.6043, Longitud = -58.3822, Estado = EstadoLote.Disponible, Descripcion = "Lote sector C, fila 1, columna 1" },
            new Lote { Id = 8, Sector = "C", Fila = 1, Columna = 2, Latitud = -34.6044, Longitud = -58.3823, Estado = EstadoLote.Disponible, Descripcion = "Lote sector C, fila 1, columna 2" },
            new Lote { Id = 9, Sector = "C", Fila = 1, Columna = 3, Latitud = -34.6045, Longitud = -58.3824, Estado = EstadoLote.Ocupado, Descripcion = "Lote sector C, fila 1, columna 3" }
        );

        // Seed difuntos
        modelBuilder.Entity<Difunto>().HasData(
            new Difunto
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "García",
                Cedula = "12345678",
                FechaNacimiento = new DateTime(1940, 5, 15),
                FechaFallecimiento = new DateTime(2010, 3, 20),
                Observaciones = "Descansa en paz",
                LoteId = 1
            },
            new Difunto
            {
                Id = 2,
                Nombre = "María",
                Apellido = "López",
                Cedula = "87654321",
                FechaNacimiento = new DateTime(1955, 8, 10),
                FechaFallecimiento = new DateTime(2018, 11, 5),
                Observaciones = "Siempre en nuestros corazones",
                LoteId = 2
            },
            new Difunto
            {
                Id = 3,
                Nombre = "Carlos",
                Apellido = "Rodríguez",
                Cedula = "11223344",
                FechaNacimiento = new DateTime(1930, 12, 1),
                FechaFallecimiento = new DateTime(2020, 7, 14),
                Observaciones = "En memoria eterna",
                LoteId = 5
            }
        );
    }
}
