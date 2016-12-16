using System;
using System.Collections;

namespace Discord_DezBot
{
    class TriggerWordCollection : IEnumerable
    {
        private TriggerWord[] _triggers;

        public TriggerWordCollection(TriggerWord[] triggers)
        {
            _triggers = new TriggerWord[triggers.Length];

            for (int i = 0; i < triggers.Length; i++)
            {
                _triggers[i] = triggers[i];
            }
        }

        public TriggerWordCollection()
        {
            _triggers = new TriggerWord[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public TriggerEnum GetEnumerator()
        {
            return new TriggerEnum(_triggers);
        }

        public void Add(TriggerWord trg)
        {
            TriggerWord[] tmplt = new TriggerWord[_triggers.Length + 1];
            for (int i = 0; i < _triggers.Length; i++)
            {
                tmplt[i] = _triggers[i];
            }
            tmplt[tmplt.Length - 1] = trg;

            _triggers = tmplt;
        }
    }



    class TriggerEnum : IEnumerator
    {

        public TriggerWord[] triggers;
        int position = -1;

        public TriggerEnum(TriggerWord[] list)
        {
            triggers = list;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public TriggerWord Current
        {
            get
            {
                try
                {
                    return triggers[position];
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
            return (position < triggers.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
