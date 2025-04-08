using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{

    public class Player : MonoBehaviour
    {

        [Header("Movement")]
        public Transform playerRotator;
        public float rotateSpeed = 180;
        public float walkSpeed = 3;
        public float crouchSpeed = 1.5f;
        bool crouching;

        [Header("Looking")]
        public Transform cameraHolder;
        public float sensitivty = 10;
        public float maxLookVertical = 90;
        public float minLookVertical = -90;
        float cameraYAngle;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Look();
            Movement();
        }

        void Movement()
        {
            Vector2 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            float speed;

            if (crouching)
                speed = crouchSpeed;
            else
                speed = walkSpeed;
            if (movement.magnitude > 0)
            {
                float movementAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
                Quaternion desiredRotation = Quaternion.Euler(0, movementAngle + cameraYAngle, 0);

                playerRotator.transform.rotation = Quaternion.RotateTowards(playerRotator.transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);

                transform.position += playerRotator.transform.forward * speed * Time.deltaTime;
            }
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
    }

}
