using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : Trap
{
    [SerializeField] private float explodeRadius = 2f;
    [SerializeField] private float explosiveForce;
    protected override void OntrapTriggered()
    {
        //use sphere overlap to explode all nearby IKillables
        Collider[] overlaps = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach(Collider overlap in overlaps)
        {
            if(TryGetComponent<IKillable>(out IKillable killable))
            {
                if(killable is IRagdoll)
                    (killable as IRagdoll).Ragdoll(transform.position, explosiveForce);
                else
                    killable.Kill();
            }
        }
        

        base.OntrapTriggered();
    }
}
