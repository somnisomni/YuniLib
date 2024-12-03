using System;
using Somni.YuniLib.Inspector;
using UnityEngine;

#if UNITY_EDITOR
namespace UnityEditor {
    [CustomPropertyDrawer(typeof(InspectorReadOnlyAttribute), true)]
    public class InspectorReadOnlyAttributeDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            GUI.enabled = !Application.isPlaying && ((InspectorReadOnlyAttribute)attribute).RuntimeOnly;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif

namespace Somni.YuniLib.Inspector {
    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorReadOnlyAttribute : PropertyAttribute {
        public readonly bool RuntimeOnly;

        public InspectorReadOnlyAttribute(bool runtimeOnly = false) {
            RuntimeOnly = runtimeOnly;
        }
    }
}
