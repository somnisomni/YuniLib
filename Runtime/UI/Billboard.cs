using Somni.YuniLib.Inspector;
using UnityEngine;

namespace Somni.YuniLib.UI {
    [AddComponentMenu("somni YuniLib/UI/Billboard")]
    public class Billboard : MonoBehaviour {
        [HeaderEx("Target", HeaderExStyle.H1)]
        [SerializeField]
        [Tooltip("A transform that will be rotated at this position. This usually be the transform of Camera. If this is not specified, the main camera will be used.")]
        public Transform lookTarget;

        private Vector3 lastPositionOfTarget;
        private Quaternion lastRotationOfTarget;

        private void Start() {
            if(!lookTarget) {
                lookTarget = Camera.main?.transform;
            }
        }

        private void LateUpdate() {
            if(!lookTarget) {
                return;
            }

            if(lastPositionOfTarget == lookTarget.position && lastRotationOfTarget == lookTarget.rotation) return;
            
            transform.LookAt(
                worldPosition: transform.position + (lookTarget.rotation * Vector3.forward),
                worldUp: lookTarget.rotation * Vector3.up);
            
            lastPositionOfTarget = lookTarget.position;
            lastRotationOfTarget = lookTarget.rotation;
        }
    }
}
