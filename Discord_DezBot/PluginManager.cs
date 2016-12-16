using System;
using System.Text.RegularExpressions;

using Discord;

namespace Discord_DezBot
{
    /// <summary>
    /// PluginManager implements an easy way to add modifications to DezBot functionality
    /// </summary>
    class PluginManager
    {
        private DezClient _client;
        private PluginCollection _plcol;
        public CommandCollection RegisteredCommands;

        public PluginManager(DezClient _client)
        {
            this._client = _client;
            _plcol = new PluginCollection();
            RegisteredCommands = new CommandCollection();
        }

        /// <summary>
        /// Selects plugin based on message and sends command to it for parsing.
        /// </summary>
        /// <param name="m">Message to parse</param>
        /// <returns>Message to send to server</returns>
        public string SendCommand(Message m)
        {
            try
            {
                MatchCollection mtchc = Regex.Matches(m.Text, @"\S+");

                string[] cmd = new string[mtchc.Count];

                int i = 0;
                foreach(Match mtch in mtchc)
                {
                    cmd[i] = mtch.Value;
                    i++;
                }

                string[] args = new string[cmd.Length - 1];
                for(int k = 0; k < args.Length; k++)
                {
                    args[k] = cmd[k + 1];
                }

                Command comd = RegisteredCommands.GetByName(cmd[0].Replace("?", "").ToLower());
                if (comd != null)
                {
                    return comd.Plugin.ParseCommand(m, comd, args);
                }

                return "Invalid command, please check ?help";

            }
            catch(Exception e)
            {
                _client.OnExceptionCaught(e);
                return "Something went wrong, please let server owner or my almighty creator Dezrodino know about this bug!";
            }
        }

        public void AddPlugin(Plugin pl)
        {
            _plcol.Add(pl);
            for(int i = 0; i < pl.Command.Length; i++)
            {
                RegisteredCommands.Add(pl.Command[i]);
            }
        }

        public void RegisterCommand(Command c)
        {
            RegisteredCommands.Add(c);
        }

        public void UnregisterCommand(string name)
        {
            RegisteredCommands.Remove(name);
        }

        public Plugin GetPluginByName(string name)
        {
            return _plcol.GetByName(name);
        }

        public T GetPluginByClass<T>() where T : Plugin
        {
            foreach(Plugin pl in _plcol)
            {
                if(pl is T)
                {
                    return (T)pl;
                }
            }
            return null;
        }

    }
}
