using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : DictionaryItemGenerator
{
    void Start()
    {
        Generate(GenerationManager.Instance.activeRoomGroup.rooms);
    }
}
