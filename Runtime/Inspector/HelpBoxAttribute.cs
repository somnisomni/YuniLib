using System;
using UnityEngine;

namespace Somni.YuniLib.Inspector {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class HelpBoxAttribute : PropertyAttribute {
        public string Text { get; }
        public HelpBoxType Type { get; }
        public float YPadding { get; }

        public HelpBoxAttribute(string text, HelpBoxType type = HelpBoxType.Info, float yPadding = 5.0f) {
            Text = text;
            Type = type;
            YPadding = yPadding;
        }
    }
    
    public enum HelpBoxType {
        Info,
        Warning,
        Error
    }
}
