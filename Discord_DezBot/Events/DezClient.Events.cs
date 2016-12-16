using System;
using System.Runtime.CompilerServices;

namespace Discord_DezBot
{
    partial class DezClient
    {
        public event EventHandler<ExceptionCaughtEventArgs> ExceptionCaught = delegate { };

        public event EventHandler<DebugModeChangedEventArgs> DebugModeChanged = delegate { };



        internal void OnExceptionCaught(Exception e) => OnEvent(ExceptionCaught, new ExceptionCaughtEventArgs(e));

        internal void OnDebugModeChanged(bool DebugModeState) => OnEvent(DebugModeChanged, new DebugModeChangedEventArgs(DebugModeState));




        private void OnEvent<T>(EventHandler<T> handler, T eventArgs, [CallerMemberName] string callerName = null)
        {
            try { handler(this, eventArgs); }
            catch (Exception ex)
            {
                Logger.Error($"{callerName.Substring(2)}'s handler encountered an error", ex);
            }
        }

        private void OnEvent(EventHandler handler, [CallerMemberName] string callerName = null)
        {
            try { handler(this, EventArgs.Empty); }
            catch (Exception ex)
            {
                Logger.Error($"{callerName.Substring(2)}'s handler encountered an error", ex);
            }
        }
    }
}
