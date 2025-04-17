using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeDetection : MonoBehaviour
{
    protected bool Detect(Vector3 dectectionOrigin, Collider targetCollider, float viewConeAngle, float viewConeDistance)
    {
        Vector3 targetDirection = targetCollider.transform.position - dectectionOrigin;
        //
        float angleDifference = Vector3.Angle(transform.forward, targetDirection);

        if (angleDifference < viewConeAngle && targetDirection.magnitude < viewConeDistance)
        {
            Physics.Raycast(dectectionOrigin, targetDirection, out RaycastHit hit, targetDirection.magnitude);
            return hit.collider == targetCollider;
        }
        return false;
    }
}
