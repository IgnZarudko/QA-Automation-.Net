using System.Collections.Generic;
using Api.Tests.Models;
using log4net;

namespace Api.Tests.Utils
{
    public class PostsChecker
    {
        public static bool IsListSortedById(List<Post> posts)
        {
            int i = 0;
            LogManager.GetLogger(typeof(PostsChecker)).Info($"Got {posts.Count} posts to check");
            do
            {
                if (posts[i + 1].id <= posts[i].id)
                {
                    LogManager.GetLogger(typeof(PostsChecker)).Error($"Post with id {posts[i].id} stays before post with id {posts[i + 1].id}");
                    return false;
                }
                i++;
            } while (i + 1 < posts.Count);

            return true;
        }
    }
}