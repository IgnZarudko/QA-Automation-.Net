using System;

namespace Api.Tests.Models
{
    public class User {
        public int id { get; set; } 
        public string name { get; set; } 
        public string username { get; set; } 
        public string email { get; set; } 
        public Address address { get; set; } 
        public string phone { get; set; } 
        public string website { get; set; } 
        public Company company { get; set; }

        protected bool Equals(User other)
        {
            return id == other.id && name == other.name && username == other.username && email == other.email && Equals(address, other.address) && phone == other.phone && website == other.website && Equals(company, other.company);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name, username, email, address, phone, website, company);
        }
    }
}