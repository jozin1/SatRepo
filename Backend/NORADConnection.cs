using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using System;
using System.Linq;

namespace EFCdemo
{
    class NORADConnection
    {
        //public static readonly HttpClient client = new HttpClient();
        public static string msg;

        public static async void RefreshAsync()
        {
            using (HttpClient client = new HttpClient())
            using (var context = new SatContext())
            {

                string line, Line1, Line2, Line3;

                foreach(string sufix in TypesContainer.sufixes) //powtórzenie dla każdego sufiksu
                {
                    //AcquireData(sufix).Wait();                  //zapytanie do NORAD (do konkretnego zestawu satelitów)
                    msg = await AcquireData(sufix, client);
                    Console.Write(msg);
                    
                    StringReader reader = new StringReader(msg.ToString()); //StringReader umożliwia odczytywanie wiersz po wierszu
                    while (true)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            Line1 = line;                       //odczytywanie kolejnych linii
                            line = reader.ReadLine();           //utworzenie nowego obiektu sat nastąpi jeżeli plik kończy się właściwie
                            if (line != null)
                            {
                                Line2 = line;
                                line = reader.ReadLine();
                                if (line != null)
                                {
                                    Line3 = line;
                                    var Sat = new Sat(TypesContainer.ToType(sufix), Line1, Line2, Line3);
                                    if (context.Sats.Any(o => o.Line1 == Line1))
                                        context.Update(Sat);
                                     context.Add(Sat);      //dodanie do listy nowego satelity lub zmiana tle
                                }
                            }
                        }
                        else
                        {
                            break;
                        }

                    }
                }               
                context.SaveChanges();
            }
            TypesContainer.initialization();
        }
        


        private static async Task<string> AcquireData(string sufix, HttpClient client)         //zapytanie do NORAD
        {
            client.DefaultRequestHeaders.Accept.Clear();            //tworzenie headerów zapytania
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Add("User-Agent", "test");
            var adress = "https://celestrak.com/NORAD/elements/"+sufix;
            var stringTask = await client.GetStringAsync(adress);         //zapytanie w nowym wątku
            return stringTask;
            //msg = await stringTask;                                 //oczekiwanie na odpowiedź
        }
    }
}