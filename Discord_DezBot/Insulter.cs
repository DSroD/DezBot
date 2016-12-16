using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Discord_DezBot
{
    static class Insulter
    {

        public static string Insult(string name, string sender)
        {

            if(!File.Exists("Resources/Insults.xml"))
            {
                string wrt = @"<?xml version=""1.0"" encoding=""utf - 8"" ?>
<insults>
  <insult>%user% is a somehting, no insults set yet, sorry %sender% :(</insult>
<insults>";
                File.WriteAllText("Resources/Insults.xml", wrt);
            }

            XDocument doc = XDocument.Load("Resources/Insults.xml");
            var insults = doc.Descendants("insult");

            Random r = new Random();
            int g = r.Next(0, insults.Count<XElement>());

            string ret = insults.ToArray<XElement>()[g].Value;

            ret = ret.Replace("%user%", name);
            ret = ret.Replace("%sender%", sender);

            return ret;

        }

    }
}
