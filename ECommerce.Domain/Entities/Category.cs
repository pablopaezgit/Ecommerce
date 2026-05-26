namespace ECommerce.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    // Constructor vacío requerido por EF Core
    private Category() { }

    // Constructor de negocio con validaciones
    public Category(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la categoría es obligatorio.");

        Id          = Guid.NewGuid();
        Name        = name;
        Description = description;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("El nombre no puede estar vacío.");

        Name = newName;
    }
}
