using System;
using PrimeTween;
using Somni.YuniLib.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Somni.YuniLib.UI.Utility {
    [AddComponentMenu("somni YuniLib/UI/Utility/Fade In Out")]
    public class CommonFadeInOut : MonoBehaviour {
        /// <summary>
        /// A target GameObject to be faded in/out.
        /// If this is not set, the GameObject attached to this component will be used as the target.
        /// </summary>
        [Header("Target settings")]
        [Tooltip("A custom target GameObject to be faded in/out. If this is not set, the GameObject attached to this component will be used as the target.")]
        [SerializeField]
        private GameObject customTarget = null;
        
        /// <summary>
        /// Duration of the fade animation.
        /// </summary>
        [Header("Animation settings")]
        [Tooltip("Duration of the fade animation.")]
        [SerializeField]
        public float duration = 1.0f;

        /// <summary>
        /// Easing function of the fade animation.
        /// </summary>
        [Tooltip("Easing function of the fade animation.")]
        [SerializeField]
        public Ease easing = Ease.OutQuad;

        /// <summary>
        /// Whether to start fade-in animation when this component is enabled.
        /// If both `fadeInOnEnable` and `fadeOutOnEnable` are true, `fadeInOnEnable` will be prioritized.
        /// </summary>
        [Header("Behavior settings")]
        [Tooltip("Whether to start fade-in animation when this component is enabled.")]
        [SerializeField]
        public bool fadeInOnEnable = true;

        /// <summary>
        /// Whether to start fade-out animation when this component is enabled.
        /// If both `fadeInOnEnable` and `fadeOutOnEnable` are true, `fadeInOnEnable` will be prioritized.
        /// </summary>
        [Tooltip("Whether to start fade-out animation when this component is enabled.")]
        [SerializeField]
        public bool fadeOutOnEnable = false;

        /// <summary>
        /// Whether to destroy the GameObject attached to this component after the fade-out animation is completed.
        /// </summary>
        [Tooltip("Whether to destroy the GameObject attached to this component after the fade-out animation is completed.")]
        [SerializeField]
        public bool destroyAfterFadeOut = true;

        /// <summary>
        /// Events to be invoked when the fade animation is completed, regardless of fade-in or fade-out.
        /// </summary>
        [Header("Events")]
        [Tooltip("Events to be invoked when the fade animation is completed, regardless of fade-in or fade-out.")]
        public UnityEvent onFadeComplete;
        
        /// <summary>
        /// Events to be invoked when the fade-in animation is completed.
        /// </summary>
        [Tooltip("Events to be invoked when the fade-in animation is completed.")]
        public UnityEvent onFadeInComplete;
        
        /// <summary>
        /// Events to be invoked when the fade-out animation is completed.
        /// </summary>
        [Tooltip("Events to be invoked when the fade-out animation is completed.")]
        public UnityEvent onFadeOutComplete;

        /// <summary>
        /// Whether the fade animation is currently running.
        /// </summary>
        public bool IsFading => tween is { isAlive: true };
        
        private CanvasGroup canvasGroup;
        private Tween? tween;
        
        private void Awake() {
            canvasGroup = (customTarget ?? gameObject).GetOrAddComponent<CanvasGroup>();
        }

        private void OnEnable() {
            if (fadeInOnEnable) {
                FadeIn();
            } else if (fadeOutOnEnable) {
                FadeOut();
            }
        }

        /// <summary>
        /// Start the fade-in animation.
        /// </summary>
        /// <param name="beginWithCurrentAlpha">Whether to start the animation with the current alpha value of the CanvasGroup.</param>
        /// <param name="onComplete">A callback function to be called when the animation is completed.</param>
        public void FadeIn(bool beginWithCurrentAlpha = false, Action onComplete = null) {
            tween = Tween.Alpha(
                    target: canvasGroup,
                    startValue: beginWithCurrentAlpha ? canvasGroup.alpha : 0.0f,
                    endValue: 1.0f,
                    duration: duration,
                    ease: easing)
                .OnComplete(() => {
                    tween = null;
                    onComplete?.Invoke();
                    onFadeComplete?.Invoke();
                    onFadeInComplete?.Invoke();
                });
        }

        /// <summary>
        /// Start the fade-out animation.
        /// </summary>
        /// <param name="beginWithCurrentAlpha">Whether to start the animation with the current alpha value of the CanvasGroup.</param>
        /// <param name="onComplete">A callback function to be called when the animation is completed.</param>
        public void FadeOut(bool beginWithCurrentAlpha = false, Action onComplete = null) {
            tween = Tween.Alpha(
                    target: canvasGroup,
                    startValue: beginWithCurrentAlpha ? canvasGroup.alpha : 1.0f,
                    endValue: 0.0f,
                    duration: duration,
                    ease: easing)
                .OnComplete(() => {
                    tween = null;
                    onComplete?.Invoke();
                    onFadeComplete?.Invoke();
                    onFadeOutComplete?.Invoke();
                    
                    if(destroyAfterFadeOut) {
                        Destroy(customTarget ?? gameObject);
                    }
                });
        }
    }
}
