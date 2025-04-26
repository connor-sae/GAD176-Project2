using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDetection : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float hearingRadius = 5f;  
    [SerializeField] private float moveThreshold = 0.1f;

    private CharacterController playerController;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        playerController = player.GetComponent<CharacterController>();
        if (playerController == null)
            Debug.LogError("Player needs a CharacterController!");
    }

    void Update()
    {        
        if (playerController.velocity.magnitude < moveThreshold)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > hearingRadius)
            return;

        Vector3 lookPos =(player.position);
        lookPos.y = transform.position.y;        
        transform.LookAt(lookPos);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);
    }
} 