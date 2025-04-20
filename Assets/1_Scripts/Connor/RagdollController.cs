using System.Collections;
using System.Collections.Generic;
using GAD176.Connor;
using UnityEngine;

namespace GAD176.Connor
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private GameObject displayObject;
        [SerializeField] private Collider[] ragdollColliders;
        [SerializeField] private Rigidbody[] ragdollRigidBodies;
        [SerializeField] private bool ragdollOnStart = false;


        void Start()
        {
            Activate(ragdollOnStart);
        }

        /// <summary>
        /// sets a ragdoll's activity by disabling / enabling colliders
        /// </summary>
        /// <param name="active">set ragdoll active?</param>
        public void Activate(bool active)
        {

            if(TryGetComponent<Player>(out Player player))
            {
                player.activeCollider.enabled = !active;
                player.playerAnimater.enabled = !active;
                player.activeRB.isKinematic = active;
                player.enabled = !active;
            }
            if(TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.enabled = !active;
            }

            foreach(Collider collider in ragdollColliders)
            {
                collider.enabled = active;
            }
            foreach(Rigidbody rb in ragdollRigidBodies)
            {
                rb.isKinematic = !active;
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
}
