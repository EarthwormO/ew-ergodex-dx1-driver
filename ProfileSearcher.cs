using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public class ProfileSearcher
    {
        public Profiles ProfileSearchByName(List<Profiles> ProfileTest, String SearchName)
        {
            return ProfileTest.Find(delegate(Profiles P) { return P.ProfName == SearchName; });
        }

        public Profiles ProfileSearchByPath(List<Profiles> ProfileTest, String SearchPath)
        {
            if (SearchPath == "") return null;
            return ProfileTest.Find(delegate(Profiles P) { return (P.ProfPath == SearchPath & P.ProfEnabled == true); });
        }

    }
}
