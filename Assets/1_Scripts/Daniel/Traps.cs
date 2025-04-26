using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class Traps : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            if (other.CompareTag("Player"))
            {
                Debug.Log("you died");
                Destroy(this.gameObject);
            }
        }
    }

}
