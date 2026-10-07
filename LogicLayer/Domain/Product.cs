using LogicLayer.Domain;


    public class Product
{
    private readonly List<Photo> _photos = new();

    public string Title { get; }
    public string Description { get; }
    public int Stock { get; private set; }
    public string OEM { get; }
    public decimal Price { get; }
    public bool IsActive { get; private set; }
    public Category Category { get; }
    public Brand Brand { get; }
    public IReadOnlyList<Photo> Photos
    {
        get { return _photos; }
    }

    public Product(string title, string description, int stock, string oem, decimal price,
                   Category category, Brand brand, Photo[] photos)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Titel is verplicht");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Beschrijving is verplicht");
        if (stock < 0)
            throw new ArgumentException("Voorraad mag niet negatief zijn");
        if (string.IsNullOrWhiteSpace(oem))
            throw new ArgumentException("OEM is verplicht");
        if (price <= 0)
            throw new ArgumentException("Prijs moet groter zijn dan 0");
        if (category == null)
            throw new ArgumentException("Categorie is verplicht");
        if (brand == null)
            throw new ArgumentException("Merk is verplicht");
        if (photos == null)
            throw new ArgumentException("Minstens één foto is verplicht");
        if (photos.Length == 0)
            throw new ArgumentException("Minstens één foto is verplicht");

        Title = title;
        Description = description;
        Stock = stock;
        OEM = oem;
        Price = price;
        Category = category;
        Brand = brand;
        _photos.AddRange(photos);
        IsActive = true; 
    }

    public void AddPhoto(Photo photo)
    {
        _photos.Add(photo);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}

