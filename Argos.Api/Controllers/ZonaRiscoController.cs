using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Argos.Api.Data;
using Argos.Api.Models;

namespace Argos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZonaRiscoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ZonaRiscoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ZonaRisco>>> Get()
    {
        return await _context.ZonasRisco.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ZonaRisco>> GetById(int id)
    {
        var zona = await _context.ZonasRisco.FindAsync(id);

        if (zona == null)
            return NotFound();

        return zona;
    }

    [HttpPost]
    public async Task<ActionResult> Post(ZonaRisco zona)
    {
        _context.ZonasRisco.Add(zona);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = zona.Id },
            zona
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, ZonaRisco zona)
    {
        if (id != zona.Id)
            return BadRequest();

        _context.Entry(zona).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var zona = await _context.ZonasRisco.FindAsync(id);

        if (zona == null)
            return NotFound();

        _context.ZonasRisco.Remove(zona);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}