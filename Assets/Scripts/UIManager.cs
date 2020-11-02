using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SPB
{

    public class UIManager : MonoBehaviour
    {

        public TextMeshProUGUI levelText;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI targetsText;

        public GameManager gameManager;

        // Update is called once per frame
        void Start()
        {
            transform.Find("GameUI").gameObject.SetActive(true);
            levelText.text = "Level " + GameManager.level.ToString();
        }
        private void Update()
        {
            timerText.text = "Timer " + GameManager.levelTimer.ToString("F2");
            targetsText.text = "Targets " + gameManager.targetsRemaining;
        }
    }
}
