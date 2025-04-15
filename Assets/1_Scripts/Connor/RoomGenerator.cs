using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MarkerItemGenerator
{


    void Start()
    {
        Generate(GenerationManager.Instance.activeRoomGroup.rooms);
    }
}
