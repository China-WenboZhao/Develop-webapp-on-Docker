using MoviesService.Models;
using System;
using System.Linq;

namespace MoviesService.Data
{
    public class DBinitializer
    {
        public static void init(MovieContext mcontext)
        {
            try {
                mcontext.Database.EnsureCreated();

            
                if (mcontext.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                var publishcompanys = new PublishCompany[]
                {
                    new PublishCompany { CompanyID = 10000, CompanyName = "博纳" },
                    new PublishCompany { CompanyID = 10001, CompanyName = "迪士尼" }
                };
                foreach (PublishCompany c in publishcompanys)
                {
                    mcontext.PublishCompany.Add(c);
                }
                mcontext.SaveChanges();

                var movies = new Movie[]
                {
                    new Movie{ID=1,Title="我不是药神",ReleaseDate=DateTime.Parse("2018-07-15"),Genre="剧情",Price=10.0M,CompanyID=10000},
                    new Movie{ID=2,Title="侏罗纪世界2",ReleaseDate=DateTime.Parse("2018-07-10"),Genre="冒险",Price=11.0M,CompanyID=10001}
                };
                foreach (Movie m in movies)
                {
                    mcontext.Movie.Add(m);
                    Console.WriteLine(m.Title);
                }
                 mcontext.SaveChanges();
            }catch(Exception e)
            {

            }
         }
    }
}
