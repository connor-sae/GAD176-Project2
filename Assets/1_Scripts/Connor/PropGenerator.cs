using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public class PropGenerator : MarkerItemGenerator
    {
        protected override void Awake()
        {
            base.Awake();
            Generate(GenerationManager.Instance.activeRoomGroup.props);
        }
    }
}
