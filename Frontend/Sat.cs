using System.Runtime.Serialization;
using Zeptomoby.OrbitTools;
using System;


namespace WebAPIClient
{
    [DataContract(Name="Sat")]
    public class Sat
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "Line1")]
        public string Line1 { get; set; }

        [DataMember(Name = "Line2")]
        public string Line2 { get; set; }

        [DataMember(Name = "Line3")]
        public string Line3 { get; set; }

        private Tle TLE { get; set; }

        private Satellite PlaceHolder { get; set; }

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

    }
}
