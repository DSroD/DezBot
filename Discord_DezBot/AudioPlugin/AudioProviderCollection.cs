using System.Collections;
using System;
using Discord;

namespace Discord_DezBot.AudioPlugin
{
    class AudioProviderCollection : IEnumerable
    {

        private AudioProvider[] _providers;

        public AudioProviderCollection(AudioProvider[] provider)
        {
            _providers = new AudioProvider[provider.Length];

            for(int i = 0; i < provider.Length; i++)
            {
                _providers[i] = provider[i];
            }
        }

        public AudioProviderCollection()
        {
            _providers = new AudioProvider[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public AudioProviderEnum GetEnumerator()
        {
            return new AudioProviderEnum(_providers);
        }

        public void Add(AudioProvider provider)
        {
            AudioProvider[] tmplt = new AudioProvider[_providers.Length + 1];
            for (int i = 0; i < _providers.Length; i++)
            {
                tmplt[i] = _providers[i];
            }
            tmplt[tmplt.Length - 1] = provider;

            _providers = tmplt;
        }

        public AudioProvider GetByChannel(Channel vc, bool generateNew = false, DezClient client = null)
        {
            for(int i = 0; i < _providers.Length; i++)
            {
                if(_providers[i].channel == vc)
                {
                    return _providers[i];
                }
            }
            if (generateNew)
            {
                if(client == null)
                {
                    throw new ArgumentNullException();
                }
                AudioProvider pr = new AudioProvider(vc, client);
                this.Add(pr);
                return pr;
            } else
            {
                return null;
            }
        }

    }



    class AudioProviderEnum : IEnumerator
    {

        public AudioProvider[] providers;

        int position = -1;

        public AudioProviderEnum(AudioProvider[] list)
        {
            providers = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < providers.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public AudioProvider Current
        {
            get
            {
                try
                {
                    return providers[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
