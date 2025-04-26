using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class SpikeTrap : Traps
    {
        protected override void OnTriggerEnter(Collider other)
        {
            // Destroys the trap when it is triggered
            base.OnTriggerEnter(other);
            if (other.CompareTag("Player"))
            {
                //placeholder to trigger animation or UI
                Debug.Log("YOU DIED!!!");
            }
            if (other.CompareTag("Enemy"))
            {
                //placeholder to trigger animation or UI
                Destroy(other.gameObject);
            }
        }
    }

}
