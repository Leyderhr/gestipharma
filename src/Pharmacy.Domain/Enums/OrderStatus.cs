namespace Pharmacy.Domain.Enums;

/// <summary>
/// Estados posibles de una orden de dispensación.
/// Equivale a un enum de Java, pero sin lógica propia (la lógica vive en DispensingOrder).
/// </summary>
public enum OrderStatus
{
    Pending,    // Creada, esperando a que el robot la tome
    Picking,    // El robot la está procesando
    Dispensed,  // Terminada con éxito (estado final)
    Failed,     // Falló durante el proceso (estado final)
    Cancelled   // Cancelada antes de empezar (estado final)
}
