using System.Linq;
using UnityEngine;

namespace Somni.YuniLib.Extensions {
    public static class LayerMaskExtensions {
        public static bool Contains(this LayerMask layerMask, int targetLayer) {
            return (layerMask & (1 << targetLayer)) != 0;
        }
        
        public static bool ContainsAll(this LayerMask layerMask, LayerMask targetLayerMask) {
            return (layerMask & targetLayerMask) == targetLayerMask;
        }
        
        public static bool ContainsAll(this LayerMask layerMask, params int[] targetLayers) {
            return targetLayers.All(targetLayer => layerMask.Contains(targetLayer));
        }
    }
}
