using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if(!context.Posts.Any())
            {
                var Posts = new List<Post>
                    {
                        new Post {
                            Title = "First Post!",
                            Date = DateTime.Now.AddDays(-10),
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                        },
                        new Post {
                            Title = "This is My Second Post!",
                            Date = DateTime.Now.AddDays(-7),
                            Body = "Dolor sed viverra ipsum nunc aliquet bibendum enim."
                        },
                        new Post {
                            Title = "Another Day, Another Post!",
                            Date = DateTime.Now.AddDays(-4),
                            Body = "In massa tempor nec feugiat."
                        }
                    };

                context.Posts.AddRange(Posts);
                context.SaveChanges();
            }
        }
    }
}