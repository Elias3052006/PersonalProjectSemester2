using System;

namespace LogicLayer.Domain
{
    public class ProductFilter
    {
        public int? CategoryId { get; }
        public int? BrandId { get; }
        public decimal? MinPrice { get; }
        public decimal? MaxPrice { get; }

        public ProductFilter(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice)
        {
            CategoryId = categoryId;
            BrandId = brandId;
            MinPrice = minPrice;
            MaxPrice = maxPrice;

            CheckPrices();
        }

        public static ProductFilter Empty()
        {
            return new ProductFilter(null, null, null, null);
        }

        public bool Matches(Product product)
        {
            if (IdFits(CategoryId, product.Category.CategoryId) == false)
            {
                return false;
            }
            if (IdFits(BrandId, product.Brand.BrandId) == false)
            {
                return false;
            }

            return PriceFits(product.Price);
        }

        private void CheckPrices()
        {
            CheckNotNegative(MinPrice, "De minimumprijs");
            CheckNotNegative(MaxPrice, "De maximumprijs");

            if (MinPrice > MaxPrice)
            {
                throw new ArgumentException("De minimumprijs mag niet hoger zijn dan de maximumprijs");
            }
        }

        private void CheckNotNegative(decimal? price, string name)
        {
            if (price < 0)
            {
                throw new ArgumentException(name + " mag niet negatief zijn");
            }
        }

        private bool IdFits(int? wantedId, int productId)
        {
            if (wantedId == null)
            {
                return true;
            }

            return wantedId == productId;
        }

        private bool PriceFits(decimal price)
        {
            if (MinPrice != null)
            {
                if (price < MinPrice)
                {
                    return false;
                }
            }
            if (MaxPrice != null)
            {
                if (price > MaxPrice)
                {
                    return false;
                }
            }

            return true;
        }
    }
}