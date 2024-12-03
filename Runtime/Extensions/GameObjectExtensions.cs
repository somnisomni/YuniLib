using System;
using UnityEngine;

namespace Somni.YuniLib.Extensions {
    public static class GameObjectExtensions {
        public static RectTransform GetRectTransform(this GameObject gameObject) {
            if(!gameObject) {
                throw new ArgumentException("GameObject is null.");
            }

            if(!gameObject.TryGetComponent(out RectTransform rectTransform)) {
                throw new InvalidOperationException("RectTransform is not found in this GameObject.");
            }
            
            return rectTransform;
        }
        
        public static RectTransform GetRectTransform(this Component component) {
            return GetRectTransform(component.gameObject);
        }
    }
}
