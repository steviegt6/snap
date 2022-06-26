using StardewModdingAPI;

namespace SNAP.API.Logging
{
    public interface ISnapMonitor : IMonitor, IConsoleMonitor, ILogFileMonitor
    {
    }
}