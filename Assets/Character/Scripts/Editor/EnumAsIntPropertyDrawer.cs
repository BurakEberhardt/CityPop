using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CityPop.Character.Editor
{
    [CustomPropertyDrawer(typeof(BodyType))]
    [CustomPropertyDrawer(typeof(HairType))]
    [CustomPropertyDrawer(typeof(FaceType))]
    public class EnumAsIntPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            property.intValue = EditorGUI.IntField(position, label, property.intValue);
            EditorGUI.EndProperty();
        }
    }
}