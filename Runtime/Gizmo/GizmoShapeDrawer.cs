using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Somni.YuniLib.Gizmo {
    public static class GizmoShapeDrawer {
        public static void DrawCircularSector(Vector3 center,
            Vector3 normal,
            Vector3 direction,
            float angleDegree,
            float radius,
            Color color,
            float colorMaxAlpha = 0.5f) {
#if UNITY_EDITOR
            Color originalHandleColor = Handles.color;

            Handles.color = new Color(color.r, color.g, color.b, Mathf.Min(color.a, colorMaxAlpha));
            Handles.DrawSolidArc(center, normal, direction, angleDegree / 2, radius);
            Handles.DrawSolidArc(center, normal, direction, -angleDegree / 2, radius);
            Handles.color = originalHandleColor;
#endif
        }

        public static void DrawCircularSector(Transform target,
            Vector3 normal,
            float angleDegree,
            float radius,
            Color color,
            float colorMaxAlpha = 0.5f) {
            DrawCircularSector(target.position, normal, target.forward, angleDegree, radius, color, colorMaxAlpha);
        }

        public static void DrawDisc(Vector3 center,
            Vector3 normal,
            float radius,
            Color color,
            float colorMaxAlpha = 0.5f) {
#if UNITY_EDITOR
            Color originalHandleColor = Handles.color;

            Handles.color = new Color(color.r, color.g, color.b, Mathf.Min(color.a, colorMaxAlpha));
            Handles.DrawSolidDisc(center, normal, radius);
            Handles.color = originalHandleColor;
#endif
        }

        public static void DrawDisc(Transform target,
            Vector3 normal,
            float radius,
            Color color,
            float colorMaxAlpha = 0.5f) {
            DrawDisc(target.position, normal, radius, color, colorMaxAlpha);
        }
    }
}
