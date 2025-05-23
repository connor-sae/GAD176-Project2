using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class Traps : MonoBehaviour
    {
        [SerializeField] BoxCollider BoxCollider;
        [SerializeField] protected Player player;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            BoxCollider = this.GetComponent<BoxCollider>();
            player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            BoxCollider.enabled = false;
        }
    }

}
