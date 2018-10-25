using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.Serialization.Json;
using EFCdemo;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //load data from NORAD to local database, should be used at least once a day
            //NORADConnection.RefreshAsync();                                               //UNCOMMENT TO REFRESH DB
            //copy objects from DbSet to another List in order to serialize them and send to client
            TypesContainer.initialization();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
