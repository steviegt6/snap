using StardewModdingAPI;

namespace SNAP.API.Logging
{
    public interface ILogFileMonitor
    {
        void FileWriteLine(string message, LogLevel level = LogLevel.Trace);
        
        void FileWriteLineOnce(string message, LogLevel level = LogLevel.Trace);

        void FileWriteLineVerbose(string message);
    }
}