using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace DanielGAD176
{
    public class SilentBoots : PowerItems
    {
        protected override void Start()
        {
            base.Start();
        }
        protected override void OnTriggerEnter(Collider other)
        {
           print("You picked up Silent Boots");
           player.silentBoots = true;
           base.OnTriggerEnter(other);
        }
    }

}
