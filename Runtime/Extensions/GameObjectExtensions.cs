using System;
using UnityEngine;

namespace Somni.YuniLib.Extensions {
    public static class GameObjectExtensions {
        /// <summary>
        /// Get the <see cref="RectTransform" /> component from the <see cref="GameObject" />.
        /// </summary>
        /// <param name="gameObject">A <see cref="GameObject" /> where <see cref="RectTransform" /> has attached to.</param>
        /// <returns><see cref="RectTransform" /> component instance if exists, otherwise <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If the <see cref="GameObject" /> passed as the parameter here is <c>null</c> or destroyed.</exception>
        public static RectTransform GetRectTransform(this GameObject gameObject) {
            if(!gameObject) {
                throw new ArgumentNullException(nameof(gameObject), "GameObject is null.");
            }

            return !gameObject.TryGetComponent(out RectTransform rectTransform) ? null : rectTransform;
        }
        
        /// <summary>
        /// Get the <see cref="RectTransform" /> component from the <see cref="GameObject" /> where specified <see cref="Component" /> has attached to.
        /// </summary>
        /// <param name="component">A <see cref="Component" /> where the parent <see cref="GameObject" /> contains <see cref="RectTransform" />.</param>
        /// <returns><see cref="RectTransform" /> component instance if exists, otherwise <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If the <see cref="Component" /> passed as the parameter here or parent <see cref="GameObject" /> is <c>null</c> or destroyed.</exception>
        public static RectTransform GetRectTransform(this Component component) {
            if(!component) {
                throw new ArgumentNullException(nameof(component), "Component is null.");
            }
            
            return GetRectTransform(component.gameObject);
        }
        
        /// <summary>
        /// Get the <see cref="Component" /> of specified type from the <see cref="GameObject" />. If the component does not exist, create a new instance of the component to <see cref="GameObject" />. <br />
        /// This makes sure that the <see cref="GameObject" /> always has the specified component attached to it.
        /// </summary>
        /// <param name="gameObject">A <see cref="GameObject" /> where the <see cref="Component" /> has (or should be) attached to.</param>
        /// <typeparam name="TComponent">Class type of the <see cref="Component" />.</typeparam>
        /// <returns>A <see cref="Component" /> instance with specified type, which is already attached or newly created on the <see cref="GameObject" />.</returns>
        /// <exception cref="ArgumentNullException">If the <see cref="GameObject" /> passed as the parameter here is <c>null</c> or destroyed.</exception>
        public static TComponent GetOrAddComponent<TComponent>(this GameObject gameObject) where TComponent : Component {
            if(!gameObject) {
                throw new ArgumentNullException(nameof(gameObject), "GameObject is null.");
            }
            
            return gameObject.TryGetComponent(out TComponent foundComponent)
                ? foundComponent
                : gameObject.AddComponent<TComponent>();
        }
        
        /// <summary>
        /// Get the <see cref="Component" /> of specified type from the <see cref="GameObject" /> where specified <see cref="Component" /> has attached to. If the component does not exist, create a new instance of the component to <see cref="GameObject" />. <br />
        /// This makes sure that the <see cref="GameObject" /> always has the specified component attached to it.
        /// </summary>
        /// <param name="component">A <see cref="Component" /> where the <see cref="Component" /> has (or should be) attached to the parent <see cref="GameObject" /> of it.</param>
        /// <typeparam name="TComponent">Class type of the <see cref="Component" />.</typeparam>
        /// <returns>A <see cref="Component" /> instance with specified type, which is already attached or newly created on the <see cref="GameObject" />.</returns>
        /// <exception cref="ArgumentNullException">If the <see cref="GameObject" /> passed as the parameter here is <c>null</c> or destroyed.</exception>
        public static TComponent GetOrAddComponent<TComponent>(this Component component) where TComponent : Component {
            return GetOrAddComponent<TComponent>(component.gameObject);
        }
    }
}
