using ClienteCrud.Domain.Entities;
using ClienteCrud.Domain.Interfaces;

namespace ClienteCrud.Business.Services;

/// <summary>
/// Capa de negocio: reglas y validaciones sobre el objeto Cliente.
/// </summary>
public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Cliente>> ListarAsync()
    {
        return _repository.ObtenerTodosAsync();
    }

    public Task<Cliente?> ObtenerAsync(int id)
    {
        return _repository.ObtenerPorIdAsync(id);
    }

    public async Task<(bool Ok, string Mensaje, Cliente? Cliente)> CrearAsync(Cliente cliente)
    {
        cliente.Email = cliente.Email.Trim().ToLowerInvariant();
        cliente.Nombre = cliente.Nombre.Trim();
        cliente.Apellido = cliente.Apellido.Trim();
        cliente.Telefono = cliente.Telefono.Trim();
        cliente.Direccion = cliente.Direccion.Trim();
        cliente.FechaRegistro = DateTime.Now;

        if (await _repository.ExisteEmailAsync(cliente.Email))
        {
            return (false, "Ya existe un cliente registrado con ese correo electrónico.", null);
        }

        var creado = await _repository.CrearAsync(cliente);
        return (true, "Cliente creado correctamente.", creado);
    }

    public async Task<(bool Ok, string Mensaje)> ActualizarAsync(Cliente cliente)
    {
        var existente = await _repository.ObtenerPorIdAsync(cliente.Id);
        if (existente is null)
        {
            return (false, "El cliente no existe.");
        }

        cliente.Email = cliente.Email.Trim().ToLowerInvariant();
        cliente.Nombre = cliente.Nombre.Trim();
        cliente.Apellido = cliente.Apellido.Trim();
        cliente.Telefono = cliente.Telefono.Trim();
        cliente.Direccion = cliente.Direccion.Trim();

        if (await _repository.ExisteEmailAsync(cliente.Email, cliente.Id))
        {
            return (false, "Ya existe otro cliente con ese correo electrónico.");
        }

        // Conservar la fecha original de registro
        existente.Nombre = cliente.Nombre;
        existente.Apellido = cliente.Apellido;
        existente.Email = cliente.Email;
        existente.Telefono = cliente.Telefono;
        existente.Direccion = cliente.Direccion;

        await _repository.ActualizarAsync(existente);
        return (true, "Cliente actualizado correctamente.");
    }

    public async Task<(bool Ok, string Mensaje)> EliminarAsync(int id)
    {
        var existente = await _repository.ObtenerPorIdAsync(id);
        if (existente is null)
        {
            return (false, "El cliente no existe.");
        }

        await _repository.EliminarAsync(id);
        return (true, "Cliente eliminado correctamente.");
    }
}
