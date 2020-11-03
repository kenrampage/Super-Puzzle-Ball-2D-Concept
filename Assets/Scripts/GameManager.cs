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
        public bool levelIsComplete;
        public int targetsRemaining;
        public static int level = 0;
        public static bool gameRunning = false;
        public GameObject environment;
        public UIManager UIManager;
        public ScoreKeeper scoreKeeper;
        public GameObject player;

        public static float levelTimer;
        [HideInInspector] public float levelStartTime;

        public GameInput controls;

        public int levelToTest;
        public bool testLevels;
        public bool progressTestLevels;

        private void Awake()
        {
            // Set variable as new instance of the InputActions
            controls = new GameInput();

            // Subscribes to events and directs output
            controls.Player.Menu.performed += context => GameStateChanger();

            setLevelStates();

        }

        private void Start()
        {
            levelStartTime = Time.time;
            Debug.Log("Level is: " + level);
            gameIsPaused = false;
            levelIsComplete = false;
            Time.timeScale = 1f;

            if (level != 0)
            {
                player.gameObject.SetActive(true);
            }

        }

        private void Update()
        {
            targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;
            levelTimer = Time.time - levelStartTime;


            if (levelIsComplete)
            {
                LevelEnd();
            }

        }

        // Chooses method based on whether the game is paused
        public void GameStateChanger()
        {

            if (gameIsPaused)
            {
                ResumeGame();

            }
            else if (levelIsComplete)
            {

            }
            else
            {
                PauseGame();
            }

        }

        private void OnEnable()
        {
            // Enables input controls
            controls.Enable();
        }


        private void OnDisable()
        {
            // Disables input controls
            controls.Disable();
        }


        // When the game resumes
        public void ResumeGame()
        {
            UIManager.PauseMenu(false);

            Time.timeScale = 1f;
            gameIsPaused = false;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", false);
        }

        // When the game pauses
        public void PauseGame()
        {
            UIManager.PauseMenu(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", true);
        }

        // Loading game scenes
        public void StartGame()
        {
            //GameManager.level = 1;
            gameRunning = true;
            gameIsPaused = false;
            levelIsComplete = false;
            SceneManager.LoadScene("Game");

        }

        public void LevelEnd()
        {
            scoreKeeper.UpdateScoreText();
            scoreKeeper.setTimerText();
            UIManager.GameUI(false);
            Time.timeScale = 0f;
            gameIsPaused = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", true);

            if (GameManager.level == 5)
            {
                UIManager.GameEndMenu(true);
            }
            else
            {
                UIManager.LevelEndMenu(true);
            }

        }

        public void StartNextLevel()
        {

            UIManager.LevelEndMenu(false);
            UIManager.GameEndMenu(false);

            //UIManager.transform.Find("GameUI").gameObject.SetActive(true);
            SceneManager.LoadScene("Game");
        }

        // Quit to main menu
        public void QuitToMenu()
        {
            Debug.Log("Player has quit to the menu");
            UIManager.LevelEndMenu(false);
            UIManager.GameEndMenu(false);
            gameRunning = false;
            SceneManager.LoadScene("Game");
        }

        // Exit the game
        public void QuitGame()
        {
            Debug.Log("Player has quit the game");
            Application.Quit();

        }

        public void setLevelStates()
        {
            if (gameRunning)
            {
                GameManager.level += 1;
            }
            else if (!gameRunning && testLevels && progressTestLevels)
            {
                GameManager.level = levelToTest;
                gameRunning = true;
            }
            else if (!gameRunning && testLevels)
            {
                GameManager.level = levelToTest;
            }
            else
            {
                GameManager.level = 0;
                scoreKeeper.ResetTimers();
            }
        }

    }
}
