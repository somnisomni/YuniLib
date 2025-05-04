using System;
using PrimeTween;
using Somni.YuniLib.Extensions;
using Somni.YuniLib.Inspector;
using Somni.YuniLib.Inspector.Internal;
using Somni.YuniLib.Internal.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Somni.YuniLib.UI {
    [AddComponentMenu("somni YuniLib/UI/UI Simple Fader")]
    public class UISimpleFader : SingleTweenActorBase {
        /// <summary>
        /// Enum type of fade types.
        /// </summary>
        public enum FadeType { In, Out }

        /// <summary>
        /// A custom target GameObject to be faded in/out.
        /// If this is not set, the GameObject attached to this component will be used as the target.
        /// </summary>
        [YuniLibBanner(nameof(UISimpleFader))]
        [HeaderEx("Target", HeaderExStyle.H1)]
        [HelpBox("If 'Target To Fade' is not set, the GameObject where this component attached to will be used as the target.")]
        [Tooltip("A custom target GameObject to be faded in/out.")]
        [SerializeField]
        private GameObject targetToFade = null;

        /// <summary>
        /// Animation data for the fade animation.
        /// </summary>
        [InspectorHorizontalLine]
        [HeaderEx("Fade Animation", HeaderExStyle.H1)]
        [SerializeField]
        public TweenAnimationData animationData;

        /// <summary>
        /// Whether to start fade animation when this component is enabled.
        /// </summary>
        [InspectorHorizontalLine]
        [HeaderEx("Default Behaviour", HeaderExStyle.H1)]
        [Tooltip("Whether to start fade animation when this component is enabled.")]
        [SerializeField]
        public bool fadeOnEnable = true;
        
        /// <summary>
        /// Default fade type.
        /// </summary>
        [Tooltip("Default fade type to be played when this component is enabled.")]
        [SerializeField]
        public FadeType fadeTypeOnEnable = FadeType.In;
        
        /// <summary>
        /// Whether to destroy the GameObject attached to this component after the fade-out animation is completed.
        /// </summary>
        [Tooltip("Whether to destroy the GameObject attached to this component after the fade-out animation is completed.")]
        [SerializeField]
        public bool destroyAfterFadeOut = true;

        /// <summary>
        /// Events to be invoked when the fade animation is completed, regardless of fade-in or fade-out.
        /// </summary>
        [InspectorHorizontalLine]
        [HeaderEx("Event", HeaderExStyle.H1)]
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
        
        private void Awake() {
            if(!targetToFade) {
                targetToFade = gameObject;
            }
            
            canvasGroup = targetToFade.GetOrAddComponent<CanvasGroup>();
        }

        private void OnEnable() {
            if(fadeOnEnable) {
                StartAnimation();
            }
        }

        /// <inheritdoc />
        public override void StartAnimation(Action onComplete = null) {
            switch(fadeTypeOnEnable) {
                case FadeType.In:
                    FadeIn(onComplete: onComplete);
                    break;
                case FadeType.Out:
                    FadeOut(onComplete: onComplete);
                    break;
                default:
                    throw new InvalidOperationException("Unknown fade type.");
            }
        }

        /// <summary>
        /// Start the fade-in animation.
        /// </summary>
        /// <param name="beginWithCurrentAlpha">Whether to start the animation with the current alpha value of the CanvasGroup.</param>
        /// <param name="onComplete">A callback function to be called when the animation is completed.</param>
        public void FadeIn(bool beginWithCurrentAlpha = false, Action onComplete = null) {
            StopAnimation();
            
            tween = Tween.Alpha(
                    target: canvasGroup,
                    startValue: beginWithCurrentAlpha ? canvasGroup.alpha : 0.0f,
                    endValue: 1.0f,
                    duration: animationData.duration,
                    ease: animationData.easing,
                    cycles: animationData.loop ? animationData.loopCycles : 1,
                    cycleMode: animationData.loopCycleMode,
                    useUnscaledTime: animationData.ignoreTimeScale)
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
        /// <param name="preventDestroy">Whether to prevent the GameObject from being destroyed after the fade-out animation is completed. This overrides the value of <see cref="destroyAfterFadeOut" /> if this param's value is <c>true</c>.</param>
        /// <param name="onComplete">A callback function to be called when the animation is completed.</param>
        public void FadeOut(bool beginWithCurrentAlpha = false, bool preventDestroy = false, Action onComplete = null) {
            StopAnimation();
            
            tween = Tween.Alpha(
                    target: canvasGroup,
                    startValue: beginWithCurrentAlpha ? canvasGroup.alpha : 1.0f,
                    endValue: 0.0f,
                    duration: animationData.duration,
                    ease: animationData.easing,
                    cycles: animationData.loop ? animationData.loopCycles : 1,
                    cycleMode: animationData.loopCycleMode,
                    useUnscaledTime: animationData.ignoreTimeScale)
                .OnComplete(() => {
                    tween = null;
                    onComplete?.Invoke();
                    onFadeComplete?.Invoke();
                    onFadeOutComplete?.Invoke();
                    
                    if(destroyAfterFadeOut && !preventDestroy) {
                        Destroy(targetToFade);
                    }
                });
        }
    }
}
