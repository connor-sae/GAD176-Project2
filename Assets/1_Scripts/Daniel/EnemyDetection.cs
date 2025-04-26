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
        [SerializeField] float susMeter = 0f;
        [SerializeField] float susIncreaseRate = 10f;
        [SerializeField] float maxSus = 100f;
        
        // to use this code make your enemies inherit form this and use FixedUpdate for VisualDetection (IMPORTANT).
        void Start()
        {
            
             //You need to change the tag of the player to "Player" for this to work.
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            if (player == null)
            {
                Debug.LogError("tag player with Player");
            }
        }

        
        protected virtual void FixedUpdate()
        {
            // if player detected increses suspicion based on time in view field.
            if (VisualDetection()) 
            {
                susMeter += susIncreaseRate * Time.deltaTime;
                susMeter = Mathf.Min(susMeter, maxSus);
                Debug.Log(susMeter);
            }
            else
            {
                susMeter -= susIncreaseRate * 0.5f * Time.deltaTime;
                susMeter = Mathf.Max(susMeter, 0f);
                Debug.Log(susMeter);
            } 
        }

        // fuction that checks if player is within view cone of enemy using distance and angles.
        // returns bool if detected or not.
        protected virtual bool VisualDetection()
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
        // Draws detection lines for reference.     
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