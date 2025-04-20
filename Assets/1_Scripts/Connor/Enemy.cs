using System.Collections;
using System.Collections.Generic;
using GAD176.Connor;
using UnityEngine;

namespace GAD176.Connor
{
    
    [RequireComponent(typeof(RagdollController))]
    public class Enemy : VisionConeDetection, IAlertable, IRagdoll
    {
        private Player targetPlayer;

        [SerializeField] private Transform[] patrolPoints;
        private int patrolTarget;
        private EnemyState state;
        private float patrolPointLenience = 0.4f;

        private Vector3 POI;

        [SerializeField] private float rotateSpeed = 180f;
        [SerializeField] private float patrolSpeed = 1f;
        [SerializeField] private float searchSpeed = 2f;

        [SerializeField] private float searchTime = 5f;

        [SerializeField] private float takeDownLieniency = 50;

        private RagdollController ragdoll;

        
        public void Alert(Vector3 alertOrigin)
        {
            if(searchRoutine != null)
                StopCoroutine(searchRoutine);

            state = EnemyState.SEARCH;
            POI = alertOrigin;
            searchRoutine = StartCoroutine(SearchDelay());
        }

        void Start()
        {
            foreach(Transform patrolPoint in patrolPoints)
            {   
                patrolPoint.parent = null;
            }
            targetPlayer = FindAnyObjectByType<Player>();
            ragdoll = GetComponent<RagdollController>();
        }

        void Update()
        {
            if(Detect(targetPlayer))
            {
                //increase suspiction
                GameManager.Instance.IncreaseSuspition();
                Alert(targetPlayer.detectionTarget.position);
            }


            switch(state)
            {
                case EnemyState.PATROL:
                    //rotate towards active patrol point
                    RotateTowards(patrolPoints[patrolTarget].position);
                    //move forward
                    transform.position += transform.forward * patrolSpeed * Time.deltaTime;
                    if(Vector3.Distance(transform.position, patrolPoints[patrolTarget].position) < patrolPointLenience)
                    {
                        // reached patrol point -> update
                        patrolTarget ++;
                        if (patrolTarget >= patrolPoints.Length)
                        {
                            patrolTarget = 0;
                        }
                    }
                break;

                case EnemyState.SEARCH:

                    if(Vector3.Distance(transform.position, patrolPoints[patrolTarget].position) > patrolPointLenience)
                    {
                        RotateTowards(POI);
                        //move forward
                        transform.position += transform.forward * searchSpeed * Time.deltaTime;
                    }
                break;
            }
        }


        private Coroutine searchRoutine;
        IEnumerator SearchDelay()
        {
            yield return new WaitForSeconds(searchTime);
            state = EnemyState.PATROL;
        }

        private void RotateTowards(Vector3 target)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.forward, target - transform.position);
            Quaternion clampedRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, clampedRotation, rotateSpeed*Time.deltaTime);
        }

        public void TryTakeDown(Vector3 origin)
        {
            //if behind us
            Vector3 direction = origin - transform.position;
            //ignore vertical distance
            direction = new Vector3(direction.x, 0, direction.z).normalized;
            if(Vector3.Angle(direction, transform.forward) > 180 - takeDownLieniency)
            {
                Ragdoll(origin, 0.1f);
                GameManager.Instance.CheckEnemies();
            }
        }

        public void Ragdoll(Vector3 origin, float force)
        {
            Kill();
            ragdoll.Activate(true);
            ragdoll.AddForceFromOrigin(origin, force);
        }

        public void Kill()
        {
            Debug.Log("ded");
        }

        enum EnemyState
        {
            PATROL,
            SEARCH
        }

        void OnDrawGizmos()
        {
            for(int i = 0; i < patrolPoints.Length - 1; i++)
            {   
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(patrolPoints[i].position, 0.5f);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i+1].position);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(patrolPoints[^1].position, 0.5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(patrolPoints[^1].position, patrolPoints[0].position);
            
        }

    }
}

