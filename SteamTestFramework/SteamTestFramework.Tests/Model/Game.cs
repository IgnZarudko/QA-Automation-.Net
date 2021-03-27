using System;

namespace SteamTestFramework.Tests.Model
{
    public class Game : IComparable<Game>
    {
        public string Name { get; set; }
        public string OriginalPrice { get; set; }
        public string NewPrice { get; set; }
        public int DiscountAmount { get; set; }

        public Game(string name, string originalPrice, string newPrice, int discountAmount)
        {
            Name = name;
            OriginalPrice = originalPrice;
            NewPrice = newPrice;
            DiscountAmount = discountAmount;
        }

        protected bool Equals(Game other)
        {
            return Name == other.Name && OriginalPrice == other.OriginalPrice && NewPrice == other.NewPrice && DiscountAmount == other.DiscountAmount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Game) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, OriginalPrice, NewPrice, DiscountAmount);
        }

        public int CompareTo(Game other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return DiscountAmount.CompareTo(other.DiscountAmount);
        }
    }
}