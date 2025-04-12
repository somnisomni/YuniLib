using System;
using Somni.YuniLib.Inspector;
using UnityEngine;

#if UNITY_EDITOR
namespace UnityEditor {
    [CustomPropertyDrawer(typeof(InspectorHorizontalLineAttribute), true)]
    public class InspectorHorizontalLineAttributeDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var attr = (InspectorHorizontalLineAttribute)attribute;
            
            return attr.LineHeight
                   + (attr.YPadding * 2)
                   + EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var attr = (InspectorHorizontalLineAttribute)attribute;
            
            Color color = GUI.color;
            color.a = attr.LineColorAlpha;
            
            Rect lineRect = EditorGUI.IndentedRect(position);
            lineRect.y += attr.YPadding;
            lineRect.height = attr.LineHeight;
            EditorGUI.DrawRect(lineRect, color);

            Rect propertyRect = lineRect;
            propertyRect.y += attr.LineHeight + attr.YPadding;
            propertyRect.height = EditorGUI.GetPropertyHeight(property, label, true);
            EditorGUI.PropertyField(propertyRect, property, label, true);
        }
    }
}
#endif

namespace Somni.YuniLib.Inspector {
    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorHorizontalLineAttribute : PropertyAttribute {
        public float LineHeight { get; }
        public float YPadding { get; }
        public float LineColorAlpha { get; }
        
        public InspectorHorizontalLineAttribute(float lineHeight = 1.0f, float yPadding = 5.0f, float lineColorAlpha = 0.25f) {
            LineHeight = lineHeight;
            YPadding = yPadding;
            LineColorAlpha = lineColorAlpha;
        }
    }
}
