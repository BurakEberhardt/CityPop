using System.Threading.Tasks;
using CityPop.Map.Views;
using CityPop.World.Data;
using Zen.Core.Addressables;
using Zen.Core.Extensions;
using Zen.Core.View;

namespace CityPop.Map
{
    public class MapService
    {
        public MapView CurrentMap { get; private set; }

        public MapView LoadMap(MapId mapId)
        {
            using (Addressables.LoadComponent<MapView>($"Maps/Map/{mapId}", out var prefab))
            {
                CurrentMap = prefab.GetViewFromObjectPool();
                return CurrentMap;
            }
        }

        public async Task<MapView> LoadMapAsync(MapId mapId)
        {
            using ((await Addressables.LoadComponentAsync($"Maps/Map/{mapId}")).TakeComponentResult<MapView>(out var prefab))
            {
                CurrentMap = prefab.GetViewFromObjectPool();
                return CurrentMap;
            }
        }
    }
}