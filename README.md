# CRUD Clientes

Aplicación web con **ASP.NET Core MVC (.NET 8)**, **MySQL** y **Entity Framework Core** para administrar clientes (crear, listar, editar, consultar y eliminar).

## Cómo está dividido el proyecto

La solución usa **arquitectura por capas**:

| Proyecto | Qué hace |
|---|---|
| `ClienteCrud.Domain` | Define el objeto `Cliente` (POO) y las interfaces del repositorio y del servicio. |
| `ClienteCrud.Data` | Conecta con MySQL mediante EF Core (`ApplicationDbContext`) e implementa el CRUD en `ClienteRepository`. |
| `ClienteCrud.Business` | Aplica reglas de negocio (por ejemplo, email único, limpieza de datos) en `ClienteService`. |
| `ClienteCrud.Web` | Interfaz MVC: controladores, vistas y configuración de la app. |

Flujo típico de una petición:

1. El usuario interactúa en la vista (`ClienteCrud.Web`).
2. El controlador llama a `IClienteService`.
3. El servicio aplica reglas y usa `IClienteRepository`.
4. El repositorio persiste o consulta datos con EF Core en MySQL.

## Cómo funciona el CRUD

Rutas principales del controlador `ClientesController`:

| Acción | Método | Descripción |
|---|---|---|
| `Index` | GET | Lista todos los clientes. |
| `Details` | GET | Muestra un cliente por `Id`. |
| `Create` | GET / POST | Formulario y registro de un nuevo cliente. |
| `Edit` | GET / POST | Formulario y actualización de un cliente existente. |
| `Delete` | GET / POST | Confirmación y eliminación del cliente. |

Campos de la entidad `Cliente`:

- `Id`
- `Nombre`
- `Apellido`
- `Email`
- `Telefono`
- `Direccion`
- `FechaRegistro`

## Cómo funciona el ORM y la base de datos

Se usa **Entity Framework Core** con el proveedor **Pomelo** para MySQL.

- El modelo `Cliente` se mapea a la tabla `Clientes`.
- `ApplicationDbContext` define el `DbSet<Cliente>` y las restricciones (longitudes, email único, etc.).
- Al iniciar la aplicación, `Database.EnsureCreated()` crea automáticamente:
  - la base de datos `ClientesDb`
  - la tabla `Clientes`
- No hace falta crear la base a mano en phpMyAdmin si MySQL está encendido y la conexión es correcta.

Cadena de conexión en `ClienteCrud.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=ClientesDb;User=root;Password=;"
}
```

Si MySQL tiene contraseña, colócala en `Password=`.

## Requisitos

- .NET SDK 8
- MySQL disponible (por ejemplo XAMPP) y en ejecución
- Usuario MySQL configurado en `appsettings.json` (por defecto `root` sin contraseña)

## Cómo ejecutar

1. Inicia MySQL (XAMPP Control Panel o `mysql_start.bat`).
2. Abre una terminal en la carpeta del proyecto web:

```powershell
cd "ClienteCrud\ClienteCrud.Web"
dotnet run
```

3. Abre en el navegador la URL que muestre la consola, normalmente:

```text
http://localhost:5046
```

Desde ahí puedes usar el CRUD completo: agregar, buscar en el listado, ver, editar y eliminar clientes.
