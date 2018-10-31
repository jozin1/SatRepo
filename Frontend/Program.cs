using System;

using System.Linq;


namespace WebAPIClient
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("HTTP RESTFUL API CLIENT");
            Console.WriteLine("press any key if server is up");
            Console.ReadKey();

            var Sats = ClassLibrary2.Kalkulator.ProcessSatellites().Result;
            ClassLibrary1.Sat example = Sats.First(o => o.Id == 8);
            ClassLibrary2.Kalkulator kalkulator = new ClassLibrary2.Kalkulator(example);

            kalkulator.TLEInitialize();
            kalkulator.ExampleOutput();

            Console.WriteLine("PRESS ANY KEY TO EXIT");
            Console.ReadKey();
        }

        
    }
}
