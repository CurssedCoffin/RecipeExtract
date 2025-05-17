using Terraria.ModLoader;
using RecipeExtract.Common.Services;

namespace RecipeExtract
{
    /// <summary>
    /// Main mod class for RecipeExtract, responsible for extracting and exporting game data
    /// </summary>
    public class RecipeExtract : Mod
    {
        private DataExportService _dataExportService;

        public override void Load()
        {
            _dataExportService = new DataExportService(this);
        }

        /// <summary>
        /// Called after all mod content is loaded
        /// </summary>
        public override void PostSetupContent()
        {
            base.PostSetupContent();
            // Do nothing when the mod is not loaded
        }

        /// <summary>
        /// Exports all game data to JSON files
        /// </summary>
        public void ExportData()
        {
            _dataExportService.ExportData();
        }
    }
}