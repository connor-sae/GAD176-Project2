using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace DanielGAD176
{
    public class EnemyDetection : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] float maxConeDistance = 5f;
        [SerializeField] float maxConeAngle = 45f;
        int counter = 0;

        // Start is called before the first frame update
        void Start()
        {
            //You need to change the tag of the player to "Player" for this to work.
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("tag player with Player");
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            VisualDetection();
        }

        private bool VisualDetection()
        {
            Vector3 distanceVector = player.transform.position - transform.position;
            distanceVector = new Vector3(distanceVector.x, 0, distanceVector.z);
            float distanceFromPlayer = distanceVector.magnitude;

            float angleDifference = Vector3.Angle(transform.forward, distanceVector.normalized);
            if (distanceFromPlayer <= maxConeDistance && angleDifference <= maxConeAngle)
            {
                if (Physics.Raycast(transform.position, distanceVector.normalized, out RaycastHit hit, maxConeDistance))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player Detected");
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * maxConeDistance);

            Vector3 left = Quaternion.Euler(0, -maxConeAngle, 0) * transform.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, left * maxConeDistance);

            Vector3 right = Quaternion.Euler(0, maxConeAngle, 0) * transform.forward;
            Gizmos.DrawRay(transform.position, right * maxConeDistance);
        }

    }
}