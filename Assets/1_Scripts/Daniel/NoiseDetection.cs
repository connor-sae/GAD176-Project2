using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{

    public class NoiseDetection : MonoBehaviour
    {
        [SerializeField] private Transform playerPos;
        [SerializeField] private float hearingRadius = 5f;  
        [SerializeField] private float moveThreshold = 0.1f;

        [SerializeField] private Player player;

        private CharacterController playerController;

        void Start()
        {
            player = FindObjectOfType<Player>();

            if (playerPos == null)
                playerPos = GameObject.FindGameObjectWithTag("Player").transform;

            playerController = playerPos.GetComponent<CharacterController>();
            if (playerController == null)
                Debug.LogError("Player needs a CharacterController!");
        }

        void Update()
        {        
            if (playerController.velocity.magnitude < moveThreshold)
                return;

            float distance = Vector3.Distance(transform.position, playerPos.position);
            if (distance > hearingRadius)
                return;

            if (player.silentBoots == false)
            {
                Vector3 lookPos =(playerPos.position);
                lookPos.y = transform.position.y;        
                transform.LookAt(lookPos);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, hearingRadius);
        }
    } 
}