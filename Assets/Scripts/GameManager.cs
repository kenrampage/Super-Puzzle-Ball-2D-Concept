using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPB;
using UnityEngine.SceneManagement;

namespace SPB
{
    // Handles persistent data between levels
    public class GameManager : MonoBehaviour
    {

        public bool gameIsPaused = false;
        public bool levelIsComplete = false;
        public int targetsRemaining;
        public static int level;
        public GameObject environment;
        public MenuManager menuManager;

        public static float levelTimer;
        [HideInInspector] public float levelStartTime;

        private void Awake()
        {

        }

        private void Start()
        {
            levelStartTime = Time.time;
            Debug.Log("Level is: " + level);
        }

        private void Update()
        {
            targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;

            if (targetsRemaining > 0)
            {
                levelTimer = Time.time - levelStartTime;
            }
            else if (targetsRemaining == 0 && SceneManager.GetActiveScene().name != "StartMenu")
            {
                
                menuManager.LevelEnd();
            }

        }

    }
}
