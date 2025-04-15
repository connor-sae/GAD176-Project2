using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MarkerItemGenerator
{
    void Start()
    {
        Generate(GenerationManager.Instance.activeRoomGroup.props);
    }
}
