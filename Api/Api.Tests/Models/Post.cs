using System;
using Newtonsoft.Json;

namespace Api.Tests.Models
{
    public class Post    
    {
        [JsonProperty("id")]
        public int Id { get; set; } 

        [JsonProperty("userId")]
        public int UserId { get; set; } 

        [JsonProperty("title")]
        public string Title { get; set; } 

        [JsonProperty("body")]
        public string Body { get; set; }

        protected bool Equals(Post other)
        {
            return Id == other.Id && UserId == other.UserId && Title == other.Title && Body == other.Body;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Post) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserId, Title, Body);
        }
    }
}