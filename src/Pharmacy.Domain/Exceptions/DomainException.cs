namespace Pharmacy.Domain.Exceptions;

/// <summary>Error que indica que se violó una regla de negocio.</summary>
public class DomainException(string message) : Exception(message);
