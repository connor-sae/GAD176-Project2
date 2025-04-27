using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielGAD176
{
    public class TrapImmunity : PowerItems
    {
        // Start is called before the first frame update
        new void Start()
            {
                base.Start();
            }

        protected override void OnTriggerEnter(Collider other)
        {
            print("You picked up Trap Immunity");
            player.trapImmunity = true;
            base.OnTriggerEnter(other);
        }


    }

}
