using ClienteCrud.Domain.Entities;

namespace ClienteCrud.Domain.Interfaces;

/// <summary>
/// Contrato del repositorio CRUD para Cliente.
/// </summary>
public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObtenerTodosAsync();
    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task<Cliente> CrearAsync(Cliente cliente);
    Task ActualizarAsync(Cliente cliente);
    Task EliminarAsync(int id);
    Task<bool> ExisteEmailAsync(string email, int? excluirId = null);
}
