using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MusicLibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string urls = FetchURLs();
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(urls);
        }

        private static string FetchURLs()
        {
            string urls = string.Empty;
            using (StreamReader r = new StreamReader("hosting.json"))
            {
                string json = r.ReadToEnd();
                Newtonsoft.Json.JsonTextReader reader = new Newtonsoft.Json.JsonTextReader(new StringReader(json));
                while (reader.Read())
                {
                    if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "urls")
                    {
                        urls = reader.ReadAsString();
                    }

                }
            }
            return urls;
        }
    }
}
