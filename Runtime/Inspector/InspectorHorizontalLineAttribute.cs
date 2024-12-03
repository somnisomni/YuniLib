using System;
using Somni.YuniLib.Inspector;
using UnityEngine;

#if UNITY_EDITOR
namespace UnityEditor {
    [CustomPropertyDrawer(typeof(InspectorHorizontalLineAttribute), true)]
    public class InspectorHorizontalLineAttributeDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight + ((InspectorHorizontalLineAttribute)attribute).LineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            InspectorHorizontalLineAttribute attr = (InspectorHorizontalLineAttribute)attribute;
            Rect rect = EditorGUI.IndentedRect(position);
            rect.y += EditorGUIUtility.singleLineHeight / 3;
            rect.height = attr.LineHeight;
            EditorGUI.DrawRect(rect, attr.LineColor);
        }
    }
}
#endif

namespace Somni.YuniLib.Inspector {
    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorHorizontalLineAttribute : PropertyAttribute {
        public float LineHeight { get; }
        public Color LineColor { get; }
        
        public InspectorHorizontalLineAttribute(float lineHeight = 1.0f, Color? lineColor = null) {
            LineHeight = lineHeight;
            LineColor = lineColor ?? Color.black;
        }
    }
}
