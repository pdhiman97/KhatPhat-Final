using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 180f; // 3 minutes

    public TMP_Text timerText;
    public Image fadePanel;
    public GameObject endGamePanel;
    public AudioSource childrenAudio;

    bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
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

        float duration = 2f;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = t / duration;
            fadePanel.color = new Color(0,0,0,alpha);
            yield return null;
        }

        childrenAudio.Play();

        yield return new WaitForSeconds(2f);

        endGamePanel.SetActive(true);
    }
}