using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SPB;
using UnityEngine.SceneManagement;

namespace SPB
{

    public class UIManager : MonoBehaviour
    {

        public TextMeshProUGUI levelText;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI targetsText;

        public GameManager gameManager;

        public GameObject pauseMenuUI;
        public GameObject startMenuUI;
        public GameObject startOptionsMenuUI;
        public GameObject levelEndMenuUI;
        public GameObject gameEndMenuUI;
        public GameObject gameUI;

        // Update is called once per frame
        void Start()
        {
            levelText.text = "Level " + GameManager.level.ToString();
            SetMenuStates();

        }
        private void Update()
        {
            timerText.text = "Timer " + GameManager.levelTimer.ToString("F2");
            targetsText.text = "Targets " + gameManager.targetsRemaining;
        }

        // Set Menu States at start
        public void SetMenuStates()
        {
            if (GameManager.level == 0)
            {
                startMenuUI.SetActive(true);
                startOptionsMenuUI.SetActive(false);
                pauseMenuUI.SetActive(false);
                levelEndMenuUI.SetActive(false);
                gameUI.SetActive(false);

            }
            else if (GameManager.level > 0)
            {
                startMenuUI.SetActive(false);
                startOptionsMenuUI.SetActive(false);
                pauseMenuUI.SetActive(false);
                levelEndMenuUI.SetActive(false);
                gameUI.SetActive(true);
            }
        }

        public void StartMenu(bool state)
        {
            //bool state = startMenuUI.gameObject.activeSelf;
            startMenuUI.SetActive(state);
        }

        public void PauseMenu(bool state)
        {
            //bool state = startMenuUI.gameObject.activeSelf;
            pauseMenuUI.SetActive(state);
        }

        public void LevelEndMenu(bool state)
        {
            //bool state = startMenuUI.gameObject.activeSelf;
            levelEndMenuUI.SetActive(state);
        }

        public void GameUI(bool state)
        {
            gameUI.SetActive(state);
        }

        public void GameEndMenu(bool state)
        {
            //bool state = startMenuUI.gameObject.activeSelf;
            gameEndMenuUI.SetActive(state);
        }
    }

}
