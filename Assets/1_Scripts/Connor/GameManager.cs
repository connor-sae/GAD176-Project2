using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GAD176.Connor
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [HideInInspector] public float suspition = 0; 
        [SerializeField] float suspitionIncreaseRate = 0.15f;
        [SerializeField] float suspitionDecreaseRate = 0.05f;

        [SerializeField] private Slider suspicionMeter;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        private bool increaseSus;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }else
            {
                Destroy(gameObject);
            }
        }

        
        void OnEnable()
        {
            GameEvents.OnGameLose += LoseGame;
            GameEvents.OnGameWin += WinGame;
        }

        void OnDisable()
        {
            GameEvents.OnGameLose -= LoseGame;
            GameEvents.OnGameWin -= WinGame;
        }

        public void IncreaseSuspition()
        {
            increaseSus = true;
        }

        //late update to ensure it is called last
        void LateUpdate()
        {
            if(increaseSus)
            {
                suspition += suspitionIncreaseRate * Time.deltaTime;
                increaseSus = false;
                if(suspition >= 1)
                {
                    GameEvents.OnGameLose?.Invoke();
                }
            }
            else
                suspition -= suspitionDecreaseRate * Time.deltaTime;
            
            suspition = Mathf.Clamp01(suspition);

            suspicionMeter.value = suspition;
        }

        //End Game if there are no more enemies
        public void CheckEnemies()
        {
            int enemyCount = 0;
            foreach(Enemy enemy in FindObjectsOfType<Enemy>())
            {
                if(enemy.enabled)
                    enemyCount++;
            }
            Debug.Log(enemyCount);
            if(enemyCount <= 0)
                GameEvents.OnGameWin?.Invoke();
        }

        private void WinGame()
        {
            Cursor.lockState = CursorLockMode.None;
            winScreen.SetActive(true);
            
        }
        private void LoseGame()
        {
            Cursor.lockState = CursorLockMode.None;
            loseScreen.SetActive(true);
            
        }

    }

}
