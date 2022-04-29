using EFWebApi_v2.Data;
using EFWebApi_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace EFWebapi_v2.Models
{
    public static class PrepDB
    {

        private static readonly string[] Resulties = new[]
        {
            "Понедельник", "Вторник", "Среда", "Четверг", "Пянтница",
            "Суббота", "Воскресенье"
        };

        
        private static readonly string[] Paramsies = new[]
        {
            "Year 1985, Month 4, Day 2", "Year 1945, Month 12, Day 26", "Year 1685, Month 2, Day 13",
            "Year 1673, Month 7, Day 6", "Year 1921, Month 5, Day 20", "Year 1990, Month 10, Day 1",
            "Year 1457, Month 10, Day 2"
        };

        private const int DEFAULT_TABLE_SIZE = 10;

        public static WebApplicationBuilder PopulateDb(this WebApplicationBuilder builder)
        {
            using var serviceProvider = builder.Services.BuildServiceProvider();

            var context = serviceProvider.GetService<SqlEfContex>();

            if (context == null)
                throw new Exception("Contex is not created!");

            Console.WriteLine("Apllying migrations");

            context.Database.Migrate();

            if (!context.Days.Any())
            {
                Console.WriteLine("Creating default content");

                var rnd = new Random();

                var dtArray = new Record[DEFAULT_TABLE_SIZE];
                for(int i = 0; i < dtArray.Length; i++)
                {
                    dtArray[i] = new Record()
                    {
                        Params = Paramsies[rnd.Next(Paramsies.Length)],
                        Created = DateTime.Now,
                        Result = Resulties[rnd.Next(Resulties.Length)],
                    };
                }

                context.Days.AddRange(dtArray);
                context.SaveChanges();
            }

            return builder;
        }
    }
}

