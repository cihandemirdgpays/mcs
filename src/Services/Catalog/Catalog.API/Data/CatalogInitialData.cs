using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync(cancellation))
        {
          return;   
        }
        
        session.Store<Product>(GetPreConfiguratedProducts());
        await session.SaveChangesAsync(cancellation);
        
    }

    private static IEnumerable<Product> GetPreConfiguratedProducts() => new List<Product>
    {
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Wireless Mouse",
            Category = ["Electronics", "Accessories"],
            Description = "Ergonomic wireless mouse with adjustable DPI settings.",
            ImageFile = "wireless_mouse.jpg",
            Price = 29.99m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Mechanical Keyboard",
            Category = ["Electronics", "Accessories"],
            Description = "High-quality mechanical keyboard with customizable RGB backlighting.",
            ImageFile = "mechanical_keyboard.jpg",
            Price = 89.99m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Gaming Headset",
            Category = ["Electronics", "Gaming"],
            Description = "Surround sound gaming headset with a built-in noise-canceling microphone.",
            ImageFile = "gaming_headset.jpg",
            Price = 59.99m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Smartphone Stand",
            Category = ["Accessories", "Office Supplies"],
            Description = "Adjustable smartphone stand for desk with a non-slip base.",
            ImageFile = "smartphone_stand.jpg",
            Price = 14.99m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Portable Charger",
            Category = ["Electronics", "Travel"],
            Description = "High-capacity portable charger with fast-charging capability.",
            ImageFile = "portable_charger.jpg",
            Price = 39.99m
        }
    };

}