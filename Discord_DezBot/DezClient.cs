using System;

using Discord;
using Discord.Logging;

namespace Discord_DezBot
{
    /// <summary>
    /// DezClient implemets Discord.DiscordClient to provide basic connection with discord server
    /// </summary>
    partial class DezClient : DiscordClient
    {

        private PluginManager _pmgr;
        
        internal Logger Logger { get; }

        public DezClient() : base()
        {
            _pmgr = new PluginManager(this);
        }

        public DezClient(DiscordConfig config) : base(config)
        {
            _pmgr = new PluginManager(this);
        }

        public DezClient(DiscordConfigBuilder builder) : base(builder)
        {
            _pmgr = new PluginManager(this);
        }

        public PluginManager PluginManager
        {
            get
            {
                return _pmgr;
            }
        }

    }
}
