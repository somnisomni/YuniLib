using System;
using PrimeTween;
using UnityEngine;

namespace Somni.YuniLib.Internal.Animations {
    internal interface ITweenActor {
        public void StartAnimation(Action onComplete = null);
        public void StopAnimation(bool immediate = false);
    }

    public abstract class SingleTweenActorBase : MonoBehaviour, ITweenActor {
        protected Tween? tween = null;

        /// <summary>
        /// Start the animation.
        /// </summary>
        /// <param name="onComplete">A callback function to be called when the animation is completed.</param>
        public abstract void StartAnimation(Action onComplete = null);

        /// <summary>
        /// Stop the animation. By default, the animation will be 'completed' immediately unless <c>immediate</c> is set to true.
        /// </summary>
        /// <param name="immediate">Whether to stop the animation immediately.</param>
        public virtual void StopAnimation(bool immediate = false) {
            if (immediate) {
                tween?.Stop();
            } else {
                tween?.Complete();
            }
            
            tween = null;
        }
    }
}
