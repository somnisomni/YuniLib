using System;
using Somni.YuniLib.Inspector;
using UnityEngine;

#if UNITY_EDITOR
namespace UnityEditor {
    [CustomPropertyDrawer(typeof(HeaderExAttribute), true)]
    public class HeaderExAttributeAttributeDrawer : PropertyDrawer {
        private const float EmphasizeWidth = 5.0f;
        private const float EmphasizeMargin = 5.0f;
        private const float EmphasizeAlpha = 0.5f;
        
        private static GUIStyle GetTextStyle(HeaderExAttribute attr) {
            GUIStyle style = new(EditorStyles.wordWrappedLabel) {
                fontSize = attr.TextSize,
                fontStyle = (attr.Bold ? FontStyle.Bold : FontStyle.Normal)
                            | (attr.Italic ? FontStyle.Italic : FontStyle.Normal)
            };
            
            return style;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var attr = (HeaderExAttribute)attribute;
            GUIStyle style = GetTextStyle(attr);
            
            float textHeight = style.CalcHeight(new GUIContent(attr.Text), EditorGUIUtility.currentViewWidth);
            float propertyHeight = EditorGUI.GetPropertyHeight(property, label, true);
            
            return textHeight
                   + (attr.YPadding * 2)
                   + propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var attr = (HeaderExAttribute)attribute;
            GUIStyle style = GetTextStyle(attr);
            float textHeight = style.CalcHeight(new GUIContent(attr.Text), EditorGUIUtility.currentViewWidth);

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
            float originalX = textRect.x;
            textRect.x += attr.Emphasize ? EmphasizeWidth + EmphasizeMargin : 0.0f;
            textRect.y += attr.YPadding;
            textRect.height = textHeight;
            EditorGUI.LabelField(textRect, new GUIContent(attr.Text), style);
            
            Rect propertyRect = textRect;
            propertyRect.x = originalX;
            propertyRect.y += textRect.height + attr.YPadding;
            propertyRect.height = EditorGUI.GetPropertyHeight(property, label, true);
            EditorGUI.PropertyField(propertyRect, property, label, true);
        }
    }
}
#endif

namespace Somni.YuniLib.Inspector {
    public enum HeaderExStyle {
        H1,
        H2,
        H3
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class HeaderExAttribute : PropertyAttribute {
        public string Text { get; }
        public int TextSize { get; }
        public float YPadding { get; }
        public bool Emphasize { get; }
        public bool Bold { get; }
        public bool Italic { get; }
        
        public HeaderExAttribute(string text, int textSize = 16, float yPadding = 5.0f, bool emphasize = false, bool bold = true, bool italic = false) {
            Text = text;
            TextSize = textSize;
            YPadding = yPadding;
            Emphasize = emphasize;
            Bold = bold;
            Italic = italic;
        }

        public HeaderExAttribute(string text, HeaderExStyle style) : this(text) {
            switch(style) {
                case HeaderExStyle.H1:
                    TextSize = 18;
                    Emphasize = true;
                    break;
                case HeaderExStyle.H2:
                    TextSize = 16;
                    break;
                case HeaderExStyle.H3:
                    TextSize = 14;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }
    }
}
