using System;
using PrimeTween;
using Somni.YuniLib.Inspector;
using UnityEngine;

namespace Somni.YuniLib.Internal.Animations {
    [Serializable]
    public struct TweenAnimationData {
        /// <summary>
        /// Duration of the animation.
        /// </summary>
        [Tooltip("Duration of the animation.")]
        [SerializeField]
        public float duration;

        /// <summary>
        /// Easing function of the animation.
        /// </summary>
        [Tooltip("Easing function of the animation.")]
        [SerializeField]
        public Ease easing;

        /// <summary>
        /// Whether to loop the animation.
        /// </summary>
        [Tooltip("Whether to loop the animation.")]
        [SerializeField]
        public bool loop;
        
        /// <summary>
        /// Cycles of the loop. If set to -1, the loop will be infinite.
        /// </summary>
        [Tooltip("Cycles of the loop. If set to -1, the loop will be infinite.")]
        [SerializeField]
        public int loopCycles;

        /// <summary>
        /// Cycle mode of the loop.
        /// </summary>
        [Tooltip("Cycle mode of the loop.")]
        [SerializeField]
        public CycleMode loopCycleMode;

        /// <summary>
        /// Whether to ignore the timescale.
        /// </summary>
        [Tooltip("Whether to ignore the timescale.")]
        [SerializeField]
        public bool ignoreTimeScale;

        public TweenAnimationData(
            float duration = 1.0f,
            Ease easing = Ease.OutQuad,
            bool loop = false,
            int loopCycles = -1,
            CycleMode loopCycleMode = CycleMode.Rewind,
            bool ignoreTimeScale = false) {
            this.duration = duration;
            this.easing = easing;
            this.loop = loop;
            this.loopCycles = loopCycles;
            this.loopCycleMode = loopCycleMode;
            this.ignoreTimeScale = ignoreTimeScale;
        }
    }
}
