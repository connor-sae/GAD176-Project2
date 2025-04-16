using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] GameObject[] pathways;

        public RoomGroup activeRoomGroup;

        public static GenerationManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            //generate a random pathway from prefabs
            int randomPathway = Random.Range(0, pathways.Length);
            GameObject generatedPathway = Instantiate(pathways[randomPathway], transform);
            //pathways automatically generate rooms and props if they have the room/prop generator script respectivly
        }


    }
}
