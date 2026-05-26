namespace ECommerce.Domain.Exceptions;

// Excepción base para todos los errores del dominio.
// Permite atraparlos en el middleware con un solo catch.
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

// Se lanza cuando se busca algo que no existe.
public class NotFoundException : DomainException
{
    public NotFoundException(string entity, Guid id)
        : base($"{entity} con Id '{id}' no fue encontrado.") { }
}

// Se lanza cuando una regla de negocio es violada.
public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message) { }
}
