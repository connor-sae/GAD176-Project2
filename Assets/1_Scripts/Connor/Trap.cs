using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] bool destroyOnTrigger;
    [SerializeField] float triggerDelay;
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IKillable>() != null)
        {
            Invoke("OntrapTriggered", triggerDelay);
        }
    }


    /// <summary>
    /// Called when an IKillable object enters the trigger volume and destroys the Tap if destroy on trigger is true
    /// </summary>
    /// <param name="triggerer">the Ikillable that enteres the trigger volume</param>
    protected virtual void OntrapTriggered()
    {
        Debug.Log($"{name} Triggered");
        if(destroyOnTrigger) Destroy(gameObject);
    }
}
