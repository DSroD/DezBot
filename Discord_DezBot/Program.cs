using System;
using Discord_DezBot.ConsoleWorks;
namespace Discord_DezBot
{
    class Program
    {
        public static bool islowendian = BitConverter.IsLittleEndian;

        static void Main(string[] args) => new Program().Start(args);

        public DezClient _client { get; private set; }

        private string conneciton_token = "MjA0NTE3Mzc0OTkxNDAwOTYx.CzMNwA.i3dnvDlGpAdZE8sev9T5BKrGUgQ";

        public void Start(string[] args)
        {

            _client = new DezClient();

            ColoredConsole.WriteLine("Creating event handlers!", fgcolor: ConsoleColor.Green);

            _client.MessageReceived += async (s, e) =>
            {
                if (!e.Message.IsAuthor && e.Message.Text != "" && e.Message.Text.ToCharArray()[0] == '?' && e.Message.Text != "?")
                {
                    string rt = _client.PluginManager.SendCommand(e.Message);
                    if (rt != null && rt != "")
                    {
                        await e.Channel.SendMessage(rt);
                    }
                }
            };

            ColoredConsole.WriteLine("Loading plugins!", fgcolor: ConsoleColor.Green);
            _client.PluginManager.AddPlugin(new BasicFunctionsPlugin(_client)); //BASIC FUNCTIONS PLUGIN
            _client.PluginManager.AddPlugin(new AudioPlugin.AudioPlugin(_client)); //AUDIO PLUGIN
            _client.PluginManager.AddPlugin(new UrbanDictionaryPlugin.UrbanDictionaryPlugin(_client)); //UD PLUGIN
            _client.PluginManager.AddPlugin(new ResponserPlugin(_client)); //RESPONSER PLUGIN
            _client.PluginManager.AddPlugin(new DebuggerPlugin.DebuggerPlugin(_client));

            ColoredConsole.WriteLine("Loading Reponse Triggers", fgcolor: ConsoleColor.Green);

            string fld = (_client.PluginManager.GetPluginByClass<ResponserPlugin>().LoadTriggersFromFile("Resources/Triggers.xml")) ?  "File Loaded" : "Created template file!";
            ColoredConsole.WriteLine(fld, fgcolor: ConsoleColor.Green);
            ColoredConsole.WriteLine("Connecting!", fgcolor: ConsoleColor.Green);

            _client.Ready += (s, e) =>
            {
                _client.SetGame("http://dezrodino.mzf.cz/");
            };

            _client.DebugModeChanged += (s, e) =>
            {
                string state = (e.DebugModeState) ? "activated" : "deactivated";
                ColoredConsole.WriteLine("Debug mode " + state, fgcolor: ConsoleColor.Yellow);
            };

            _client.ExceptionCaught += (s, e) =>
            {
                ColoredConsole.WriteLine(e.e.ToString(), fgcolor: ConsoleColor.Red);
            };

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect(conneciton_token, Discord.TokenType.Bot);
            });

        }
    }
}
