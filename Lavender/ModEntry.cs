using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace Lavender
{
    public class ModEntry : Mod, IAssetLoader
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs args)
        {
            // get the internal asset key for the map file
            string mapAssetKey = this.Helper.Content.GetActualAssetKey("assets/Lavender.tmx", ContentSource.ModFolder);
            string roomAssetKey = this.Helper.Content.GetActualAssetKey("assets/LavenderHouse.tmx", ContentSource.ModFolder);

            // add the location
            GameLocation locationOutdoor = new GameLocation(mapAssetKey, "Lavender") { IsOutdoors = true, IsFarm = false };
            GameLocation locationIndoor = new GameLocation(roomAssetKey, "LavenderHouse") { IsOutdoors = false, IsFarm = false };

            Game1.locations.Add(locationOutdoor);
            Game1.locations.Add(locationIndoor);
        }

        /// <summary>Get whether this instance can load the initial version of the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanLoad<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Maps/Town");
        }

        /// <summary>Load a matched asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public T Load<T>(IAssetInfo asset)
        {
            return this.Helper.Content.Load<T>("assets/Town.tmx");
        }
    }
}