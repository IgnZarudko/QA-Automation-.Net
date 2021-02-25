using System;
using Newtonsoft.Json;

namespace Api.Tests.Models
{
    public class User    
    {
        [JsonProperty("id")]
        public int Id { get; set; } 

        [JsonProperty("name")]
        public string Name { get; set; } 

        [JsonProperty("username")]
        public string Username { get; set; } 

        [JsonProperty("email")]
        public string Email { get; set; } 

        [JsonProperty("address")]
        public Address Address { get; set; } 

        [JsonProperty("phone")]
        public string Phone { get; set; } 

        [JsonProperty("website")]
        public string Website { get; set; } 

        [JsonProperty("company")]
        public Company Company { get; set; }

        protected bool Equals(User other)
        {
            return Id == other.Id && Name == other.Name && Username == other.Username && Email == other.Email && Equals(Address, other.Address) && Phone == other.Phone && Website == other.Website && Equals(Company, other.Company);
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
            return HashCode.Combine(Id, Name, Username, Email, Address, Phone, Website, Company);
        }
    }
}