using HelloWebdriver.Tests.model;
using HelloWebdriver.Tests.util;

namespace HelloWebdriver.Tests.service
{
    public static class UserCreator
    {
        public static User WithCredentialsFromConfig(TestConfig config)
        {
            return new User(config.UserLogin, config.UserPassword, config.Username, config.UserEmail);
        }

        public static User WithoutLogin(TestConfig config)
        {
            return new User("", config.UserPassword, config.Username, config.UserEmail);
        }

        public static User WithoutPassword(TestConfig config)
        {
            return new User(config.UserLogin, "", config.Username, config.UserEmail);
        }
        
        public static User WithoutCredentials(TestConfig config)
        {
            return new User("", "", config.Username, config.UserEmail);
        }
    }
}