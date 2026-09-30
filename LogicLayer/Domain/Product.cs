namespace LogicLayer.Domain
{
    public class Product
    {
        public string Titel { get;  }
        public string Description {  get;  }
        public int Stock {  get;  }
        public int OEM { get;  }

        public Product(string titel, string description, int stock, int oem) 
        {
            if (string.IsNullOrWhiteSpace(titel))
            {
                throw new ArgumentException("Deze titel is niet ingevuld");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Deze descriptie is niet ingevuld");
            }
            if (stock <= 0)
            {
                throw new ArgumentException("De voorraad kan niet 0 of lager zijn");
            }
            Titel = titel;
            Description = description;
            Stock = stock;
            OEM = oem;
        }

    }
}
