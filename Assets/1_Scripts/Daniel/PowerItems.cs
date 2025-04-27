using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class PowerItems : MonoBehaviour
    {
        [SerializeField] protected Player player;
        protected virtual void Start()
        {
            player = FindObjectOfType<Player>();
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                print("you picked up a power up");
                Destroy(gameObject);
            }
        }
    }

}
