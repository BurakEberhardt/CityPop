// using UnityEngine;
//
// namespace CityPop.Character
// {
//     public class CharacterVisualsController
//     {
//         CharacterVisualsData _data;
//
//         public CharacterVisualsData Data
//         {
//             get => _data;
//             set
//             {
//                 _data = value;
//                 _body ??= new BodyVisualsController();
//                 _body.Data = _data.Body;
//             }
//         }
//
//         public CharacterVisualsController()
//         {
//         }
//             
//         public CharacterVisualsController(CharacterVisualsData data)
//         {
//             Data = data;
//         }
//
//         BodyVisualsController _body;
//         public BodyVisualsData Body
//         {
//             get => _body.Data;
//             set => _body.Data = value;
//         }
//
//         public HairVisualsController Hair;
//         public FaceVisualsController Face;
//     }
//
//     public class BodyVisualsController
//     {
//         public BodyVisualsData Data;
//
//         public BodyVisualsController()
//         {
//         }
//         
//         public BodyVisualsController(BodyVisualsData data)
//         {
//             Data = data;
//         }
//     }
//     
//     public class HairVisualsController
//     {
//         public BodyType Type;
//         public Color32 Color;
//     }
//     
//     public class FaceVisualsController
//     {
//         public BodyType Type;
//         public Color32 Color;
//     }
// }