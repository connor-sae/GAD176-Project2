using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{

    public class Player : MonoBehaviour
    {

        [Header("Movement")]
        public float walkSpeed = 3;
        public float crouchSpeed = 1.5f;
        bool crouching;

        [Header("Looking")]
        public Transform cameraHolder;
        public float sensitivty = 10;
        public float maxLookVertical = 90;
        public float minLookVertical = -90;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Movement();
            Look();
        }

        void Movement()
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            float speed;

            if (crouching)
                speed = crouchSpeed;
            else
                speed = walkSpeed;
            Vector3 localMovement = transform.forward * movement.z + transform.right * movement.x;

            transform.position += localMovement * speed * Time.deltaTime;
        }

        void Look()
        {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivty * Time.deltaTime;

            float currentXAngle = cameraHolder.transform.rotation.eulerAngles.x;
            float currentYAngle = transform.rotation.eulerAngles.y;

            if (currentXAngle >= 180)
                currentXAngle -= 360;

            float lookHorizontal = currentYAngle + mouseMovement.x;
            float lookVertical = Mathf.Clamp(currentXAngle - mouseMovement.y, minLookVertical, maxLookVertical);

            transform.rotation = Quaternion.Euler(0, lookHorizontal, 0);
            cameraHolder.localRotation = Quaternion.Euler(lookVertical, 0, 0);
        }
    }

}
