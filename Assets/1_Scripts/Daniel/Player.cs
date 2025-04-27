using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class Player : MonoBehaviour
    {
        public bool silentBoots;
        public bool trapImmunity;
        // Start is called before the first frame update
        void Start()
        {
            silentBoots = false;
            trapImmunity = false;        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
