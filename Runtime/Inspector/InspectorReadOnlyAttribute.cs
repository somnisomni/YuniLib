using System;
using UnityEngine;

namespace Somni.YuniLib.Inspector {
    public enum InspectorReadOnlyMode {
        Both,
        RuntimeOnly,
        EditorOnly,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorReadOnlyAttribute : PropertyAttribute {
        public InspectorReadOnlyMode Mode { get; }

        public InspectorReadOnlyAttribute(InspectorReadOnlyMode mode = InspectorReadOnlyMode.Both) {
            Mode = mode;
        }
    }
}
