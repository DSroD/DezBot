using Discord;

namespace Discord_DezBot
{
    abstract class Plugin
    {

        internal DezClient _client;

        public string Name { get; protected set; }

        public abstract Command[] Command { get; protected set; }

        public Plugin(DezClient client, string Name)
        {
            this._client = client;
            this.Name = Name;
        }

        public abstract string ParseCommand(Message m, Command cmd, string[] args = null);

    }
}
