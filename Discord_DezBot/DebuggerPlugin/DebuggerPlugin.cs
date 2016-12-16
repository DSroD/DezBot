using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Discord_DezBot.DebuggerPlugin
{
    class DebuggerPlugin : Plugin
    {
        public override Command[] Command { get; protected set; }

        private bool debug;

        public DebuggerPlugin(DezClient client) : base(client, "DebuggerPlugin")
        {
            debug = false;

            Command = new Command[] {
                            new Command("debug", "Enters Debug mode", this, (m, args) =>
                                        {
                                            if(args.Length == 0)
                                            {
                                                debug = !debug;
                                                if(debug)
                                                {
                                                    DebugModeOn();
                                                }
                                                else
                                                {
                                                    DebugModeOff();
                                                }
                                            }
                                            else
                                            {
                                                switch(args[0])
                                                {
                                                    case "true":
                                                        debug = true;
                                                        DebugModeOn();
                                                        break;
                                                    case "1":
                                                        debug = true;
                                                        DebugModeOn();
                                                        break;
                                                    case "false":
                                                        debug = false;
                                                        DebugModeOff();
                                                        break;
                                                    case "0":
                                                        debug = false;
                                                        DebugModeOff();
                                                        break;
                                                    default:
                                                        return "Usage: ?debug {true, 1, false, 0}";

                                                }
                                            }

                                            _client.OnDebugModeChanged(debug);
                                            return (debug) ? "Debug Mode enabled" : "Debug Mode disabled";


                                        })
            };
        }

        public override string ParseCommand(Message m, Command cmd, string[] args = null)
        {
            return cmd.Run(m, args);
        }

        private void DebugModeOn()
        {
            _client.PluginManager.RegisterCommand(new Command("throwexception", "Throws Exception", this, (m, args) =>
                            {
                                if (!debug)
                                {
                                    return "Requires debug mode!";
                                }
                                else
                                {
                                    if (args.Length == 0)
                                    {
                                        throw new Exception();
                                    }
                                    else
                                    {
                                        switch (args[0])
                                        {
                                            case "NotImplementedException":
                                                throw new NotImplementedException();

                                            case "IndexOutOfRangeException":
                                                throw new IndexOutOfRangeException();

                                            case "Exception":
                                                throw new Exception();

                                            default:
                                                return "Availible exceptions: NotImplementedException, IndexOutOfRangeException";
                                        }
                                    }
                                }
                            }));
        }

        private void DebugModeOff()
        {
            _client.PluginManager.UnregisterCommand("throwexception");
        }
    }
}
