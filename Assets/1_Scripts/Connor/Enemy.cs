using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : VisionConeDetection, IAlertable
{
    public Collider playerCollider;

    [SerializeField] private Transform[] patrolPoints;
    private int patrolTarget;
    private EnemyState state;
    private float patrolPointLenience = 0.4f;

    private Vector3 POI;

    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float searchSpeed = 2f;

    [SerializeField] private float searchTime = 5f;

    
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
    }

    void Update()
    {
        if(Detect(playerCollider))
        {
            //increase suspiction
            Alert(playerCollider.transform.position);
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
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

