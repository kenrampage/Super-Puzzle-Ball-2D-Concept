using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPB;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace SPB
{
    public class MenuManager : MonoBehaviour


    {
        public GameObject pauseMenuUI;
        public GameInput controls;

        private void Awake()
        {
            controls = new GameInput();
            controls.Player.Menu.performed += context => GameStateChanger();
        }

        private void Update()
        {

        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public void ResumeGame()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", false);

        }

        public void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.gameIsPaused = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", true);
        }


        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitToMenu()
        {
            Debug.Log("Player has quit to the menu");
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Debug.Log("Player has quit the game");
            Application.Quit();


        }

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

        private void TestOutput(string content)
        {
            Debug.Log("The Output is :" + content);
        }
    }
}