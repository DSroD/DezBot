using System;
using System.Collections;


namespace Discord_DezBot
{
    //PLUGIN COLLECTION - ENUMERABLE
    class PluginCollection : IEnumerable
    {
        private Plugin[] _plugins;
        
        public PluginCollection(Plugin[] pluginArray)
        {
            _plugins = new Plugin[pluginArray.Length];

            for(int i = 0; i < pluginArray.Length; i++)
            {
                _plugins[i] = pluginArray[i];
            }
        }

        public PluginCollection()
        {
            _plugins = new Plugin[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PluginEnum GetEnumerator()
        {
            return new PluginEnum(_plugins);
        }

        public void Add(Plugin plg)
        {
            Plugin[] tmplt = new Plugin[_plugins.Length + 1];
            for(int i = 0; i < _plugins.Length; i++)
            {
                tmplt[i] = _plugins[i];
            }
            tmplt[tmplt.Length - 1] = plg;

            _plugins = tmplt;
        }

        public Plugin GetByName(string name)
        {
            for (int i = 0; i < _plugins.Length; i++)
            {
                if (_plugins[i].Name == name)
                {
                    return _plugins[i];
                }
            }

            return null;
        }

    }


    //PLUGIN ENUMERATOR
    class PluginEnum : IEnumerator
    {
        public Plugin[] plugins;

        int position = -1;

        public PluginEnum(Plugin[] list)
        {
            plugins = list;
        }

        public bool MoveNext()
        {
            position++;

            return (position < plugins.Length);
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

        public Plugin Current
        {
            get
            {
                try
                {
                    return plugins[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }

        }


    }

}
