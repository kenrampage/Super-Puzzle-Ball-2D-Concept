using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPB;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace SPB
{

    //Handles loading/unloading menus and methods for buttons to call
    public class MenuManager : MonoBehaviour

    {
        public GameObject pauseMenuUI;
        public GameObject startMenuUI;
        public GameObject startOptionsMenuUI;

        // Declare variable for InputAction asset
        public GameInput controls;

        private void Awake()
        {
            // Set variable as new instance of the InputActions
            controls = new GameInput();

            // Subscribes to events and directs output
            controls.Player.Menu.performed += context => GameStateChanger();
        }

        private void Start()
        {   
            // Sets menu visibility based on current scene
            SetMenuStates();
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
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", false);
        }

        // When the game pauses
        public void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.gameIsPaused = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", true);
        }

        // Loading game scenes
        public void LoadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Quit to main menu
        public void QuitToMenu()
        {
            Debug.Log("Player has quit to the menu");
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
            SceneManager.LoadScene(0);
        }

        // Exit the game
        public void QuitGame()
        {
            Debug.Log("Player has quit the game");
            Application.Quit();

        }

        // Chooses method based on whether the game is paused
        public void GameStateChanger()
        {

            if (GameManager.gameIsPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();
            }

        }

        // Set Menu States at start
        public void SetMenuStates()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                startMenuUI.SetActive(true);
                startOptionsMenuUI.SetActive(false);
                pauseMenuUI.SetActive(false);
            }
            else if (SceneManager.GetActiveScene().name == "Level_1")
            {
                startMenuUI.SetActive(false);
                startOptionsMenuUI.SetActive(false);
                pauseMenuUI.SetActive(false);
            }
        }

        // Test output for debugging
        private void TestOutput(string content)
        {
            Debug.Log("The Output is :" + content);
        }
    }
}