using System;
using UnityEngine;

namespace Somni.YuniLib.Extensions {
    public static class GameObjectExtensions {
        public static RectTransform GetRectTransform(this GameObject gameObject) {
            if(!gameObject) {
                throw new ArgumentNullException(nameof(gameObject), "GameObject is null.");
            }

            if(!gameObject.TryGetComponent(out RectTransform rectTransform)) {
                throw new InvalidOperationException("RectTransform is not found in this GameObject.");
            }
            
            return rectTransform;
        }
        
        public static RectTransform GetRectTransform(this Component component) {
            return GetRectTransform(component.gameObject);
        }
        
        public static TComponent GetOrAddComponent<TComponent>(this GameObject gameObject) where TComponent : Component {
            if(!gameObject) {
                throw new ArgumentNullException(nameof(gameObject), "GameObject is null.");
            }
            
            return gameObject.TryGetComponent(out TComponent foundComponent)
                ? foundComponent
                : gameObject.AddComponent<TComponent>();
        }
        
        public static TComponent GetOrAddComponent<TComponent>(this Component component) where TComponent : Component {
            return GetOrAddComponent<TComponent>(component.gameObject);
        }
    }
}
