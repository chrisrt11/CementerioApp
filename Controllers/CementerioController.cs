using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CementerioApp.Data;
using CementerioApp.Models;

namespace CementerioApp.Controllers;

public class CementerioController : Controller
{
    private readonly ApplicationDbContext _context;

    public CementerioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET /cementerio/mapa
    public IActionResult Mapa()
    {
        return View();
    }

    // GET /api/lotes/filtrar?sector=A&estado=Ocupado
    [HttpGet("/api/lotes/filtrar")]
    public async Task<IActionResult> FiltrarLotes([FromQuery] string? sector, [FromQuery] string? estado)
    {
        var query = _context.Lotes.Include(l => l.Difunto).AsQueryable();

        if (!string.IsNullOrWhiteSpace(sector))
        {
            query = query.Where(l => l.Sector == sector);
        }

        if (!string.IsNullOrWhiteSpace(estado) && Enum.TryParse<EstadoLote>(estado, out var estadoEnum))
        {
            query = query.Where(l => l.Estado == estadoEnum);
        }

        var lotes = await query.Select(l => new
        {
            l.Id,
            l.Sector,
            l.Fila,
            l.Columna,
            l.Latitud,
            l.Longitud,
            Estado = l.Estado.ToString(),
            l.Descripcion,
            Identificador = l.Identificador,
            Difunto = l.Difunto == null ? null : new
            {
                l.Difunto.Id,
                l.Difunto.Nombre,
                l.Difunto.Apellido,
                l.Difunto.Cedula,
                FechaNacimiento = l.Difunto.FechaNacimiento.ToString("dd/MM/yyyy"),
                FechaFallecimiento = l.Difunto.FechaFallecimiento.ToString("dd/MM/yyyy"),
                l.Difunto.Observaciones
            }
        }).ToListAsync();

        return Json(lotes);
    }

    // GET /api/lotes/{id}
    [HttpGet("/api/lotes/{id}")]
    public async Task<IActionResult> ObtenerLote(int id)
    {
        var lote = await _context.Lotes.Include(l => l.Difunto)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lote == null)
        {
            return NotFound(new { mensaje = "Lote no encontrado" });
        }

        return Json(new
        {
            lote.Id,
            lote.Sector,
            lote.Fila,
            lote.Columna,
            lote.Latitud,
            lote.Longitud,
            Estado = lote.Estado.ToString(),
            lote.Descripcion,
            Identificador = lote.Identificador,
            Difunto = lote.Difunto == null ? null : new
            {
                lote.Difunto.Id,
                lote.Difunto.Nombre,
                lote.Difunto.Apellido,
                lote.Difunto.Cedula,
                FechaNacimiento = lote.Difunto.FechaNacimiento.ToString("dd/MM/yyyy"),
                FechaFallecimiento = lote.Difunto.FechaFallecimiento.ToString("dd/MM/yyyy"),
                lote.Difunto.Observaciones
            }
        });
    }
}
