using Somni.YuniLib.Inspector;
using UnityEditor;
using UnityEngine;

namespace Somni.YuniLib.Editor.Inspector {
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    internal class HelpBoxDrawer : DecoratorDrawer {
        private const float MinimumHeight = 50.0f;
        
        private float cachedTextHeight = 0.0f;
        
        private static MessageType ConvertFromHelpBoxType(HelpBoxType type) {
            return type switch {
                HelpBoxType.Info => MessageType.Info,
                HelpBoxType.Warning => MessageType.Warning,
                HelpBoxType.Error => MessageType.Error,
                _ => MessageType.None
            };
        }
        
        public override float GetHeight() {
            var attr = (HelpBoxAttribute)attribute;

            float height = EditorGUIUtility.singleLineHeight;
            if(cachedTextHeight > 0.0f) {
                height = cachedTextHeight;
            }
            
            return Mathf.Max(height, MinimumHeight) + (attr.YPadding * 2);
        }

        public override void OnGUI(Rect position) {
            var attr = (HelpBoxAttribute)attribute;
            
            // Cache
            cachedTextHeight = new GUIStyle(EditorStyles.helpBox)
                .CalcHeight(new GUIContent(attr.Text), EditorGUIUtility.currentViewWidth); 
            
            // Top spacing
            Rect topRect = position;
            topRect.height = attr.YPadding;
            EditorGUI.DrawRect(topRect, Color.clear);
            
            // Help box
            Rect helpBoxRect = EditorGUI.IndentedRect(position);
            helpBoxRect.y += attr.YPadding;
            helpBoxRect.height = position.height - (attr.YPadding * 2);
            EditorGUI.HelpBox(helpBoxRect, attr.Text, ConvertFromHelpBoxType(attr.Type));
            
            // Bottom spacing
            Rect bottomRect = position;
            bottomRect.y += position.height;
            bottomRect.height = attr.YPadding;
            EditorGUI.DrawRect(bottomRect, Color.clear);
        }
    }
}
