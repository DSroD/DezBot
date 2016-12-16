using System;
using System.IO;
using System.Net;

using Discord;
using Discord.Audio;
using System.Threading.Tasks;

using NAudio.Wave;
using System.Collections.Generic;

namespace Discord_DezBot.AudioPlugin
{
    class AudioProvider
    {
        Random r;
        public Channel channel { get; private set; }
        public string song { get; private set; }
        private List<string> queue;
        private DezClient _client;
        private IAudioClient _vclient;

        private bool _playing;
        private bool _skipThis = false;

        private static string _musicDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music");


        public AudioProvider(Channel ch, DezClient c)
        {
            r = new Random();
            this._client = c;
            this.channel = ch;
            _playing = false;
            queue = new List<string>();
        }

        public string Parse(string[] args)
        {
            if(args.Length == 0)
            {
                return "Usage:\r\n`?music play [song] - Adds song to queue` `?music list - Lists all the availible songs` `?music skip - Skips current song` `?music stop - Stops playin'` `?music all (shuffle) - plays everythin!`";
            }
            string ret;
            switch (args[0].ToLower())
            {
                case "play":
                    if(args.Length < 2)
                    {
                        return "Usage:\r\n`?music play[song] -Adds song to queue`";
                    }
                    string songName = "";
                    for(int i = 1; i < args.Length; i++)
                    {
                        songName += args[i];
                        if(i < args.Length - 1)
                        {
                            songName += " ";
                        }
                    }
                    if(!_playing)
                    {
                        ret = Play(songName);
                    }
                    else
                    {
                        queue.Add(songName);
                        ret = songName + " added to Q u fuk!";
                    }

                    
                    return ret;

                case "list":
                    int pg;
                    if(args.Length < 2)
                    {
                        pg = 1;
                    }
                    else
                    {
                        if(!int.TryParse(args[1], out pg))
                        {
                            return "Usage: ?music list (page)";
                        }
                    }
                    ret = "`Availible songs";

                    string[] asongs = Directory.GetFiles(_musicDir, "*.mp3");

                    float zzzz = asongs.Length / 20;

                    int pgnum = (int)Math.Ceiling(zzzz) + 1;

                    ret += "(page " + pg + "/" + pgnum + ")`: ";

                    for (int i = 0+(pg-1)*20; i< 20+(pg-1)*20; i++)
                    {
                            if(i >= asongs.Length)
                            {
                                break;
                            }
                        if (asongs[i].Contains(".mp3"))
                        {
                            ret += asongs[i].Replace(_musicDir + "\\", "").Replace(".mp3", "") + "; ";
                        }
                    }
                    return ret;

                case "skip":

                    return "No functionality yet!";

                case "stop":

                    return "No functionality yet!";

                case "queue":
                    ret = "Queued songs: ";
                    foreach(string sng in queue)
                    {
                        ret += sng + "; ";
                    }
                    return ret;

                case "all":
                    bool shuffle = false;
                    if(args.Length > 1)
                    {
                        if (args[1] == "true" || args[1] == "shuffle")
                        {
                            shuffle = true;
                        }
                    }

                    List<string> zeList = new List<string>();

                    foreach(string s in Directory.GetFiles(_musicDir, "*.mp3"))
                    {
                        if (queue.Count == 0)
                        {
                            queue.Add(s.Replace(_musicDir + "\\", "").Replace(".mp3", ""));
                        }
                        else
                        {
                            if (shuffle)
                            {
                                queue.Insert(r.Next(0, zeList.Count - 1), s.Replace(_musicDir + "\\", "").Replace(".mp3", ""));
                            }
                            else
                            {
                                queue.Add(s.Replace(_musicDir + "\\", "").Replace(".mp3", ""));
                            }
                        }
                        
                    }
                    if (!_playing)
                    {
                        Play(queue[0]);
                        queue.RemoveAt(0);
                    }

                    return "Playin' all ze shiet!";

                default:
                    return "Wrong Command!\r\nUsage:\r\n`?music play [song] - Adds song to queue` `?music list - Lists all the availible songs` `?music skip - Skips current song` `?music stop - Stops playin'`";

            }
        }

        private string Play(string song)
        {
            if (!File.Exists(@_musicDir+"\\"+@song+@".mp3"))
            {
                return song + " not found!";
            }
            else
            {
                SendSound(_musicDir + "\\" + song + @".mp3");
                this.song = song;
                return "Now playing: " + song;
            }

        }

        private Task<string> GetNextSong()
        {
            _skipThis = false;
            return Task.Run(async () =>
            {
                if(queue.Count == 0)
                {
                    _playing = false;
                    await _vclient.Disconnect();
                    return null;
                }
                else
                {
                    string nsong = queue[0];
                    queue.RemoveAt(0);
                    return Play(nsong);
                }
            });
        }

        private async Task SendSound(string song)
        {
            if (!_playing)
            {
                _vclient = await _client.GetService<AudioService>().Join(channel);
                _playing = true;
            }
            _skipThis = false;
            await Task.Run(() =>
            {
               int channelCount = _client.GetService<AudioService>().Config.Channels;
               WaveFormat outFormat = new WaveFormat(48000, 16, channelCount);
               AudioFileReader r = new AudioFileReader(song);
               using (MediaFoundationResampler resampler = new MediaFoundationResampler(r, outFormat))
               {
                   resampler.ResamplerQuality = 60;
                   int blockSize = outFormat.AverageBytesPerSecond / 50;
                   byte[] buffer = new byte[blockSize];
                   int byteCount;

                   while ((byteCount = resampler.Read(buffer, 0, blockSize)) > 0 && _playing == true)
                   {

                       if (byteCount < blockSize)
                       {
                            // Incomplete Frame
                            for (int i = byteCount; i < blockSize; i++)
                           {
                               buffer[i] = 0;
                               if (_skipThis)
                               {
                                   _vclient.Clear();
                                   break;
                               }
                                
                            }
                       }
                       _vclient.Send(buffer, 0, blockSize);
                       _vclient.Wait();
                    }

                   GetNextSong();
               }
            });
        } 



    }
}
