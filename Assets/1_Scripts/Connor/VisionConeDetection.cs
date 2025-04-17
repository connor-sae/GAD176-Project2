using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeDetection : MonoBehaviour
{
    [SerializeField] float viewConeAngle = 45;
    [SerializeField] float viewConeDistance = 4;
    [SerializeField] Transform viewOrigin;

    /// <summary>
    /// Detects whether the given Collider is within the view Cone and is unobstructed from the view Origin
    /// </summary>
    /// <returns>whether the object is detected</returns>
    protected bool Detect(Collider targetCollider)
    {
        Vector3 targetDirection = targetCollider.transform.position - viewOrigin.position;
        //
        float angleDifference = Vector3.Angle(transform.forward, targetDirection);

        if (angleDifference < viewConeAngle && targetDirection.magnitude < viewConeDistance)
        {
            Physics.Raycast(viewOrigin.position, targetDirection, out RaycastHit hit, targetDirection.magnitude);
            return hit.collider == targetCollider;
        }
        return false;
    }
}
