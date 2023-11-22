using Market.Shared.Domain;

namespace ProductCatalog.Domain.Product;

public class Unit : BaseIdEntity
{
    public string Abbriviation { get; set; }
    
    public string  Description { get; set; }

    public static Unit CreateUnit(string abbriviation, string description)
    {
        return new Unit()
        {
            Id= Guid.NewGuid(),
            Abbriviation = abbriviation,
            Description = description
        };
    }
}