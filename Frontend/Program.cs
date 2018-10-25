using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;


namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        
        static readonly string adress1 = "text/html";

        static readonly string host1 = "https://localhost:5001/api/satellites";

        static void Main(string[] args)
        {
            Console.WriteLine("HTTP RESTFUL API CLIENT");
            Console.WriteLine("press any key if server is up");
            Console.ReadKey();

            var Sats = ProcessSatellites().Result;
            Sat example = Sats.First(o => o.Id == 8); 
            example.TLEInitialize();
            example.ExampleOutput();

            Console.WriteLine("PRESS ANY KEY TO EXIT");
            Console.ReadKey();
        }

        private static async Task<List<Sat>> ProcessSatellites()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(adress1));
            client.DefaultRequestHeaders.Add("User-Agent", "test");

            var serializer = new DataContractJsonSerializer(typeof(List<Sat>));
            var streamTask = client.GetStreamAsync(host1);
            
            var satellites = serializer.ReadObject(await streamTask) as List<Sat>;
            return satellites;
        }
    }
}
