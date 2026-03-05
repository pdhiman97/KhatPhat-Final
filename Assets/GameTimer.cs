using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 180f; // 3 minutes

    public TMP_Text timerText;
    public Image fadePanel;
    public GameObject endGamePanel;
    public AudioSource childrenAudio;
    public Slider noiseMeter;

    bool gameEnded = false;

    void Start()
    {
        UpdateTimerDisplay();

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }

        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {
        if (gameEnded) return;

        // Timer countdown
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }

        // Condition 1: Timer ends
        if (timeRemaining <= 0 && !gameEnded)
        {
            StartCoroutine(EndSequence());
        }

        // Condition 2: Noise meter full
        if (noiseMeter != null && noiseMeter.value >= noiseMeter.maxValue && !gameEnded)
        {
            StartCoroutine(EndSequence());
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator EndSequence()
    {
        gameEnded = true;

        // Fade screen
        float duration = 2f;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = t / duration;

            if (fadePanel != null)
                fadePanel.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        // Play children ambience
        if (childrenAudio != null)
            childrenAudio.Play();

        // Wait before showing end screen
        yield return new WaitForSeconds(2f);

        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        // Stop gameplay
        Time.timeScale = 0f;
    }

    // Restart button function
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene"); // replace with your start scene name
    }
}