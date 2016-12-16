using System;
using System.Collections;
using Discord;

namespace Discord_DezBot
{
    class ResponserServerSettingsCollection : IEnumerable
    {
        private ResponserServerSettings[] _rspsrvst;

        public ResponserServerSettingsCollection(ResponserServerSettings[] rspsrvsttngs)
        {
            _rspsrvst = new ResponserServerSettings[rspsrvsttngs.Length];

            for(int i = 0; i < rspsrvsttngs.Length; i++)
            {
                _rspsrvst[i] = rspsrvsttngs[i];
            }
        }

        public ResponserServerSettingsCollection()
        {
            _rspsrvst = new ResponserServerSettings[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ResponserServerSettingsEnum GetEnumerator()
        {
            return new ResponserServerSettingsEnum(_rspsrvst);
        }

        public void Add(ResponserServerSettings settings)
        {
            ResponserServerSettings[] temp = new ResponserServerSettings[_rspsrvst.Length + 1];
            for(int i = 0; i < _rspsrvst.Length; i++)
            {
                temp[i] = _rspsrvst[i];
            }
            temp[temp.Length - 1] = settings;

            _rspsrvst = temp;
        }

        public ResponserServerSettings getByServer(Server s, bool rtNew = false)
        {
            foreach(ResponserServerSettings stt in _rspsrvst)
            {
                if(stt.server == s)
                {
                    return stt;
                }
            }
            if(rtNew)
            {
                ResponserServerSettings r = new ResponserServerSettings(s, false);
                this.Add(r);
                return r;
            }
            else
            {
                return null;
            }
        }
    }


    class ResponserServerSettingsEnum : IEnumerator
    {
        ResponserServerSettings[] _rspsrvstt;
        int position = -1;

        public ResponserServerSettingsEnum(ResponserServerSettings[] list)
        {
            _rspsrvstt = list;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public ResponserServerSettings Current
        {
            get
            {
                try
                {
                    return _rspsrvstt[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < _rspsrvstt.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
