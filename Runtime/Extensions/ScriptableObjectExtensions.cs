using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Somni.YuniLib.Extensions {
    public static class ScriptableObjectExtensions {
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject {
            if(scriptableObject == null) {
                throw new ArgumentNullException(nameof(scriptableObject), "ScriptableObject is null.");
            }
            
            return Object.Instantiate(scriptableObject);
        }
    }
}
