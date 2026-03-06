using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisturbanceManager : MonoBehaviour
{
    public static DisturbanceManager Instance;

    [Header("UI Reference")]
    public Slider noiseBar;

    [Header("Logic Settings")]
    public float currentNoise = 0f;
    public float maxNoise = 180f;
    public float decayRate = 2f;

    [Header("Audio Settings")]
    public AudioSource ambientSource; // Drag your background/cricket sound here
    public AudioSource taujiVoice;
    public AudioClip clip35, clip70, clip100;

    [Header("Uncle State Flags")]
    private bool has35 = false;
    private bool has70 = false;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        if (currentNoise > 0)
        {
            currentNoise -= decayRate * Time.deltaTime;
            currentNoise = Mathf.Clamp(currentNoise, 0, maxNoise);
            UpdateUI();
        }
    }

    public void AddNoise(float amount)
    {
        if (isGameOver) return;

        currentNoise += amount;
        currentNoise = Mathf.Clamp(currentNoise, 0, maxNoise);
        UpdateUI();
        CheckThresholds();
    }

    void UpdateUI()
    {
        if (noiseBar != null)
        {
            noiseBar.value = currentNoise;
        }
    }

    void CheckThresholds()
    {
        float percent = (currentNoise / maxNoise) * 100f;

        if (percent >= 100f)
        {
            if (!isGameOver)
            {
                StopAmbientAndPlayGameOver();
            }
            TriggerWakeUp();
        }
        else if (percent >= 70f && !has70)
        {
            PlayVoice(clip70);
            has70 = true;
            has35 = true;
        }
        else if (percent >= 35f && !has35)
        {
            PlayVoice(clip35);
            has35 = true;
        }

        if (percent < 25f) has35 = false;
        if (percent < 60f) has70 = false;
    }

    void PlayVoice(AudioClip clip)
    {
        if (taujiVoice != null && clip != null)
        {
            taujiVoice.Stop();
            taujiVoice.clip = clip;
            taujiVoice.Play();
        }
    }

    void StopAmbientAndPlayGameOver()
    {
        // Stop the background yard sounds
        if (ambientSource != null && ambientSource.isPlaying)
        {
            ambientSource.Stop();
        }

        // Play the "Caught" Game Over audio
        PlayVoice(clip100);
    }

    void TriggerWakeUp()
    {
        isGameOver = true;
        if (noiseBar != null)
        {
            Image fillImage = noiseBar.fillRect.GetComponent<Image>();
            if (fillImage != null) fillImage.color = Color.red;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}