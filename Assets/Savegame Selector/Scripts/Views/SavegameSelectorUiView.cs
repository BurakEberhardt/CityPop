using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CityPop.Character;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Player.Data;
using SavegameSelector.Data;
using UnityEngine;

namespace SavegameSelector.Views
{
    [DataBinding(typeof(SavegameSelectorData))]
    public partial class SavegameSelectorUiView : View
        , SavegameSelectorData.IPlayersListener
    {
        [SerializeField] SavegameSlotUiView _savegameSlotView;
        [SerializeField] Transform _savegameSlotParent;
        readonly List<SavegameSlotUiView> _savegameSlotViews = new();
        
        void Start()
        {
            var players = new List<PlayerData>()
            {
                new PlayerData()
                {
                    Character = new CharacterData()
                    {
                        Name = "Buri",
                        Visuals = new CharacterVisualsData()
                        {
                            Body = new BodyVisualsData() {Type = (BodyType) 1, Color = Color.white},
                            Hair = new HairVisualsData() {Type = (HairType) 2, Color = Color.white},
                            Face = new FaceVisualsData() {Type = (FaceType) 2, Color = Color.white}
                        }
                    }
                },
                new PlayerData()
                {
                    Character = new CharacterData()
                    {
                        Name = "Belli",
                        Visuals = new CharacterVisualsData()
                        {
                            Body = new BodyVisualsData() {Type = (BodyType) 0, Color = Color.white},
                            Hair = new HairVisualsData() {Type = (HairType) 1, Color = Color.white},
                            Face = new FaceVisualsData() {Type = (FaceType) 2, Color = Color.white}
                        }
                    }
                },
                new PlayerData()
                {
                    Character = new CharacterData()
                    {
                        Name = "Miri",
                        Visuals = new CharacterVisualsData()
                        {
                            Body = new BodyVisualsData() {Type = (BodyType) 0, Color = Color.white},
                            Hair = new HairVisualsData() {Type = (HairType) 0, Color = Color.cyan},
                            Face = new FaceVisualsData() {Type = (FaceType) 2, Color = Color.white}
                        }
                    }
                },
                new PlayerData()
                {
                    Character = new CharacterData()
                    {
                        Name = "Muri",
                        Visuals = new CharacterVisualsData()
                        {
                            Body = new BodyVisualsData() {Type = (BodyType) 0, Color = Color.white},
                            Hair = new HairVisualsData() {Type = (HairType) 0, Color = Color.gray},
                            Face = new FaceVisualsData() {Type = (FaceType) 2, Color = Color.white}
                        }
                    }
                }
            };

            for (var i = 0; i < 6; ++i)
            {
                players.Add(new PlayerData()
                {
                    Character = new CharacterData()
                    {
                        Name = $"Character {players.Count + 1}",
                        Visuals = new CharacterVisualsData()
                        {
                            Body = new BodyVisualsData() {Type = (BodyType) Random.Range(0, 3), Color = Random.ColorHSV(0f, 1f, 0f, 0.5f, 0.9f, 1f)},
                            Hair = new HairVisualsData() {Type = (HairType) Random.Range(0, 3), Color = Random.ColorHSV(0f, 1f, 0f, 0.5f, 0.9f, 1f)},
                            Face = new FaceVisualsData() {Type = (FaceType) Random.Range(0, 3), Color = Random.ColorHSV(0f, 1f, 0f, 0.5f, 0.9f, 1f)}
                        }
                    }
                });
            }
            
            SavegameSelectorData = new SavegameSelectorData()
            {
                Players = players.ToArray()
            };
        }

        [UpdateOnInitialize]
        void SavegameSelectorData.IPlayersListener.OnPlayers(PlayerData[] players)
        {
            CleanPreviousSavegameSlots();

            foreach (var playerData in players)
            {
                var savegameSlotView = Instantiate(_savegameSlotView, _savegameSlotParent);
                savegameSlotView.PlayerData = playerData;
                
                _savegameSlotViews.Add(savegameSlotView);
            }
        }

        void CleanPreviousSavegameSlots()
        {
            foreach (var savegameSlotView in _savegameSlotViews)
                Destroy(savegameSlotView.gameObject);
        }
    }
}