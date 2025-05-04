using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Somni.YuniLib.Extensions {
    public static class ScriptableObjectExtensions {
        /// <summary>
        /// Clone an instance of the <see cref="ScriptableObject" /> with its data using <see cref="Object.Instantiate(Object)" />.
        /// </summary>
        /// <param name="scriptableObject">An instance of the <see cref="ScriptableObject" /> to clone.</param>
        /// <typeparam name="T">Class type of the <see cref="ScriptableObject" />.</typeparam>
        /// <returns>A cloned instance of the <see cref="ScriptableObject" />.</returns>
        /// <exception cref="ArgumentNullException">If the <see cref="ScriptableObject" /> instance passed as the parameter here is <c>null</c> or destroyed.</exception>
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject {
            if(!scriptableObject) {
                throw new ArgumentNullException(nameof(scriptableObject), "ScriptableObject is null.");
            }
            
            return Object.Instantiate(scriptableObject);
        }
    }
}
