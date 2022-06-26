using System;

namespace SNAP.API.Logging
{
    public interface IConsoleMonitor
    {
        void ConsoleWrite(string msg, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, bool resetColor = false);
        
        void ConsoleWriteLine(string msg, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, bool resetColor = false);
        
        void MarkupWrite(string msg);
        
        void MarkupWriteLine(string msg);
    }
}