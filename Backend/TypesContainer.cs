
using System.Collections.Generic;


namespace EFCdemo
{
    class TypesContainer
    {
        static public List<string> sufixes = new List<string>()
            {
                "visual.txt",
                "amateur.txt"
            };
        static public List<string> types = new List<string>()
            {
                "100 (or so) Brightest",
                "Amateur Radio"
            };

        static public string ToType(string sufix)
        {
            return types[sufixes.IndexOf(sufix)];
        }

        static public string ToSufix(string type)
        {
            return sufixes[types.IndexOf(type)];
        }

        static public List<ClassLibrary1.Sat> spagetti = new List<ClassLibrary1.Sat>();

        static public void initialization()
        {
            using (var context = new SatContext())
            {
                foreach (ClassLibrary1.Sat sat in context.Sats)
                {
                    spagetti.Add(sat);
                }
            }
        }
    }
}
