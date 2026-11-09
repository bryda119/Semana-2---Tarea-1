using ClienteCrud.Domain.Entities;

namespace ClienteCrud.Domain.Interfaces;

/// <summary>
/// Contrato de la capa de negocio para operaciones CRUD de Cliente.
/// </summary>
public interface IClienteService
{
    Task<IEnumerable<Cliente>> ListarAsync();
    Task<Cliente?> ObtenerAsync(int id);
    Task<(bool Ok, string Mensaje, Cliente? Cliente)> CrearAsync(Cliente cliente);
    Task<(bool Ok, string Mensaje)> ActualizarAsync(Cliente cliente);
    Task<(bool Ok, string Mensaje)> EliminarAsync(int id);
}
