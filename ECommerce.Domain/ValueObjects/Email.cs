using System.Text.RegularExpressions;

namespace ECommerce.Domain.ValueObjects;

// Los Value Objects son inmutables: una vez creados no cambian.
// Se comparan por VALOR, no por referencia.
public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El email no puede estar vacío.");

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException($"'{value}' no es un email válido.");

        Value = value.ToLowerInvariant();
    }

    // Igualdad por valor
    public bool Equals(Email? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Email e && Equals(e);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    // Conversión implícita para comodidad
    public static implicit operator string(Email email) => email.Value;
}
