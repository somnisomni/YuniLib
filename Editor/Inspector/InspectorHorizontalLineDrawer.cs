using Somni.YuniLib.Inspector;
using UnityEditor;
using UnityEngine;

namespace Somni.YuniLib.Editor.Inspector {
    [CustomPropertyDrawer(typeof(InspectorHorizontalLineAttribute))]
    public class InspectorHorizontalLineDrawer : DecoratorDrawer {
        public override float GetHeight() {
            var attr = (InspectorHorizontalLineAttribute)attribute;

            return attr.LineHeight + (attr.YPadding * 2);
        }

        public override void OnGUI(Rect position) {
            var attr = (InspectorHorizontalLineAttribute)attribute;
            
            Color color = GUI.color;
            color.a = attr.LineColorAlpha;
            
            Rect lineRect = EditorGUI.IndentedRect(position);
            lineRect.y += attr.YPadding;
            lineRect.height = attr.LineHeight;
            EditorGUI.DrawRect(lineRect, color);
        }
    }
}
