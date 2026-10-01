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
    public IReadOnlyList<Photo> Photos => _photos;

    public Product(string title, string description, int stock, string oem, decimal price,
                   Category category, Brand brand, Photo[] photos)
    {
        

        Title = title;
        Description = description;
        Stock = stock;
        OEM = oem;
        Price = price;
        Category = category;
        Brand = brand;
        _photos.AddRange(photos);
        IsActive = false; 
    }

    public void IncreaseStock(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Het aantal moet groter zijn dan 0");
        }
        Stock += amount;
    }

    public void DecreaseStock(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Het aantal moet groter zijn dan 0");
        }
        if (amount > Stock)
        {
            throw new ArgumentException("Er is niet genoeg voorraad");
        }
        Stock -= amount;
    }

    public void AddPhoto(Photo photo)
    {
        _photos.Add(photo);
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}

