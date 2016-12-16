using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_DezBot
{
    class TriggerWord
    {
        public string Name { get; private set; }
        private string[] trigger;
        private string[] returns;
        private bool nsfw;

        public TriggerWord(string name, string[] trigger, string[] returns, bool nsfw)
        {
            this.Name = name;
            this.trigger = trigger;
            this.returns = returns;
            this.nsfw = nsfw;
        }

        public bool hasTrigger(string message)
        {
            foreach (string s in trigger)
            {
                if (message.ToLower().Contains(s.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public string[] getResponse()
        {
            return returns;
        }

        public bool isNSFW
        {
            get
            {
                return nsfw;
            }
        }

        public int ResponseCount
        {
            get
            {
                return returns.Length;
            }
        }

    }
}
