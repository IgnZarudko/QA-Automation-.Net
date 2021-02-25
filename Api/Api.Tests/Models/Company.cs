using System;
using Newtonsoft.Json;

namespace Api.Tests.Models
{
    public class Company    
    {
        [JsonProperty("name")]
        public string Name { get; set; } 

        [JsonProperty("catchPhrase")]
        public string CatchPhrase { get; set; } 

        [JsonProperty("bs")]
        public string Bs { get; set; }

        protected bool Equals(Company other)
        {
            return Name == other.Name && CatchPhrase == other.CatchPhrase && Bs == other.Bs;
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
            return HashCode.Combine(Name, CatchPhrase, Bs);
        }
    }
}