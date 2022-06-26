using SNAP.API;
using SNAP.API.Logging;
using SNAP.Logging;
using StardewModdingAPI;

namespace SNAP
{
    public class SnapMod : Mod, ISnapMod
    {
        #region ISnapMod Impl

        public ISnapMonitor SnapMonitor { get; set; } = null!;

        #endregion
        
        public override void Entry(IModHelper helper) {
            SnapMonitor = new SnapMonitor(Monitor);
            DoEntrypointLogging();
        }

        protected virtual void DoEntrypointLogging() {
            SnapConsole.WriteStartMessage(ModManifest.Version, SnapMonitor);
        }
    }
}