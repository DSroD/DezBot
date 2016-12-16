using System;
using Discord;

namespace Discord_DezBot
{
    class ResponserServerSettings
    {
        public Server server { get; private set; }
        public bool nsfw { get; set; }

        public ResponserServerSettings(Server server, bool nsfw)
        {
            this.server = server;
            this.nsfw = nsfw;
        }
    }
}
