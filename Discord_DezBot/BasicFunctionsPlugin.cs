using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Discord_DezBot
{
    class BasicFunctionsPlugin : Plugin
    {
        public override Command[] Command { get; protected set; }

        public BasicFunctionsPlugin(DezClient client) : base(client, "BasicFunctionsPlugin")
        {
            Command = new Command[] { new Command("help", "List all availible commands", this, (m, args) =>
                                        {
                                            string helpmsg = "Availible commands:\n";
                                            foreach (Command cmd in _client.PluginManager.RegisteredCommands)
                                            {
                                                helpmsg += "**`?" + cmd.Name + " - " + cmd.Description + "`**  ";
                                            }
                                            return helpmsg;

                                        }),
                                      new Command("insult", "Insults user", this, (m, args) =>
                                        {
                                            string user;
                                            string sender = m.User.Name;

                                            if(args.Length == 0)
                                            {
                                                user = sender;
                                            } else
                                            {
                                                user = args[0];
                                            }

                                            if(user.ToLower().Contains("dezbot"))
                                            {
                                                return "Can't insult myself you dumb fuck!";
                                            }

                                            if(user.ToLower().Contains("dezrodino"))
                                            {
                                                return "Who'd want to insult such an awesome person!?";
                                            }



                                            return Insulter.Insult(user, sender);

                                        }),
                                        new Command("purge", "Clears bot's messages", this, (m, args) =>
                                        {
                                            if(args.Length == 0)
                                            {
                                                return "Usage: ?purge [ammount]";
                                            }
                                            int amm;
                                            if(!int.TryParse(args[0],out amm))
                                            {
                                                return "Ammount has to be integer!";
                                            }

                                            int rtr = 1;
                                            int fnds = 0;
                                            var msgs = m.Channel.Messages;
                                            int ct = msgs.Count();
                                            if(ct < amm)
                                            {
                                                amm = ct;
                                            }
                                            while ((fnds < amm && rtr < ct))
                                            {
                                                if(msgs.ElementAt(ct-rtr).IsAuthor)
                                                {
                                                   msgs.ElementAt(ct-rtr).Delete();
                                                   fnds++;
                                                }
                                                rtr++;
                                            }
                                            return null;
                                        }),
                                        new Command("clear", "Clears messages", this, (m, args) =>
                                        {
                                            if(args.Length == 0)
                                            {
                                                return "Usage: ?clear [ammount] (user)...";
                                            }

                                            int amm;
                                            if(!int.TryParse(args[0], out amm))
                                            {
                                                return "Ammount has to be integer!";
                                            }

                                            amm++;


                                            var tsk = m.Channel.DownloadMessages();
                                            tsk.Wait();
                                            var msgs = tsk.Result;

                                            int rtr = 0;
                                            int fnds = 0;
                                            int ct = msgs.Length;

                                            if(ct < amm)
                                            {
                                                amm = ct;
                                            }
                                            string user = null;
                                            if(args.Length >= 2)
                                            {
                                                user = args[1];
                                            }
                                            while ((fnds < amm && rtr < ct-1))
                                            {
                                                 if(user != null)
                                                {
                                                    if(user == msgs.ElementAt(rtr).User.Name)
                                                    {
                                                        msgs.ElementAt(rtr).Delete();
                                                        fnds++;
                                                    }
                                                }
                                                 else
                                                {
                                                   msgs.ElementAt(rtr).Delete();
                                                    fnds++;
                                                }
                                                 rtr++;
                                            }
                                            return null;
                                        }
                                        )};
            Command[2].SetRequiredRole("Bot Slaver");
            Command[3].SetRequiredRole("Bot Slaver");
        }

        public override string ParseCommand(Message m, Command cmd, string[] args = null)
        {
            return cmd.Run(m, args);
        }
    }
}
