using ClienteCrud.Domain.Entities;
using ClienteCrud.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClienteCrud.Data.Repositories;

/// <summary>
/// Implementación del CRUD de Cliente con Entity Framework Core.
/// </summary>
public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .OrderByDescending(c => c.FechaRegistro)
            .ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> CrearAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task ActualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente is null)
        {
            return;
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteEmailAsync(string email, int? excluirId = null)
    {
        return await _context.Clientes.AnyAsync(c =>
            c.Email == email && (!excluirId.HasValue || c.Id != excluirId.Value));
    }
}
