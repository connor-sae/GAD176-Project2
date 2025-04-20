using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace GAD176.Connor
{

    
    [RequireComponent(typeof(RagdollController))]
    public class Player : MonoBehaviour, IRagdoll
    {

        [Header("Movement")]
        [SerializeField] private Transform playerRotator;
        [SerializeField] private Rigidbody activeRB;
        public Collider activeCollider;
        [SerializeField] private float rotateSpeed = 180;
        [SerializeField] private float walkSpeed = 3;
        [SerializeField] private float crouchSpeed = 1.5f;
        bool crouching;

        [Header("Looking")]
        [SerializeField] private Transform cameraHolder;
        [SerializeField] private CinemachineVirtualCamera crouchCamera;
        [SerializeField] private float sensitivty = 10;
        [SerializeField] private float maxLookVertical = 90;
        [SerializeField] private float minLookVertical = -90;
        float cameraYAngle;

        [Header("Other")]
        public Animator playerAnimater;
        private RagdollController ragdoll;
        [SerializeField] float walkAlertRange = 1.5f;
        [SerializeField] float takeDownDistance = 0.3f;
        [SerializeField] Transform takeDownOrigin;
        public Transform detectionTarget;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            ragdoll = GetComponent<RagdollController>();
        }

        void Update()
        {
            Look();
            Movement();
            
            //try takedown
            if(Input.GetMouseButtonDown(0))
            {
                Collider[] overlaps = Physics.OverlapSphere(takeDownOrigin.position, takeDownDistance);
                foreach(Collider overlap in overlaps)
                {
                    overlap.GetComponent<Enemy>()?.TryTakeDown(takeDownOrigin.position);
                }
            }
        }

        void Movement()
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            float speed;

            crouching = Input.GetKey(KeyCode.LeftControl);

            if (crouching)
            {
                speed = crouchSpeed;
                crouchCamera.Priority = 15;
            }
            else
            {
                speed = walkSpeed;
                crouchCamera.Priority = 5;
            }

                
            if (movement.magnitude > 0)
            {
                float movementAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
                Quaternion desiredRotation = Quaternion.Euler(0, movementAngle + cameraYAngle, 0);

                playerRotator.transform.rotation = Quaternion.RotateTowards(playerRotator.transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);

                activeRB.velocity = playerRotator.transform.forward * speed;

                if(!crouching)
                    AlertEnemies(walkAlertRange);
            }

            playerAnimater.SetBool("Crouching", crouching);
            playerAnimater.SetFloat("Movement", movement.magnitude);
        }

        void Look()
        {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivty * Time.deltaTime;

            Vector2 currentRotation = cameraHolder.transform.rotation.eulerAngles;

            if (currentRotation.x >= 180)
                currentRotation.x -= 360;

            float lookHorizontal = currentRotation.y + mouseMovement.x;
            float lookVertical = Mathf.Clamp(currentRotation.x - mouseMovement.y, minLookVertical, maxLookVertical);

            cameraHolder.rotation = Quaternion.Euler(lookVertical, lookHorizontal, 0);

            cameraYAngle = lookHorizontal;
        }

        void AlertEnemies(float range)
        {
            //use sphere overlap to alert all nearby alertables
            //Debug.Log("Alerttting");
            Collider[] overlaps = Physics.OverlapSphere(transform.position, range);
            foreach(Collider overlap in overlaps)
            {
                
                if(overlap.TryGetComponent<IAlertable>(out IAlertable toAlert))
                {
                    toAlert.Alert(transform.position);
                }
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
    }

}
