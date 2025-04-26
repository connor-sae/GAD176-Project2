using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace DanielGAD176
{
    public enum EnemyState
    {
        Patrolling,
        Suspicious,
        Investigating,
        Chasing
    }
    public class EnemyDetection : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] float maxConeDistance = 5f;
        [SerializeField] float maxConeAngle = 45f;
        [SerializeField] float susMeter = 0f;
        [SerializeField] float susIncreaseRate = 10f;
        [SerializeField] float maxSus = 100f;

        [SerializeField] float moveSpeed;
        [SerializeField] float chaseMoveSpeed;
        [SerializeField] float rotateSpeed;

        private EnemyState currentState = EnemyState.Patrolling;

        // For testing
        [SerializeField] private float patrolMoveDistance = 3f;
        [SerializeField] private float patrolMoveSpeed = 1.5f;
        [SerializeField] private float patrolRotateSpeed = 120f; // Degrees per second
        private bool isTurning = false;
        private float distanceMoved = 0f;
        private float rotationRemaining = 0f;

        // to use this code make your enemies inherit form this and use FixedUpdate for VisualDetection (IMPORTANT).
        void Start()
        {
            moveSpeed = 1f;
            chaseMoveSpeed = 2 * moveSpeed;
            rotateSpeed = 2f;
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
            UpdateSuspicion();
            UpdateState();
            HandleStateBehavior();
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

        private void UpdateSuspicion()
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
                susMeter -= susIncreaseRate * 0.7f * Time.deltaTime;
                susMeter = Mathf.Max(susMeter, 0f);
                Debug.Log(susMeter);
            }
        }

        private void UpdateState()
        {
            if (susMeter >= maxSus)
            {
                currentState = EnemyState.Chasing;
            }
            else if (susMeter >= 75f)
            {
                currentState = EnemyState.Investigating;
            }
            else if (susMeter >= 50f)
            {
                currentState = EnemyState.Suspicious;
            }
            else
            {
                currentState = EnemyState.Patrolling;
            }
        }

        private void HandleStateBehavior()
        {
            switch (currentState)
            {
                case EnemyState.Patrolling:
                    PatrolBehavior();
                    break;
                case EnemyState.Suspicious:
                    SuspiciousBehavior();
                    break;
                case EnemyState.Investigating:
                    InvestigateBehavior();
                    break;
                case EnemyState.Chasing:
                    ChaseBehavior();
                    break;
            }
        }

        // Can update to do whatever based on game, for now will make it walk in square pattern
        protected virtual void PatrolBehavior()
        {
            // TODO: Add patrol logic here, maybe random walking on the map or from point to point

            // Patrol Logic added for testing 
            if (!isTurning)
            {
                // Move forward
                float moveStep = patrolMoveSpeed * Time.deltaTime;
                transform.position += transform.forward * moveStep;
                distanceMoved += moveStep;

                if (distanceMoved >= patrolMoveDistance)
                {
                    isTurning = true;
                    rotationRemaining = 90f; // Start 90 degree turn
                    distanceMoved = 0f; // Reset for next side
                }
            }
            else
            {
                // Rotate 90 degrees
                float rotateStep = patrolRotateSpeed * Time.deltaTime;
                rotateStep = Mathf.Min(rotateStep, rotationRemaining); // Don't overshoot

                transform.Rotate(Vector3.up, rotateStep);
                rotationRemaining -= rotateStep;

                if (rotationRemaining <= 0f)
                {
                    isTurning = false;
                }
            }

        }

        protected virtual void SuspiciousBehavior()
        {
            RotateTowardsPlayer();
        }

        protected virtual void InvestigateBehavior()
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
        }

        protected virtual void ChaseBehavior()
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer(chaseMoveSpeed);
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= 1.5f)
            {
                CatchPlayer();
            }
        }

        private void RotateTowardsPlayer()
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            if (direction.magnitude > 0.1)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
            }

        }
        // Make moveSpeed customizable depending if chasing or investigating
        private void MoveTowardsPlayer(float overrideMoveSpeed = 1f)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0;

            transform.position += direction * overrideMoveSpeed * Time.deltaTime;
        }

        // Could be changed based on game behaviour.
        protected virtual void CatchPlayer()
        {
            Debug.Log("Player Caught!!!");
            // can potentially add scene reload
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