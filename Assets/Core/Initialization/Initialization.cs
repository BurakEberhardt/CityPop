using System;
using System.Linq;
using CityPop.Character;
using CityPop.CharacterCreator.Views;
using Core;
using Persistence;
using Player.Data;
using SavegameSelector.Data;
using SavegameSelector.Views;
using UnityEngine.Assertions.Must;
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
                var playersData = persistenceService.LoadFolder<PlayerData>("Players");
                Array.Sort(playersData, (a,b) => a.CreationDate.CompareTo(b.CreationDate));
                var view = Instantiate(prefab);
                view.SavegameSelectorData = new SavegameSelectorData()
                {
                    Players = playersData.ToList()
                };

                view.EventCreateNew += CreateNew;
                view.EventCharacterSelected += SelectCharacter;

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

                void SelectCharacter(PlayerData player)
                {
                    CloseSavegameSelector();
                    OpenCharacterCreator(player);
                }

                void CloseSavegameSelector()
                {
                    view.EventCreateNew -= CreateNew;
                    view.EventCharacterSelected -= SelectCharacter;
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
                    persistenceService.Save($"Players/{playerData.Guid}.json", playerData);
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