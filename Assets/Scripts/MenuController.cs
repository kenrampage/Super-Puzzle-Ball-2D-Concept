using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPB;
using UnityEngine.SceneManagement;

namespace SPB
{
    public class MenuController : MonoBehaviour

    {
        public bool gameIsPaused = false;
        public GameObject pauseMenuUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    ResumeGame();

                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("isGamePaused", false);

        }

        public void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
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
            gameIsPaused = false;
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Debug.Log("Player has quit the game");
            Application.Quit();

        }
    }
}