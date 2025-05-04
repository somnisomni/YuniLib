using System;
using PrimeTween;
using Somni.YuniLib.Inspector;
using UnityEngine;

namespace Somni.YuniLib.Animations.Material {
    [AddComponentMenu("somni YuniLib/Animations/Material/Material Numeric Property Animator")]
    public class MaterialNumericPropertyAnimator : MaterialPropertyAnimatorBase {
        [InspectorHorizontalLine]
        [HeaderEx("Numeric Property Animator Settings", HeaderExStyle.H1)]
        [HeaderEx("Target", HeaderExStyle.H2)]
        [HelpBox("If 'Property Is Integer' is true, 'Start Value' and 'End Value' below will be floored to the nearest integer.")]
        [Tooltip("Whether the property is an Integer property.")]
        [SerializeField]
        public bool propertyIsInteger = false;
        
        [Space(20.0f)]
        [HeaderEx("Animation", HeaderExStyle.H2)]
        [Tooltip("The start value of the animation.")]
        [SerializeField]
        public float startValue = 0.0f;

        [Tooltip("The end value of the animation.")]
        [SerializeField]
        public float endValue = 1.0f;
        
        private int StartValueInt => Mathf.FloorToInt(startValue);
        private int EndValueInt => Mathf.FloorToInt(endValue);

        protected override void Awake() {
            base.Awake();
            
            // Set start value if specified
            if(setStartValueOnAwake) {
                SetPropertyValue(targetMaterialInstance, propertyId, propertyIsInteger ? StartValueInt : startValue);
            }
        }

        public override void StartAnimation(Action onComplete = null) {
            // Check if everything is set up correctly
            if(!CheckAvailability()) {
                return;
            }
            
            // Start the animation
            tween?.Stop();
            tween = Tween.MaterialProperty(
                    target: targetMaterialInstance,
                    propertyId: propertyId,
                    startValue: propertyIsInteger ? StartValueInt : startValue,
                    endValue: propertyIsInteger ? EndValueInt : endValue,
                    duration: animationData.duration,
                    ease: animationData.easing,
                    cycles: animationData.loop ? animationData.loopCycles : 1,
                    cycleMode: animationData.loopCycleMode,
                    useUnscaledTime: animationData.ignoreTimeScale)
                .OnComplete(() => {
                    // Reset to start value if specified
                    if(setStartValueOnAnimationEnd) {
                        SetPropertyValue(targetMaterialInstance, propertyId, propertyIsInteger ? StartValueInt : startValue);
                    }

                    // Invoke the completion callback
                    onComplete?.Invoke();
                });
        }
    }
}
