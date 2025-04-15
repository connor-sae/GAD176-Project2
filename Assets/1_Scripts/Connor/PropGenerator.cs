using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : DictionaryItemGenerator
{
    void Start()
    {
        Generate(GenerationManager.Instance.activeRoomGroup.props);
    }
}
