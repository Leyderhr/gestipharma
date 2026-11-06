using Pharmacy.Domain.Enums;
using Pharmacy.Domain.Exceptions;

namespace Pharmacy.Domain.Entities;

/// <summary>
/// Orden para que el robot dispense unidades de un medicamento.
/// Su ciclo de vida es una MÁQUINA DE ESTADOS: solo se permiten ciertas transiciones.
///
///   Pending ──► Picking ──► Dispensed
///      │           │
///      │           └──────► Failed
///      ├──────────────────► Failed
///      └──────────────────► Cancelled
/// </summary>
public class DispensingOrder
{
    // Tabla de transiciones permitidas: estado actual -> estados a los que puede ir.
    private static readonly Dictionary<OrderStatus, OrderStatus[]> AllowedTransitions = new()
    {
        [OrderStatus.Pending]   = [OrderStatus.Picking, OrderStatus.Cancelled, OrderStatus.Failed],
        [OrderStatus.Picking]   = [OrderStatus.Dispensed, OrderStatus.Failed],
        [OrderStatus.Dispensed] = [],
        [OrderStatus.Failed]    = [],
        [OrderStatus.Cancelled] = []
    };

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid MedicineId { get; private set; }
    public int Quantity { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public string? FailureReason { get; private set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;

    public DispensingOrder(Guid medicineId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        MedicineId = medicineId;
        Quantity = quantity;
    }

    public bool CanTransitionTo(OrderStatus next) => AllowedTransitions[Status].Contains(next);

    public void StartPicking() => TransitionTo(OrderStatus.Picking);
    public void MarkDispensed() => TransitionTo(OrderStatus.Dispensed);
    public void Cancel() => TransitionTo(OrderStatus.Cancelled);

    public void Fail(string reason)
    {
        TransitionTo(OrderStatus.Failed);
        FailureReason = reason;
    }

    private void TransitionTo(OrderStatus next)
    {
        if (!CanTransitionTo(next))
            throw new DomainException($"Transición no permitida: {Status} -> {next}.");

        Status = next;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
