using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_DezBot
{
    class DebugModeChangedEventArgs : EventArgs
    {
        public bool DebugModeState;

        public DebugModeChangedEventArgs(bool DebugModeState)
        {
            this.DebugModeState = DebugModeState;
        }
    }
}
