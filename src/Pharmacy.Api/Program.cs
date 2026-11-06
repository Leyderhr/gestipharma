var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Endpoint de comprobación: si ves esto en el navegador, el proyecto funciona.
app.MapGet("/", () => "Pharmacy Robotics API en marcha");
app.MapGet("/health", () => Results.Ok(new { status = "ok", timeUtc = DateTime.UtcNow }));

app.Run();

// Necesario para poder probar la API con WebApplicationFactory más adelante.
public partial class Program;
