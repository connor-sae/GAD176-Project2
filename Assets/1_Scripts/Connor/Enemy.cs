using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : VisionConeDetection, IAlertable
{
    public Collider playerCollider;

    public void Alert(Vector3 alertOrigin)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        Debug.Log(Detect(transform.position, playerCollider, 45, 4));
    }

}

