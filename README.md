# Pharmacy Robotics

Plataforma de gestión de inventario y dispensación automatizada para farmacias.
API REST con **C# / ASP.NET Core (.NET 10)**, **PostgreSQL**, un robot de almacén simulado
(servicio en segundo plano) y monitorización en tiempo real con **SignalR**.

## Arquitectura (Clean Architecture)

```
src/
  Pharmacy.Domain          Entidades y reglas de negocio. No depende de nada.
  Pharmacy.Application     Casos de uso e interfaces (puertos). Depende de Domain.
  Pharmacy.Infrastructure  EF Core, PostgreSQL, implementaciones. Depende de Application.
  Pharmacy.Api             Endpoints HTTP, SignalR, JWT. Punto de entrada.
tests/
  Pharmacy.Tests           Pruebas con xUnit.
```

La regla: las dependencias apuntan siempre hacia el **Domain**.

## Ejecutar

```bash
dotnet build
dotnet test
dotnet run --project src/Pharmacy.Api
```

Abrir la URL que muestre la consola y probar `/health`.

## Hoja de ruta

- [x] Paso 1 – Estructura de la solución, dominio y primeras pruebas
- [ ] Paso 2 – PostgreSQL + Entity Framework Core (migraciones)
- [ ] Paso 3 – Endpoints REST (medicamentos, stock, órdenes)
- [ ] Paso 4 – Robot simulado (BackgroundService + cola de órdenes)
- [ ] Paso 5 – Tiempo real con SignalR
- [ ] Paso 6 – Autenticación JWT + roles
- [ ] Paso 7 – Pruebas de integración, Docker y CI/CD (GitHub Actions)
