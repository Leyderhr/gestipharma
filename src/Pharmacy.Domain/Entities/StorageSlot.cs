using Pharmacy.Domain.Exceptions;

namespace Pharmacy.Domain.Entities;

/// <summary>
/// Una posición física del almacén robotizado que guarda un tipo de medicamento.
/// </summary>
public class StorageSlot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Code { get; private set; }          // p. ej. "A-03-12"
    public Guid MedicineId { get; private set; }
    public int Capacity { get; private set; }
    public int Quantity { get; private set; }

    public StorageSlot(string code, Guid medicineId, int capacity)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("El código es obligatorio.", nameof(code));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        Code = code.Trim();
        MedicineId = medicineId;
        Capacity = capacity;
    }

    public void Add(int units)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(units);
        if (Quantity + units > Capacity)
            throw new DomainException($"La posición {Code} no admite {units} unidades más (capacidad {Capacity}).");
        Quantity += units;
    }

    public void Remove(int units)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(units);
        if (units > Quantity)
            throw new DomainException($"La posición {Code} solo tiene {Quantity} unidades.");
        Quantity -= units;
    }
}
