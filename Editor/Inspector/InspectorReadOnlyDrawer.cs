using Somni.YuniLib.Inspector;
using UnityEditor;
using UnityEngine;

namespace Somni.YuniLib.Editor.Inspector {
    [CustomPropertyDrawer(typeof(InspectorReadOnlyAttribute), true)]
    internal class InspectorReadOnlyDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if(attribute is InspectorReadOnlyAttribute attr) {
                GUI.enabled = !((attr.Mode == InspectorReadOnlyMode.Both)
                                || ((attr.Mode == InspectorReadOnlyMode.RuntimeOnly) && Application.isPlaying)
                                || ((attr.Mode == InspectorReadOnlyMode.EditorOnly) && !Application.isPlaying));
            }

            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
