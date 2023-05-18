using System;
using System.Linq;
using CityPop.Character;
using CityPop.CharacterCreator.Views;
using CityPop.Player.Constants;
using CityPop.Player.Data;
using CityPop.Player.Extensions;
using Core;
using Persistence;
using SavegameSelector.Data;
using SavegameSelector.Views;
using UnityEngine;
using Zen.Core.Addressables;
using Zen.Core.View;

namespace CityPop.Core.Initialization
{
    public class Initialization : View
    {
        void Start()
        {
            OpenSavegameSelector();
        }

        static void OpenSavegameSelector()
        {
            using (Addressables.LoadComponent("Ui/Savegame Selector", out SavegameSelectorMenu prefab))
            {
                var persistenceService = ServiceLocator.Get<PersistenceService>();
                var playersData = persistenceService.LoadFolder<PlayerData>(PlayerDataConstants.PersistenceFolderPath);
                Array.Sort(playersData, (a,b) => a.CreationDate.CompareTo(b.CreationDate));
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
                    Debug.Log("Start Game");
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
    }
}