using System;

namespace Api.Tests.Models
{
    public class Post
    {
        public int userId;

        public int id;

        public string title;

        public string body;

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