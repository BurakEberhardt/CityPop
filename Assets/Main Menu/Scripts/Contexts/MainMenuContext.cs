using System;
using System.Linq;
using System.Threading.Tasks;
using CityPop.Character;
using CityPop.CharacterCreator.Views;
using CityPop.Gameplay.Contexts;
using CityPop.Player.Constants;
using CityPop.Player.Data;
using CityPop.Player.Extensions;
using CityPop.World.Data;
using Core;
using Core.Context;
using Persistence;
using SavegameSelector.Data;
using SavegameSelector.Views;
using StartSession.Views;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Zen.Core.Addressables;
using Zen.Core.Extensions;
using Zen.Core.View;

namespace CityPop.MainMenu.Contexts
{
    public class MainMenuContext : View
    {
        public static async Task Initialize()
        {
            var context = await Context.LoadAsync<MainMenuContext>("Main Menu");
            OpenSavegameSelector();
        }

        static void OpenSavegameSelector()
        {
            using (Addressables.LoadComponent("Ui/Savegame Selector", out SavegameSelectorMenu prefab))
            {
                var persistenceService = ServiceLocator.Get<PersistenceService>();
                var playersData = persistenceService.LoadFolder<PlayerData>(PlayerDataConstants.PersistenceFolderPath);
                Array.Sort(playersData, (a, b) => a.CreationDate.CompareTo(b.CreationDate));
                var players = playersData.ToList();
                var view = Instantiate(prefab);
                view.SavegameSelectorData = new SavegameSelectorData()
                {
                    Players = players
                };

                view.EventCreateNew += CreateNew;
                view.EventPlayerSelected += SelectPlayer;
                view.EventPlayerEdit += EditPlayer;
                view.EventPlayerDelete += DeletePlayer;

                void CreateNew()
                {
                    CloseSavegameSelector();
                    OpenCharacterCreator(new PlayerData()
                    {
                        Guid = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                        Character = new CharacterData()
                        {
                            Name = string.Empty,
                            Visuals = new CharacterVisualsData()
                            {
                                Body = new BodyVisualsData(),
                                Face = new FaceVisualsData(),
                                Hair = new HairVisualsData(),
                            }
                        },
                    });
                }

                void SelectPlayer(PlayerData player)
                {
                    CloseSavegameSelector();
                    OpenStartSessionMenu(player);
                }

                void EditPlayer(PlayerData player)
                {
                    CloseSavegameSelector();
                    OpenCharacterCreator(player);
                }

                void DeletePlayer(PlayerData player)
                {
                    players.Remove(player);
                    view.SavegameSelectorData.Players = view.SavegameSelectorData.Players;
                    persistenceService.Delete(player.GetSaveFilePath());
                }

                void CloseSavegameSelector()
                {
                    view.EventCreateNew -= CreateNew;
                    view.EventPlayerSelected -= EditPlayer;
                    view.EventPlayerEdit -= EditPlayer;
                    view.EventPlayerDelete -= DeletePlayer;

                    view.SavegameSelectorData = null;
                    view.PushViewToObjectPool();
                }
            }
        }

        static void OpenCharacterCreator(PlayerData playerData)
        {
            using (Addressables.LoadComponent("Ui/Character Creator", out CharacterCreatorMenu prefab))
            {
                var view = Instantiate(prefab);
                view.CharacterData = playerData.Character;
                view.EventCreate += OnCreate;

                void OnCreate(CharacterData characterData)
                {
                    SavePlayer(playerData);
                    CloseCharacterCreator();
                    OpenSavegameSelector();
                }

                void SavePlayer(PlayerData playerData)
                {
                    var persistenceService = ServiceLocator.Get<PersistenceService>();
                    persistenceService.Save(playerData.GetSaveFilePath(), playerData);
                }

                void CloseCharacterCreator()
                {
                    view.EventCreate -= OnCreate;

                    view.CharacterData = null;
                    view.PushViewToObjectPool();
                }
            }
        }

        static void OpenStartSessionMenu(PlayerData playerData)
        {
            using (Addressables.LoadComponent("Ui/Start Session", out StartSessionMenu prefab))
            {
                var view = Instantiate(prefab);
                view.PlayerData = playerData;
                view.EventHost += OnHost;
                view.EventJoin += OnJoin;
                view.EventChangeCharacter += OnChangeCharacter;

                async void OnHost(PlayerData playerData)
                {
                    Debug.Log($"Hosting as {playerData.Character.Name}");

                    CloseStartSession();
                    NetworkManager.Singleton.StartHost();

                    await GameplayContext.Initialize(new WorldData()
                    {
                        SpawnPosition = new WorldPosition()
                        {
                            Position = Vector2.zero,
                            MapId = 0
                        }
                    }, playerData);
                }

                void OnJoin(PlayerData playerData, string address)
                {
                    Debug.Log($"Join \"{address}\" as {playerData.Character.Name}");

                    CloseStartSession();
                    NetworkManager.Singleton.StartClient(address, 12345);
                }

                void OnChangeCharacter(PlayerData playerData)
                {
                    CloseStartSession();
                    OpenSavegameSelector();
                }

                void CloseStartSession()
                {
                    view.EventHost -= OnHost;
                    view.EventJoin -= OnJoin;
                    view.EventChangeCharacter -= OnChangeCharacter;

                    view.PlayerData = null;
                    view.PushViewToObjectPool();
                }
            }
        }
    }
}