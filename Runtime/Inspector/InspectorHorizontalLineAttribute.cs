using System;
using UnityEngine;

namespace Somni.YuniLib.Inspector {
    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorHorizontalLineAttribute : PropertyAttribute {
        public float LineHeight { get; }
        public float YPadding { get; }
        public float LineColorAlpha { get; }
        
        public InspectorHorizontalLineAttribute(float lineHeight = 1.0f, float yPadding = 7.0f, float lineColorAlpha = 0.25f) {
            LineHeight = lineHeight;
            YPadding = yPadding;
            LineColorAlpha = lineColorAlpha;
        }
    }
}
