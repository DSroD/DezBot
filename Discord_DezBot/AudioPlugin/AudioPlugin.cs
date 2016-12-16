using System;
using Discord;
using Discord.Audio;

namespace Discord_DezBot.AudioPlugin
{
    class AudioPlugin : Plugin
    {
        override public Command[] Command { get; protected set; }

        AudioProviderCollection apcol;

        public AudioPlugin(DezClient client) : base(client, "AudioClient")
        {
            Command = new Command[] { new Command("music", "Plays music", this, (m, args) =>
                    {
                        if(m.User.VoiceChannel == null)
                        {
                            return "You have to be in a voice channel to use music commands... dumbo...";
                        }
                        AudioProvider dodis = apcol.GetByChannel(m.User.VoiceChannel, true, _client);                     
                        return  dodis.Parse(args);

                   })};
            Command[0].SetRequiredRole("DJ");
            apcol = new AudioProviderCollection();

            _client.UsingAudio(x =>
           {
               x.Mode = AudioMode.Outgoing;
           });

        }

        override public string ParseCommand(Message m, Command cmd, string[] args)
        {
            return cmd.Run(m, args);
        }
    }
}
