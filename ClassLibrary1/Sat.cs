using System;
using System.Runtime.Serialization;



namespace ClassLibrary1
{
    [DataContract(Name = "Sat")]
    public class Sat
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember(Name = "Type")]
        public string Type { get; set; }
        [DataMember(Name = "Line1")]
        public string Line1 { get; set; }
        [DataMember(Name = "Line2")]
        public string Line2 { get; set; }
        [DataMember(Name = "Line3")]
        public string Line3 { get; set; }

        public Sat(string XType, string XLine1, string XLine2, string XLine3)
        {
            Type = XType;
            Line1 = XLine1;
            Line2 = XLine2;
            Line3 = XLine3;
        }

        public Sat()
        {
        }
    }

    
}
