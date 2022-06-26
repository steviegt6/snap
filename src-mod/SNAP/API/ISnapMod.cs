using SNAP.API.Logging;
using StardewModdingAPI;

namespace SNAP.API
{
    public interface ISnapMod : IMod
    {
        ISnapMonitor SnapMonitor { get; }
    }
}