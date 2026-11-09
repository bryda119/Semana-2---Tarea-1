using ClienteCrud.Domain.Entities;
using ClienteCrud.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteCrud.Web.Controllers;

public class ClientesController : Controller
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // GET: Clientes
    public async Task<IActionResult> Index()
    {
        var clientes = await _clienteService.ListarAsync();
        return View(clientes);
    }

    // GET: Clientes/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var cliente = await _clienteService.ObtenerAsync(id);
        if (cliente is null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    // GET: Clientes/Create
    public IActionResult Create()
    {
        return View(new Cliente());
    }

    // POST: Clientes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        var resultado = await _clienteService.CrearAsync(cliente);
        if (!resultado.Ok)
        {
            ModelState.AddModelError(nameof(Cliente.Email), resultado.Mensaje);
            return View(cliente);
        }

        TempData["Mensaje"] = resultado.Mensaje;
        return RedirectToAction(nameof(Index));
    }

    // GET: Clientes/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _clienteService.ObtenerAsync(id);
        if (cliente is null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    // POST: Clientes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cliente cliente)
    {
        if (id != cliente.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        var resultado = await _clienteService.ActualizarAsync(cliente);
        if (!resultado.Ok)
        {
            ModelState.AddModelError(string.Empty, resultado.Mensaje);
            return View(cliente);
        }

        TempData["Mensaje"] = resultado.Mensaje;
        return RedirectToAction(nameof(Index));
    }

    // GET: Clientes/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _clienteService.ObtenerAsync(id);
        if (cliente is null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    // POST: Clientes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var resultado = await _clienteService.EliminarAsync(id);
        TempData["Mensaje"] = resultado.Mensaje;
        return RedirectToAction(nameof(Index));
    }
}
