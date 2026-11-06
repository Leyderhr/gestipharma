namespace Pharmacy.Domain.Entities;

public class Medicine
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Barcode { get; private set; }

    public Medicine(string name, string barcode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre es obligatorio.", nameof(name));
        if (string.IsNullOrWhiteSpace(barcode))
            throw new ArgumentException("El código de barras es obligatorio.", nameof(barcode));

        Name = name.Trim();
        Barcode = barcode.Trim();
    }
}
