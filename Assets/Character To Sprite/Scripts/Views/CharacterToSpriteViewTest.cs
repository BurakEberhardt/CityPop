// using System.Collections.Generic;
// using CityPop.CharacterToTexture.Data;
// using Zen.CodeGeneration;
// using Zen.Core.PlayerLoop;
//
// namespace CityPop.CharacterToTexture.Views
// {
//     public partial class CharacterToSpriteView
//     {
//         private global::CityPop.CharacterToTexture.Data.CharacterToSpriteData _characterToSpriteData;
//         public global::CityPop.CharacterToTexture.Data.CharacterToSpriteData CharacterToSpriteData
//         {
//             get => _characterToSpriteData;
//             set
//             {
//                 if (_characterToSpriteData != null)
//                 {
//                     _characterToSpriteDataPlayersListener.RemoveDeferredListener();
//                 }
//
//                 _characterToSpriteData = value;
//                 if (_characterToSpriteData != null)
//                 {
//                     _characterToSpriteDataPlayersListener.AddDeferredListener(this, value);
//                 }
//             }
//         }
//
//         readonly CharacterToSpriteDataPlayersListener _characterToSpriteDataPlayersListener = new();
//         class CharacterToSpriteDataPlayersListener : CharacterToSpriteData.ICharacterSpritesListener, IPreLateUpdate
//         {
//             CharacterToSpriteData.ICharacterSpritesListener _listener;
//             CharacterToSpriteData _data;
//             bool _dirty;
//
//             public void AddDeferredListener(CharacterToSpriteData.ICharacterSpritesListener listener, CharacterToSpriteData data)
//             {
//                 _listener = listener;
//                 _data = data;
//                 _data.AddCharacterSpritesListener(this);
//             }
//
//             public void RemoveDeferredListener()
//             {
//                 if (_dirty)
//                 {
//                     PlayerLoop.RemoveListener(this);
//                     _dirty = false;
//                 }
//                 
//                 _data.RemoveCharacterSpritesListener(this);
//             }
//
//             void CharacterToSpriteData.ICharacterSpritesListener.OnCharacterSprites(List<CharacterSpriteData> characterSprites)
//             {
//                 if (!_dirty)
//                 {
//                     Zen.Core.PlayerLoop.PlayerLoop.AddListener(this);
//                     _dirty = true;
//                 }
//             }
//
//             void IPreLateUpdate.OnPreLateUpdate()
//             {
//                 Zen.Core.PlayerLoop.PlayerLoop.RemoveListener(this);
//                 _dirty = false;
//                 
//                 _listener.OnCharacterSprites(_data.CharacterSprites);
//             }
//         }
//     }
// }