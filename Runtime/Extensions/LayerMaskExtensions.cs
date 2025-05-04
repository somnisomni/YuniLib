using System.Linq;
using UnityEngine;

namespace Somni.YuniLib.Extensions {
    public static class LayerMaskExtensions {
        /// <summary>
        /// Checks if the <see cref="LayerMask" /> contains the specified layer.
        /// </summary>
        /// <param name="layerMask">The <see cref="LayerMask" /> to check.</param>
        /// <param name="targetLayer">The layer index to check for.</param>
        /// <returns><c>true</c> if the <see cref="LayerMask" /> contains the specified layer, otherwise <c>false</c>.</returns>
        public static bool Contains(this LayerMask layerMask, int targetLayer) {
            return (layerMask & (1 << targetLayer)) != 0;
        }
        
        /// <summary>
        /// Checks if the <see cref="LayerMask" /> contains the specified <see cref="LayerMask" />. <br />
        /// This is basically <b>AND</b> operation between the two <see cref="LayerMask" />s.
        /// </summary>
        /// <param name="layerMask">The <see cref="LayerMask" /> to check.</param>
        /// <param name="targetLayerMask">The <see cref="LayerMask" /> to check for.</param>
        /// <returns><c>true</c> if the two <see cref="LayerMask" />s are identical, otherwise <c>false</c>.</returns>
        public static bool ContainsAll(this LayerMask layerMask, LayerMask targetLayerMask) {
            return (layerMask & targetLayerMask) == targetLayerMask;
        }
        
        /// <summary>
        /// Checks if the <see cref="LayerMask" /> contains the specified layers. <br />
        /// This is basically <b>AND</b> operation between the <see cref="LayerMask" /> and the layers.
        /// </summary>
        /// <param name="layerMask">The <see cref="LayerMask" /> to check.</param>
        /// <param name="targetLayers">An array of the layer indexes to check for.</param>
        /// <returns><c>true</c> if the <see cref="LayerMask" /> and the layers are identical, otherwise <c>false</c>.</returns>
        public static bool ContainsAll(this LayerMask layerMask, params int[] targetLayers) {
            return targetLayers.All(targetLayer => layerMask.Contains(targetLayer));
        }
    }
}
