using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Animator disableAnim;
    [SerializeField] private GameObject displayObject;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private Rigidbody[] ragdollRigidBodies;
    [SerializeField] private bool ragdollOnStart = false;


    void Start()
    {
        if(!ragdollOnStart)
        {
            Activate(false);
        }
    }

    /// <summary>
    /// sets a ragdoll's activity by disabling / enabling colliders
    /// </summary>
    /// <param name="active">set ragdoll active?</param>
    public void Activate(bool active)
    {
        if(disableAnim != null)
            disableAnim.enabled = !active;

        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = active;
        }
        foreach(Rigidbody rb in ragdollRigidBodies)
        {
            rb.isKinematic = active;
        }
    }

    public void AddForceFromOrigin(Vector3 origin, float force)
    {
        foreach(Rigidbody rb in ragdollRigidBodies)
        {
            Vector3 direction = rb.position - origin;
            direction.Normalize();
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

}
