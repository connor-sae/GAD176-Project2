using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class CaptureTrap : Traps
    {
        [SerializeField] float trapDuration;
        [SerializeField] GameObject Cage;
        
        new void Start()
        {
            base.Start();
            trapDuration = 3f;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if ((other.CompareTag("Player") && player.trapImmunity == false) || other.CompareTag("Enemy"))
            {
                StartCoroutine(CaptureCoroutine(other));
                base.OnTriggerEnter(other);
            }                      
        }

        IEnumerator CaptureCoroutine(Collider target)
        {
            Cage.SetActive(true);
            yield return new WaitForSeconds(trapDuration);
            Cage.SetActive(false);
        }
        
    }
}
