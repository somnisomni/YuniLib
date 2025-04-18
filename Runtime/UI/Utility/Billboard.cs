﻿using UnityEngine;

namespace Somni.YuniLib.UI.Utility {
    [AddComponentMenu("somni YuniLib/UI/Utility/Billboard")]
    public class Billboard : MonoBehaviour {
        [SerializeField]
        [Tooltip("A transform that will be rotated at this position. This usually be the transform of Camera. If this is not specified, the main camera will be used.")]
        private Transform target;

        private void Start() {
            if(!target) {
                target = Camera.main?.transform;
            }
        }

        private void LateUpdate() {
            transform.LookAt(
                worldPosition: transform.position + (target.rotation * Vector3.forward),
                worldUp: target.rotation * Vector3.up);
        }
    }
}
