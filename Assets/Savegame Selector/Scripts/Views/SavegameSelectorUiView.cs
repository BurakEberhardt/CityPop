using System.Collections.Generic;
using System.Diagnostics;
using CityPop.Character;
using CityPop.Core.ListSynchronizer;
using Player.Data;
using SavegameSelector.Data;
using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.UnityMethods.Attributes;
using Random = UnityEngine.Random;

namespace SavegameSelector.Views
{
    [DataBinding(typeof(SavegameSelectorData))]
    public partial class SavegameSelectorUiView : View
        , SavegameSelectorData.IPlayersListener
    {
        [SerializeField] SavegameSlotUiView _savegameSlotView;
        [SerializeField] Transform _savegameSlotParent;
        
        readonly List<SavegameSlotUiView> _savegameSlotViews = new();
        ListSynchronizer<PlayerData, SavegameSlotUiView, PlayerData> _savegameUiSlotViewsSynchronizer;

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
                players.Add(CreateRandomPlayerData($"Character {players.Count + 1}"));
            }

            SavegameSelectorData = new SavegameSelectorData()
            {
                Players = players
            };
        }

        static PlayerData CreateRandomPlayerData(string name)
        {
            return new PlayerData()
            {
                Character = new CharacterData()
                {
                    Name = name,
                    Visuals = new CharacterVisualsData()
                    {
                        Body = new BodyVisualsData() {Type = (BodyType) Random.Range(0, 3), Color = Random.ColorHSV(0f, 1f, 0f, 0.5f, 0.9f, 1f)},
                        Hair = new HairVisualsData() {Type = (HairType) Random.Range(0, 3), Color = Random.ColorHSV(0f, 1f, 0f, 0.5f, 0.9f, 1f)},
                        Face = new FaceVisualsData() {Type = (FaceType) Random.Range(0, 3), Color = Random.ColorHSV(0f, 1f, 0f, 0.5f, 0.9f, 1f)}
                    }
                }
            };
        }

        [UpdateOnInitialize]
        void SavegameSelectorData.IPlayersListener.OnPlayers(List<PlayerData> players)
        {
            // Debug.Log($"{nameof(SavegameSelectorData.IPlayersListener.OnPlayers)}({players.Count})");

            var stopwatch = Stopwatch.StartNew();

            _savegameUiSlotViewsSynchronizer ??= new ListSynchronizer<PlayerData, SavegameSlotUiView, PlayerData>(
                view => view.PlayerData,
                data => data,
                CreateSlotUi,
                RemoveSlotUi,
                UpdateSlotUi);
            _savegameUiSlotViewsSynchronizer.Synchronize(_savegameSlotViews, players);

            stopwatch.Stop();
            // Debug.Log($"{nameof(SavegameSelectorData.IPlayersListener.OnPlayers)}({players.Count}, {stopwatch.ElapsedTicks})");
        }

        SavegameSlotUiView CreateSlotUi(PlayerData data, int index)
        {
            var view = _savegameSlotView.GetViewFromObjectPool(_savegameSlotParent);
            view.PlayerData = data;
            UpdateSlotUi(view, data, -1, index);
            return view;
        }

        void RemoveSlotUi(SavegameSlotUiView view, int index)
        {
            view.PlayerData = null;
            view.PushViewToObjectPool();
        }

        void UpdateSlotUi(SavegameSlotUiView view, PlayerData data, int fromIndex, int toIndex)
        {
            view.transform.SetSiblingIndex(toIndex);
        }

        [Update]
        public void AddOrRemoveMoreSlots()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                SavegameSelectorData.Players.Insert(Random.Range(0, SavegameSelectorData.Players.Count), CreateRandomPlayerData($"Dynamic {SavegameSelectorData.Players.Count}"));
                SavegameSelectorData.Players = SavegameSelectorData.Players;
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                SavegameSelectorData.Players.RemoveAt(Random.Range(0, SavegameSelectorData.Players.Count));
                SavegameSelectorData.Players = SavegameSelectorData.Players;
            }
        }
    }
}