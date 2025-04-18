using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IKillable>(out IKillable triggerer))
        {
            OntrapTriggered(triggerer);
        }
    }

    protected virtual void OntrapTriggered(IKillable triggerer)
    {
        Debug.Log($"{name} Triggered");
    }
}
