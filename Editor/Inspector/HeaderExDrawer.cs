using Somni.YuniLib.Inspector;
using UnityEditor;
using UnityEngine;

namespace Somni.YuniLib.Editor.Inspector {
    [CustomPropertyDrawer(typeof(HeaderExAttribute))]
    internal class HeaderExDrawer : DecoratorDrawer {
        private const float EmphasizeWidth = 5.0f;
        private const float EmphasizeMargin = 5.0f;
        private const float EmphasizeAlpha = 0.5f;
        
        private float cachedTextHeight = 0.0f;
        
        private static GUIStyle GetTextStyle(HeaderExAttribute attr) {
            GUIStyle style = new(EditorStyles.wordWrappedLabel) {
                fontSize = attr.TextSize,
                fontStyle = (attr.Bold ? FontStyle.Bold : FontStyle.Normal)
                            | (attr.Italic ? FontStyle.Italic : FontStyle.Normal)
            };
            
            return style;
        }
        
        public override float GetHeight() {
            var attr = (HeaderExAttribute)attribute;
            
            if(cachedTextHeight > 0.0f) {
                return cachedTextHeight + (attr.YPadding * 2);
            }
            
            return EditorGUIUtility.singleLineHeight + (attr.YPadding * 2);
        }

        public override void OnGUI(Rect position) {
            var attr = (HeaderExAttribute)attribute;
            GUIStyle style = GetTextStyle(attr);
            
            float textHeight = style.CalcHeight(new GUIContent(attr.Text), EditorGUIUtility.currentViewWidth);
            cachedTextHeight = textHeight;

            if(attr.Emphasize) {
                Rect emphasizeRect = EditorGUI.IndentedRect(position);
                emphasizeRect.y += attr.YPadding;
                emphasizeRect.width = EmphasizeWidth;
                emphasizeRect.height = textHeight;
                
                Color emphasizeColor = style.normal.textColor;
                emphasizeColor.a = EmphasizeAlpha;
                EditorGUI.DrawRect(emphasizeRect, emphasizeColor);
            }
            
            Rect textRect = EditorGUI.IndentedRect(position);
            textRect.x += attr.Emphasize ? EmphasizeWidth + EmphasizeMargin : 0.0f;
            textRect.y += attr.YPadding;
            textRect.height = textHeight;
            EditorGUI.LabelField(textRect, new GUIContent(attr.Text), style);
        }
    }
}
