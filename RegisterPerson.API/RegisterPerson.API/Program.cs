﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AuthJWT.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
