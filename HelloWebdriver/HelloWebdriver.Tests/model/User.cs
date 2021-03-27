using System;

namespace HelloWebdriver.Tests.model
{
    public class User
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }

        public User(string userLogin, string userPassword, string username, string userEmail)
        {
            UserLogin = userLogin;
            UserPassword = userPassword;
            Username = username;
            UserEmail = userEmail;
        }

        private bool Equals(User other)
        {
            return Username == other.Username && UserEmail == other.UserEmail;
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
            return HashCode.Combine(Username, UserEmail);
        }
    }
}