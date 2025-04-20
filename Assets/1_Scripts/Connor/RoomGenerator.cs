using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public class RoomGenerator : MarkerItemGenerator
    {


        void Awake()
        {
            Generate(GenerationManager.Instance.activeRoomGroup.rooms);
        }
    }
}
