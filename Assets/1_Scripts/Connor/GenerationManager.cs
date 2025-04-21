using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GAD176.Connor
{
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] GameObject[] pathways;

        public RoomGroup activeRoomGroup;

        public static GenerationManager Instance;
        private GameObject generatedPathway;

        [SerializeField] bool generateForPlay = true;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            //generate a random pathway from prefabs
            int randomPathway = Random.Range(0, pathways.Length);
            generatedPathway = Instantiate(pathways[randomPathway], transform);
            //pathways automatically generate rooms and props if they have the room/prop generator script respectivly
            if(!generateForPlay)
            {
                foreach(Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
                {
                    Destroy(enemy.gameObject);
                }
                Destroy(FindAnyObjectByType<Player>().gameObject);
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void DestroyLevel()
        {
            Destroy(generatedPathway);
        }

    }
}
