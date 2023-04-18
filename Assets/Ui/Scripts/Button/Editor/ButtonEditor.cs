using UnityEditor;

namespace Zen.Ui.Editor
{
    [CustomEditor(typeof(Button), true)]
    [CanEditMultipleObjects]
    public class ButtonEditor : UnityEditor.Editor
    {
        SerializedProperty _selectableStates;

        protected void OnEnable()
        {
            _selectableStates = serializedObject.FindProperty("_selectableStates");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_selectableStates);
            serializedObject.ApplyModifiedProperties();
        }
    }
}