using System;
using System.Net;
using Discord;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Discord_DezBot.UrbanDictionaryPlugin
{
    class UrbanDictionaryPlugin : Plugin
    {

        private WebClient wc;

        override public Command[] Command { get; protected set; }

        public UrbanDictionaryPlugin(DezClient client) : base(client, "UrbanDictionaryPlugin")
        {
            wc = new WebClient();
            Command = new Command[]{new Command("urbandic", "Finds definition on Urban Dictionary", this, (m, args) =>
                             {
                                if(args.Length == 0 || args == null)
                                {
                                    return "Please enter search term!";
                                }
                                
                                string arg = "";
                                foreach(string s in args)
                                {
                                    arg += s + " ";
                                }
                                string req = wc.DownloadString(new Uri("http://api.urbandictionary.com/v0/define?term="+arg));
                                
                                MatchCollection col = Regex.Matches(req, @"(?<=definition"":"")[ -z~-🧀]+(?="",""permalink)");
                                MatchCollection col2 = Regex.Matches(req, @"(?<=example"":"")[ -z~-🧀]+(?="",""thumbs_down)");
                                
                                if(col.Count == 0)
                                {
                                    return "Nothing found :(";
                                }
                                
                                Random r = new Random();
                                string outpt = "Failed fetching data from Urban Dictionary, please try later!";
                                int max = r.Next(0, col.Count);
                                for(int i = 0; i <= max; i++)
                                {
                                    outpt = "```Definition of " + arg + "\r\n\r\n" + col[i].Value + "```*```" + col2[i].Value + "```*";
                                }

                                outpt = outpt.Replace("\\r", "\r");
                                outpt = outpt.Replace("\\n", "\n");

                                return outpt;

                             })};
        }

        public override string ParseCommand(Message m, Command cmd, string[] args = null)
        {
            return cmd.Run(m, args);
        }
    }
}
