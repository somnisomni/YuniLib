using System;
using System.Collections.Generic;
using Somni.YuniLib.Inspector;
using Somni.YuniLib.Inspector.Internal;
using Somni.YuniLib.Internal.Animations;
using UnityEngine;

namespace Somni.YuniLib.Animations.Material {
    public abstract class MaterialPropertyAnimatorBase : SingleTweenActorBase {
        [YuniLibBanner("Material Property Animator")]
        [HeaderEx("Common Settings", HeaderExStyle.H1)]
        [HeaderEx("Target", HeaderExStyle.H2)]
        [HelpBox("If 'Optional Target Object' is not set, the GameObject where this component attached to will be used as the target.")]
        [Tooltip("Optional target object.")]
        [SerializeField]
        public GameObject optionalTargetObject;
        
        [Tooltip("The material reference to apply the animation to. Materials in the Renderers that match with this material will be copied.")]
        [SerializeField]
        public UnityEngine.Material targetMaterial;
        
        [Tooltip("The copied material reference of the target material. This is used to apply the animation.")]
        [SerializeField, InspectorReadOnly]
        public UnityEngine.Material targetMaterialInstance;

#if UNITY_EDITOR
        [Tooltip("ID of the material instance. This is used for debugging purposes.")]
        [SerializeField, InspectorReadOnly]
        private int targetMaterialInstanceId = -1;
#endif
        
        [Tooltip("The list of Renderers that use the specified target material. If empty, it will search for all Renderer components in this GameObject and its children.")]
        [SerializeField, InspectorReadOnly(InspectorReadOnlyMode.RuntimeOnly)]
        public List<Renderer> targetRenderers = new();
        
        [Tooltip("The property name of the material to animate.")]
        [SerializeField]
        public string propertyName = "_Amount";
        
        [Tooltip("The property ID of specified property name.")]
        [SerializeField, InspectorReadOnly]
        public int propertyId = -1;
        
        [Space(20.0f)]
        [HeaderEx("Animation", HeaderExStyle.H2)]
        [Tooltip("Common animation data for the material property animation.")]
        [SerializeField]
        public TweenAnimationData animationData;
        
        [Tooltip("Whether to start the animation when this component is enabled.")]
        [SerializeField]
        public bool animateOnEnable = true;
        
        [Tooltip("Whether to set to the start value on Awake.")]
        [SerializeField]
        public bool setStartValueOnAwake = true;

        [Tooltip("Whether to reset to the start value on animation end.")]
        [SerializeField]
        public bool setStartValueOnAnimationEnd = false;

        protected GameObject TargetObject => optionalTargetObject ? optionalTargetObject : gameObject;
        
        private const string MaterialInstanceNameFormat = "{0} (Instance)";
        private string MaterialInstanceName => string.Format(MaterialInstanceNameFormat, targetMaterial.name);

        protected virtual void Awake() {
            // Check if the target material is set
            if(!targetMaterial) {
                Debug.LogError("Target material is not set! Please check the configuration.");
                return;
            }
            
            // Get property ID
            propertyId = Shader.PropertyToID(propertyName);
            
            // Get target renderers if not specified
            if(targetRenderers.Count <= 0) {
                targetRenderers.AddRange(TargetObject.GetComponentsInChildren<Renderer>());
            }
            
            // If another animator is using the same material, use its instance
            foreach(var component in GetComponents<MaterialPropertyAnimatorBase>()) {
                // Skip if the component is this one
                if(component == this) {
                    continue;
                }

                // Skip if the target material is not the same
                if(component.targetMaterial != targetMaterial
                   || !component.targetMaterialInstance
                   || component.targetMaterialInstance.name != MaterialInstanceName) {
                    continue;
                }
                
                // After done checking, set the target material instance with existing one
                targetMaterialInstance = component.targetMaterialInstance;
                break;
            }
            
            // Create material instance if there was no existing instance
            if(!targetMaterialInstance) {
                targetMaterialInstance = new UnityEngine.Material(targetMaterial);
                targetMaterialInstance.name = MaterialInstanceName;
            }
            
#if UNITY_EDITOR
            // Set the target material instance ID for debugging purposes
            targetMaterialInstanceId = targetMaterialInstance.GetInstanceID();
#endif
            
            // Replace existing materials with the new instance
            ReplaceMaterials(targetRenderers.ToArray(), targetMaterial, targetMaterialInstance);
        }

        protected virtual void OnEnable() {
            // Start the animation if specified
            if(animateOnEnable) {
                StartAnimation();
            }
        }
        
        protected virtual void OnDisable() {
            // Stop the animation
            StopAnimation(true);
        }

        private void OnDestroy() {
            // Stop ongoing animation
            StopAnimation(true);
            
            // Check if the target material instance is still set
            if(!targetMaterialInstance) {
                return;
            }
            
            // Replace the materials back to the original
            ReplaceMaterials(targetRenderers.ToArray(), targetMaterialInstance, targetMaterial);
            
            // Destroy the material instance
            Destroy(targetMaterialInstance);
        }

        protected bool CheckAvailability() {
            // Check if the target material and the material instance are set
            if(!targetMaterial || !targetMaterialInstance) {
                Debug.LogError("Target material is not set or not instantiated! Please check the configuration.");
                return false;
            }

            // Check if the target renderers are available
            if(targetRenderers.Count <= 0) {
                Debug.LogError("No target renderers found! Please check the configuration.");
                return false;
            }

            return true;
        }
        
        private static void ReplaceMaterials(Renderer[] renderers, UnityEngine.Material original, UnityEngine.Material replaceTo) {
            foreach(var rdr in renderers) {
                // Create a new material array with the same length as the original
                var materialArray = new UnityEngine.Material[rdr.sharedMaterials.Length];

                for(int matIdx = 0; matIdx < rdr.sharedMaterials.Length; matIdx++) {
                    // Get the material in the renderer at the current index
                    var mat = rdr.sharedMaterials[matIdx];

                    if(mat && mat == original) {
                        // If the material matches the original, replace it with the new instance
                        materialArray[matIdx] = replaceTo;
                    } else {
                        // Otherwise keep the original material
                        materialArray[matIdx] = mat;
                    }
                }

                // Set the new material array to the renderer
                rdr.sharedMaterials = materialArray;
            }
        }

        protected static void SetPropertyValue<T>(UnityEngine.Material targetMaterial, int propertyId, T value) {
            if(typeof(T) == typeof(float) && value is float floatValue) {
                targetMaterial.SetFloat(propertyId, floatValue);
            } else if(typeof(T) == typeof(int) && value is int intValue) {
                targetMaterial.SetInt(propertyId, intValue);
            } else if(typeof(T) == typeof(Color) && value is Color colorValue) {
                targetMaterial.SetColor(propertyId, colorValue);
            } else if(typeof(T) == typeof(Vector4) && value is Vector4 vectorValue) {
                targetMaterial.SetVector(propertyId, vectorValue);
            } else {
                throw new NotSupportedException($"Type '{typeof(T)}' is not supported.");
            }
        }
    }
}
