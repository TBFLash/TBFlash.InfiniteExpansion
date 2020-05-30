using SimAirport.Modding.Base;
using SimAirport.Modding.Settings;
using SimAirport.Modding.Data;
using SimAirport.Logging;

namespace TBFlash.InfiniteExpansion
{
    public class Mod : BaseMod
    {
        public override string Name => "Infinite Expansion";

        public override string InternalName => "TBFlash.InfiniteExpansion";

        public override string Description => "Infinite Expansion... Under New Management";

        public override string Author => "TBFlash";
        private const bool isTBFlashDebug = true;
        private const int OriginalMaxTileCount = 141352;
        private int MaxTileCount = OriginalMaxTileCount;
        private bool SafeMode = true;
        private bool RestrictScenario = true;
        private bool IsDisabled = false;
        private SimAirport.Modding.Data.GameState ModGameState;

        public override SettingManager SettingManager { get; set; }

        public override void OnLoad(SimAirport.Modding.Data.GameState state)
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            ModGameState = state;
            IsDisabled = false;
            SetMaxTileCount();
            TBFlashLogger(Log.FromPool($"{MaxTileCount}").WithCodepoint());
        }

        public override void OnDisabled()
        {
            IsDisabled = true;
            SetMaxTileCount();
            TBFlashLogger(Log.FromPool("").WithCodepoint());
        }

        public override void OnSettingsLoaded()
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
        }

        private void SetMaxTileCount()
        {
            if ((int)ModGameState > 2 && (ModGameState != SimAirport.Modding.Data.GameState.Scenario || (ModGameState == SimAirport.Modding.Data.GameState.Scenario && !RestrictScenario)))
            {
                MaxTileCount = IsDisabled ? OriginalMaxTileCount : SafeMode ? 429496729 : int.MaxValue;
                Map.Instance.SetMaxMapCellCount(MaxTileCount);
                //Game.current.Map().resizer.CheckIfEnabled();
                TBFlashLogger(Log.FromPool($"{MaxTileCount}/{GameMapResizer.MAX_TILE_COUNT}").WithCodepoint());
            }
        }

        public override void OnTick()
        {
        }

        internal static void TBFlashLogger(Log log)
        {
            if (isTBFlashDebug)
            {
                Game.Logger.Write(log);
            }
        }
    }
}
