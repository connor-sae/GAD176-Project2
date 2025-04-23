using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public class RoomGenerator : MarkerItemGenerator
    {
        protected override void Awake()
        {
            base.Awake();
            Generate(GenerationManager.Instance.activeRoomGroup.rooms);
        }
    }
}
