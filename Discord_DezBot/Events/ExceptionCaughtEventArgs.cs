using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_DezBot
{
    class ExceptionCaughtEventArgs : EventArgs
    {
        public Exception e { get; }

        public ExceptionCaughtEventArgs(Exception e)
        {
            this.e = e;
        }

    }
}
