using ProductCatalog.Application.Common.Services;

namespace ProductCatalog.Application.IntegrationTests.Data;
internal static class SeedData
{

    public static readonly Guid Catalog1Id = Guid.Parse("F2899CF0-9A2D-416A-8685-AF176F4AF73C");
    public static readonly Guid Catalog2Id = Guid.Parse("8B84F6B8-6F71-445A-85F2-112CED276338");

    public static readonly Guid Unit1Id1 = Guid.Parse("4C587EE3-25DC-424E-826B-390534E67DD9");

    public static readonly Guid Unit1Id2 = Guid.Parse("1E093B6D-0903-49B5-8E93-B03CB59DF773");

    private static Domain.Catalogs.Catalog catalog1 = new Domain.Catalogs.Catalog()
    {
        Id = Catalog1Id,
        Name = "Catalog 1",
        Description = string.Empty
    };

    private static Domain.Catalogs.Catalog catalog2 = new Domain.Catalogs.Catalog()
    {
        Id = Catalog2Id,
        Name = "Catalog 2",
        Description = string.Empty
    };

    public static async Task SeedAdditionalDataAsync(IProductCatalogDbContext context)
    {
        await SeedCategoriesAsync(context);
        await SeedUnitsAsync(context);
    }

    private static async Task SeedCategoriesAsync(IProductCatalogDbContext context)
    {
        await context.Categories.AddAsync(catalog1);
        await context.Categories.AddAsync(catalog2);

        await context.SaveChangesAsync(CancellationToken.None);
    }

    private static Domain.Product.Unit unit1 = new Domain.Product.Unit()
    {
        Id = Unit1Id1,
        Abbriviation = "kg",
        Description = string.Empty
    };

    private static Domain.Product.Unit unit2 = new Domain.Product.Unit()
    {
        Id = Unit1Id2,
        Abbriviation = "cm",
        Description = string.Empty
    };


    private static async Task SeedUnitsAsync(IProductCatalogDbContext context)
    {
        await context.Units.AddAsync(unit1);
        await context.Units.AddAsync(unit2);

        await context.SaveChangesAsync(CancellationToken.None);
    }

}
