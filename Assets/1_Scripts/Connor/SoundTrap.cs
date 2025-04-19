using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrap : Trap
{
    [SerializeField] float alertDistance = 5f;
    protected override void OntrapTriggered()
    {
        //use sphere overlap to alert all nearby alertables
        Collider[] overlaps = Physics.OverlapSphere(transform.position, alertDistance);
        foreach(Collider overlap in overlaps)
        {
            if(TryGetComponent<IAlertable>(out IAlertable toAlert))
            {
                toAlert.Alert(transform.position);
            }
        }
    
        base.OntrapTriggered();
    }
}
