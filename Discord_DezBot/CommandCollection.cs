using System;
using System.Collections;

namespace Discord_DezBot
{
    class CommandCollection : IEnumerable
    {
        private Command[] _commands;

        public CommandCollection(Command[] commands)
        {
            _commands = new Command[commands.Length];

            for (int i = 0; i < _commands.Length; i++)
            {
                _commands[i] = commands[i];
            }
        }

        public CommandCollection()
        {
            _commands = new Command[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public CommandEnum GetEnumerator()
        {
            return new CommandEnum(_commands);
        }

        public void Add(Command cmd)
        {
            Command[] tmplt = new Command[_commands.Length + 1];
            for (int i = 0; i < _commands.Length; i++)
            {
                tmplt[i] = _commands[i];
            }
            tmplt[tmplt.Length - 1] = cmd;

            _commands = tmplt;
        }

        public void Remove(int index)
        {
            Command[] tmplt = new Command[_commands.Length - 1];
            _commands[index] = null;
            int y = 0;
            for (int i = 0; i < _commands.Length; i++)
            {
                if (_commands[i] != null)
                {
                    tmplt[y] = _commands[i];
                    y++;
                }
            }

            _commands = tmplt;
        }

        public void Remove(string name)
        {
            int index;
            if(TryGetIndexByName(name,out index))
            {
                Remove(index);
            }
        }

        public Command GetByName(string name)
        {
            for(int i = 0; i < _commands.Length; i++)
            {
                if(_commands[i].Name == name)
                {
                    return _commands[i];
                }
            }
            return null;
        }

        public bool TryGetIndexByName(string name, out int index)
        {
            for(int i = 0; i < _commands.Length; i++)
            {
                if(_commands[i].Name == name)
                {
                    index = i;
                    return true;
                }
            }
            index = 0;
            return false;

        }

    }



    class CommandEnum : IEnumerator
    {
        public Command[] commands;

        int position = -1;

        public CommandEnum(Command[] list)
        {
            commands = list;
        }

        public bool MoveNext()
        {
            position++;

            return (position < commands.Length);
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

        public Command Current
        {
            get
            {
                try
                {
                    return commands[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }

        }

    }
}
