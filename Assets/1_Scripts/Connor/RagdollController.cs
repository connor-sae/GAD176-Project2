using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    [SerializeField] private GameObject displayObject;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private Rigidbody[] ragdollRigidBodies;

    public void Activate()
    {
        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }
        foreach(Rigidbody rb in ragdollRigidBodies)
        {
            rb.isKinematic = false;
        }
    }
}
