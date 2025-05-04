using Somni.YuniLib.Inspector.Internal;
using UnityEditor;
using UnityEngine;

namespace Somni.YuniLib.Editor.Inspector.Internal {
    [CustomPropertyDrawer(typeof(YuniLibBannerAttribute))]
    internal class YuniLibBannerDrawer : DecoratorDrawer {
        private YuniLibBannerAttribute Attribute => (YuniLibBannerAttribute)attribute;
        
        private static float Height => 70.0f;
        private static float YPadding => 10.0f;
        private static float BorderWidth => 1.0f;
        private static Color ForegroundColor { get; } = new(0.8f, 0.8f, 0.8f, 1.0f);
        private static Color BackgroundColor { get; } = new(0.15f, 0.15f, 0.15f, 1.0f);
        private static Color BorderColor { get; } = new(0.5f, 0.5f, 0.5f, 1.0f);
        private static GUIStyle BannerTextStyle { get; } = new() {
            normal = new GUIStyleState {
                textColor = ForegroundColor
            },
            fontSize = 18,
            alignment = TextAnchor.MiddleCenter,
            clipping = TextClipping.Clip
        };
        private string BannerText => $"<size='18'><b>{Attribute.ComponentName}</b></size><br><size='12'>Powered by <b>YuniLib</b> — Made by <b>somni</b></size>";

        public override float GetHeight() {
            return Height + (YPadding * 2);
        }

        public override void OnGUI(Rect position) {
            // Force set order
            attribute.order = -65535;
            
            // Background border box
            Rect backgroundBorderRect = EditorGUI.IndentedRect(position);
            backgroundBorderRect.x -= BorderWidth;
            backgroundBorderRect.y += YPadding - BorderWidth;
            backgroundBorderRect.height = Height + (BorderWidth * 2);
            backgroundBorderRect.width += BorderWidth * 2;
            EditorGUI.DrawRect(backgroundBorderRect, BorderColor);
            
            // Background box
            Rect backgroundRect = EditorGUI.IndentedRect(position);
            backgroundRect.y += YPadding;
            backgroundRect.height = Height;
            EditorGUI.DrawRect(backgroundRect, BackgroundColor);

            // Banner text
            EditorGUI.LabelField(backgroundRect, BannerText, BannerTextStyle);
        }
    }
}
