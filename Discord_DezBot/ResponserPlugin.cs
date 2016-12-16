using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;

using System.Xml;

using Discord;
using System.IO;

namespace Discord_DezBot
{
    class ResponserPlugin : Plugin
    {
        public override Command[] Command { get; protected set; }
        private TriggerWordCollection _trcol;
        private ResponserServerSettingsCollection _rssccol;

        public ResponserPlugin(DezClient client) : base(client, "ResponserPlugin")
        {

            Command = new Command[] {new Command("nsfw", "Allows/disables NSFW mode for responsed", this, (msg, args) =>
                                        {
                                            if(!_rssccol.getByServer(msg.Server, true).nsfw)
                                            {
                                                _rssccol.getByServer(msg.Server).nsfw = true;
                                                return "NSFW ACTIVATED! ( ͡° ͜ʖ ͡°)";
                                            }
                                            else
                                            {
                                                _rssccol.getByServer(msg.Server).nsfw = false;
                                                return "NSFW disabled";
                                            }
                                        })};
            Command[0].SetRequiredRole("Porn Summoner");

            _rssccol = new ResponserServerSettingsCollection();
            _trcol = new TriggerWordCollection();

            _client.MessageReceived += async (s, e) =>
            {
                if (!e.Message.IsAuthor && e.Message.Text != "" && e.Message.Text.ToCharArray()[0] != '?')
                {
                    string ret = HandleMessage(e.Message);
                    if (ret != null)
                    {
                        await e.Channel.SendMessage(ret);
                    }
                }

            };

        }

        public override string ParseCommand(Message m, Command cmd, string[] args = null)
        {
            return cmd.Run(m, args);
        }

        public string HandleMessage(Message m)
        {
            List<string> pos = new List<string>();
            Random r = new Random();
            foreach (TriggerWord t in _trcol)
            {
                if (t.hasTrigger(m.Text))
                {
                    if (!_rssccol.getByServer(m.Server, true).nsfw)
                    {
                        if (!t.isNSFW)
                        {
                            pos.Add(t.getResponse()[r.Next(0,t.ResponseCount)]);
                        }
                    }
                    else
                    {
                        pos.Add(t.getResponse()[r.Next(0, t.ResponseCount)]);
                    }
                }
            }
            if (pos.Count != 0)
            {
                int g = r.Next(0, pos.Count);
                return pos[g];
            }
            return null;
        }

        public void AddTriggers(TriggerWord t)
        {
            _trcol.Add(t);
        }

        public bool LoadTriggersFromFile(string path, bool loadWhenCreated = true)
        {
            bool retrn = true;
            if (!File.Exists(path))
            {
                retrn = false;
                XDocument xd = new XDocument();

                XElement xtriggers = new XElement("triggers");
                XElement xtrigger1 = new XElement("trigger");
                XElement xtrigger2 = new XElement("trigger");
                XElement tw1 = new XElement("triggerword");
                XElement tw2 = new XElement("triggerword");
                XElement tw3 = new XElement("triggerword");
                XElement tw4 = new XElement("triggerword");
                tw1.Value = "trigger";
                tw2.Value = "word";
                tw3.Value = "trigger word";
                tw4.Value = "in another trigger";
                XElement rt1 = new XElement("return");
                XElement rt2 = new XElement("return");
                XElement rt3 = new XElement("return");
                rt1.Value = "Response";
                rt2.Value = "And another response!";
                rt3.Value = "Something here!";
                xtrigger1.Add(tw1);
                xtrigger1.Add(tw2);
                xtrigger1.Add(rt1);
                xtrigger1.Add(rt2);

                xtrigger2.Add(tw3);
                xtrigger2.Add(tw4);
                xtrigger2.Add(rt3);

                xtrigger1.SetAttributeValue("name", "someName");
                xtrigger1.SetAttributeValue("nsfw", "false");

                xtrigger2.SetAttributeValue("name", "someName2");
                xtrigger2.SetAttributeValue("nsfw", "true");

                xtriggers.Add(xtrigger1);
                xtriggers.Add(xtrigger2);

                xd.Add(xtriggers);

                xd.Save(path);

                if (!loadWhenCreated)
                {
                    return false;
                }
            }

            XDocument doc = XDocument.Load(path);

            var triggers = doc.Descendants("trigger");
            foreach(var t in triggers)
            {
                string name = t.Attribute("name").Value;
                bool nsfw = (t.Attribute("nsfw").Value == "true") ? true : false;
                var rets = t.Descendants("return");
                string[] returns = new string[rets.Count()];
                int ct = 0;
                foreach(var rts in rets)
                {
                    returns[ct] = rts.Value;
                    ct++;
                }
                var tws = t.Descendants("triggerword");
                string[] tw = new string[tws.Count()];
                ct = 0;
                foreach(var tww in tws)
                {
                    tw[ct] = tww.Value;
                    ct++;
                }

                this.AddTriggers(new TriggerWord(name, tw, returns, nsfw));
                
            }

            return retrn;
        }

    }
}
