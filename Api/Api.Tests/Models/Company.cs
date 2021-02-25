using System;

namespace Api.Tests.Models
{
    public class Company    {
        public string name { get; set; } 
        public string catchPhrase { get; set; } 
        public string bs { get; set; }

        protected bool Equals(Company other)
        {
            return name == other.name && catchPhrase == other.catchPhrase && bs == other.bs;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Company) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, catchPhrase, bs);
        }
    }
}