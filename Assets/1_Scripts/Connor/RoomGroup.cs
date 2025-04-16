using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{

    [CreateAssetMenu(fileName = "New Room Group", menuName = "Room Group")]
    public class RoomGroup : ScriptableObject
    {
        public Dictionary<string, GameObject[]> rooms;
        public Dictionary<string, GameObject[]> props;

        [SerializeField]
        Type2GameOjectPair[] roomsDictionaryAssignment;
        [SerializeField]
        Type2GameOjectPair[] propsDictionaryAssignment;

        //serializeable string Game object pair to compile item Dictionary
        [Serializable]
        class Type2GameOjectPair
        {
            public string type;
            public GameObject[] gameObjects;
        }

        private void OnEnable()
        {
            //compile Dictionaries
            rooms = new();
            if (roomsDictionaryAssignment != null)
                foreach (Type2GameOjectPair roomPairing in roomsDictionaryAssignment)
                {
                    rooms.Add(roomPairing.type.ToLower(), roomPairing.gameObjects);
                }

            props = new();
            if (propsDictionaryAssignment != null)
                foreach (Type2GameOjectPair propPairing in propsDictionaryAssignment)
                {
                    props.Add(propPairing.type.ToLower(), propPairing.gameObjects);
                }

        }
    }
}


