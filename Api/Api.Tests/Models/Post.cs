using System;

namespace Api.Tests.Models
{
    public class Post
    {
        public int userId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        protected bool Equals(Post other)
        {
            return userId == other.userId && id == other.id && title == other.title && body == other.body;
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
            return HashCode.Combine(userId, id, title, body);
        }
    }
}