using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public class PropGenerator : MarkerItemGenerator
    {
        void Start()
        {
            Generate(GenerationManager.Instance.activeRoomGroup.props);
        }
    }
}
