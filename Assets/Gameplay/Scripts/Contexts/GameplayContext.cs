using System.Threading.Tasks;
using Character.Scripts.Data;
using CityPop.Character;
using CityPop.Gameplay.Views;
using CityPop.MainMenu.Contexts;
using CityPop.Map.Views;
using CityPop.Player.Data;
using CityPop.World.Data;
using Core.Context;
using Unity.Netcode;
using UnityEngine;
using Zen.Core.Addressables;
using Zen.Core.View;

namespace CityPop.Gameplay.Contexts
{
    public class GameplayContext : View
    {
        public static async Task Initialize(WorldData worldData, PlayerData playerData)
        {
            var context = await Context.LoadAsync<GameplayContext>("Gameplay");

            LoadMap(worldData.SpawnPosition.MapId);
            SpawnPlayer(playerData, worldData.SpawnPosition.Position);
            OpenGameplayUi();
        }

        static void OpenGameplayUi()
        {
            using (Addressables.LoadComponent<GameplayUi>("Ui/Gameplay", out var prefab))
            {
                var view = prefab.GetViewFromObjectPool();

                view.EventOnClose += OnClose;

                void OnClose()
                {
                    CloseGameplayUi();
                    CloseSession();
                    OpenMainMenuScene();
                }

                async void OpenMainMenuScene()
                {
                    await MainMenuContext.Initialize();
                }

                void CloseSession()
                {
                    NetworkManager.Singleton.Shutdown();
                }

                void CloseGameplayUi()
                {
                    view.EventOnClose -= OnClose;
                    view.PushViewToObjectPool();
                }
            }
        }

        static void LoadMap(MapId mapId)
        {
            using (Addressables.LoadComponent<MapView>($"Maps/Map/{mapId}", out var prefab))
            {
                var view = prefab.GetViewFromObjectPool();
            }
        }

        static void SpawnPlayer(PlayerData playerData, Vector2 spawnPosition)
        {
            using (Addressables.LoadComponent<CharacterControllerView>($"Gameplay/Character Controller", out var prefab))
            {
                var view = prefab.GetViewFromObjectPool();

                view.CharacterData = playerData.Character;
                view.CharacterControllerData = new CharacterControllerData()
                {
                    StartPosition = spawnPosition
                };
            }
        }
    }
}