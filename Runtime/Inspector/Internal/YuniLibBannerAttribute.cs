using System;
using UnityEngine;

namespace Somni.YuniLib.Inspector.Internal {
    [AttributeUsage(AttributeTargets.Field)]
    internal class YuniLibBannerAttribute : PropertyAttribute {
        internal string ComponentName { get; }

        internal YuniLibBannerAttribute(string componentName) {
            ComponentName = componentName;
        }
    }
}
