# Conceptos clave del proyecto (para quien viene de Java)

## C# vs Java: tabla rápida
| Java | C# |
|---|---|
| `package` / `import` | `namespace` / `using` |
| getters/setters | **propiedades**: `public string Name { get; set; }` |
| `final` | `readonly` (campos) / `const` (constantes) |
| `List<String>` | `List<string>` (`string` en minúscula) |
| `Optional<T>` / null | tipos anulables: `string?` (el compilador avisa de nulos) |
| Streams | **LINQ**: `lista.Where(x => x.Age > 18).Select(x => x.Name).ToList()` |
| interfaces | convención: prefijo `I` (`IMedicineRepository`) |
| Maven/Gradle (`pom.xml`) | **NuGet** + archivos `.csproj` |
| Spring Boot | **ASP.NET Core** |
| `@Autowired` | inyección por constructor, registrada en `Program.cs` |
| `CompletableFuture` | `Task<T>` con `async` / `await` |
| JUnit `@Test` | xUnit `[Fact]` |
| JPA / Hibernate | Entity Framework Core |

## Worker Services / BackgroundService
Proceso que corre **en segundo plano dentro de la aplicación**, sin que nadie haga una petición HTTP.
Se hereda de `BackgroundService` y se implementa `ExecuteAsync`, que se ejecuta mientras la app vive.
Equivale a un `@Scheduled` / hilo demonio de Spring. Aquí será el **robot simulado**.

## Cola de órdenes
La API recibe una orden y **no la ejecuta ella**: la deja en una cola. El robot las saca de una en una (FIFO).
Así la API responde rápido y el robot trabaja a su ritmo. En C# usaremos `System.Threading.Channels`
(cola segura entre hilos) y las órdenes quedarán también en PostgreSQL con su estado.

## Máquina de estados
Un objeto está en un estado a la vez y solo puede pasar a ciertos estados concretos.
Mira `DispensingOrder`: `Pending → Picking → Dispensed`, con salidas a `Failed` / `Cancelled`.
Una transición ilegal (p. ej. cancelar algo ya dispensado) lanza una excepción.

## SignalR
Biblioteca de ASP.NET Core para **tiempo real**: el servidor *empuja* datos a los clientes conectados
(WebSockets por debajo), en vez de que el cliente pregunte cada segundo.
Aquí: cuando el robot cambia el estado de una orden, los paneles conectados se enteran al instante.
Se define un `Hub` y se usa `hubContext.Clients.All.SendAsync(...)`.

## JWT y RBAC
- **JWT**: token firmado que el cliente envía en `Authorization: Bearer <token>`; el servidor lo valida sin consultar la BD.
- **RBAC** (*Role-Based Access Control*): los permisos dependen del **rol**, no del usuario.
  Ej.: `Farmaceutico` crea órdenes; `Admin` además gestiona stock y usuarios.
  En la práctica: un claim `role` en el JWT y `[Authorize(Roles = "Admin")]` en los endpoints.

## xUnit
| JUnit | xUnit |
|---|---|
| `@Test` | `[Fact]` |
| `@ParameterizedTest` + `@ValueSource` | `[Theory]` + `[InlineData(...)]` |
| `@BeforeEach` | el **constructor** de la clase de test |
| `assertEquals(esperado, real)` | `Assert.Equal(esperado, real)` |
| `assertThrows` | `Assert.Throws<T>(() => ...)` |
xUnit crea una instancia nueva de la clase por cada prueba, por eso no existe `@BeforeEach`.
