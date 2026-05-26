namespace ECommerce.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public decimal Amount   { get; }
    public string  Currency { get; }

    public Money(decimal amount, string currency = "ARS")
    {
        if (amount < 0)
            throw new ArgumentException("El monto no puede ser negativo.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("La moneda es obligatoria.");

        Amount   = amount;
        Currency = currency.ToUpperInvariant();
    }

    // Suma de dos montos de la misma moneda
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("No se pueden sumar monedas distintas.");

        return new Money(Amount + other.Amount, Currency);
    }

    // Multiplicar por una cantidad (ej: precio × cantidad)
    public Money Multiply(int quantity) => new(Amount * quantity, Currency);

    public bool Equals(Money? other) =>
        other is not null && Amount == other.Amount && Currency == other.Currency;

    public override bool Equals(object? obj) => obj is Money m && Equals(m);
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    public override string ToString() => $"{Currency} {Amount:F2}";

    public static implicit operator decimal(Money money) => money.Amount;
}
