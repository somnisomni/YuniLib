using System;
using UnityEngine;

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
