using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text finalTimeText;
    public GameObject endGamePanel;
    public Slider noiseSlider;

    float elapsedTime = 0f;
    bool timerRunning = true;
    bool gameEnded = false;

    void Start()
    {
        endGamePanel.SetActive(false);
    }

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void CheckNoiseMeter()
    {
        if (!gameEnded && noiseSlider.value >= noiseSlider.maxValue)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        timerRunning = false;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        finalTimeText.text = "Time Taken: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        endGamePanel.SetActive(true);
    }
}