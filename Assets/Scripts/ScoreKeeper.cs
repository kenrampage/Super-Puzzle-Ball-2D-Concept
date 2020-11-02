using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SPB
{
    public class ScoreKeeper : MonoBehaviour
    {
        public static float level1Time;
        public static float level2Time;
        public static float level3Time;
        public static float level4Time;
        public static float level5Time;

        public GameObject levelEndMenuUI;

        public void UpdateScoreText()
        {
            if (GameManager.level == 1)
            {
                level1Time = GameManager.levelTimer;
                level2Time = 0;
                level3Time = 0;
                level4Time = 0;
                level5Time = 0;
            }
            else if (GameManager.level == 2)
            {
                level2Time = GameManager.levelTimer;
            }
            else if (GameManager.level == 3)
            {
                level3Time = GameManager.levelTimer;
            }
            else if (GameManager.level == 4)
            {
                level4Time = GameManager.levelTimer;
            }
            else if (GameManager.level == 5)
            {
                level5Time = GameManager.levelTimer;
            }


        }

        private void Update()
        {
            levelEndMenuUI.transform.Find("Level" + 1 + "Time").GetComponent<TextMeshProUGUI>().text = "Level " + 1 + ": " + level1Time.ToString("F2") + " seconds";
            levelEndMenuUI.transform.Find("Level" + 2 + "Time").GetComponent<TextMeshProUGUI>().text = "Level " + 2 + ": " + level2Time.ToString("F2") + " seconds";
            levelEndMenuUI.transform.Find("Level" + 3 + "Time").GetComponent<TextMeshProUGUI>().text = "Level " + 3 + ": " + level3Time.ToString("F2") + " seconds";
            levelEndMenuUI.transform.Find("Level" + 4 + "Time").GetComponent<TextMeshProUGUI>().text = "Level " + 4 + ": " + level4Time.ToString("F2") + " seconds";
            levelEndMenuUI.transform.Find("Level" + 5 + "Time").GetComponent<TextMeshProUGUI>().text = "Level " + 5 + ": " + level5Time.ToString("F2") + " seconds";

        }
    }
}
