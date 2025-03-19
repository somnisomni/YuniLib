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
#endif

namespace Somni.YuniLib.Inspector {
    public enum InspectorReadOnlyMode {
        Both,
        RuntimeOnly,
        EditorOnly,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorReadOnlyAttribute : PropertyAttribute {
        public InspectorReadOnlyMode Mode { get; }

        public InspectorReadOnlyAttribute(InspectorReadOnlyMode mode = InspectorReadOnlyMode.Both) {
            Mode = mode;
        }
    }
}
