using Zeptomoby.OrbitTools;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;

namespace ClassLibrary2
{
    public class Kalkulator
    {
        string Type, Line1, Line2, Line3;
        public Kalkulator(ClassLibrary1.Sat sat)
        {
            Type = sat.Type;
            Line1 = sat.Line1;
            Line2 = sat.Line2;
            Line3 = sat.Line3;
        }

        private Tle TLE { get; set; }
        private Satellite PlaceHolder { get; set; }

        private static readonly HttpClient client = new HttpClient();

        static readonly string adress1 = "text/html";

        static readonly string host1 = "https://localhost:5001/api/satellites";

        public void TLEInitialize()
        {
            TLE = new Tle(Line1, Line2, Line3);
            PlaceHolder = new Satellite(TLE);
        }

        public void ExampleOutput()
        {
            DateTime utc = DateTime.Now;
            utc.AddHours(2); //to UTC
            Eci eci = PlaceHolder.PositionEci(PlaceHolder.Orbit.TPlusEpoch(utc).TotalMinutes);
            Console.WriteLine("Satellite type, tle and current position: \n");
            Console.WriteLine(Type);
            Console.WriteLine(Line1);
            Console.WriteLine(Line2);
            Console.WriteLine(Line3);
            Console.WriteLine(eci.Position.X + " " + eci.Position.Y + " " + eci.Position.Z);
        }

        public static async Task<List<ClassLibrary1.Sat>> ProcessSatellites()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(adress1));
            client.DefaultRequestHeaders.Add("User-Agent", "test");

            var serializer = new DataContractJsonSerializer(typeof(List<ClassLibrary1.Sat>));
            var streamTask = client.GetStreamAsync(host1);

            var satellites = serializer.ReadObject(await streamTask) as List<ClassLibrary1.Sat>;
            return satellites;
        }
    }
}
