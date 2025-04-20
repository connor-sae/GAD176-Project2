using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public class VisionConeDetection : MonoBehaviour
    {
        [SerializeField] float viewConeAngle = 45;
        [SerializeField] float viewConeDistance = 4;
        [SerializeField] Transform viewOrigin;

        /// <summary>
        /// Detects whether the given Collider is within the view Cone and is unobstructed from the view Origin
        /// </summary>
        /// <returns>whether the object is detected</returns>
        protected bool Detect(Player player)
        {
            Vector3 targetDirection = player.detectionTarget.position - viewOrigin.position;
            //ignore vertical difference
            targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;
            
            float angleDifference = Vector3.Angle(transform.forward, targetDirection);

            if (angleDifference < viewConeAngle && targetDirection.magnitude < viewConeDistance)
            {
                if(Physics.Raycast(viewOrigin.position, targetDirection, out RaycastHit hit, viewConeDistance))
                {
                    Debug.Log(hit.collider.name);
                    return hit.collider == player.activeCollider;
                }
            }
            return false;
        }
    }
}
