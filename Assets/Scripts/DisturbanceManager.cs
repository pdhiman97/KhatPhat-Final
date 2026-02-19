using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required if you want to restart the level

public class DisturbanceManager : MonoBehaviour
{
    public static DisturbanceManager Instance;

    [Header("UI Reference")]
    public Slider noiseBar; // Drag your 'NoiseMeterBar' here in Inspector

    [Header("Logic Settings")]
    public float currentNoise = 0f;
    public float maxNoise = 100f;
    public float decayRate = 2f; // How fast the bar empties over time

    [Header("Uncle State Flags")]
    private bool hasCoughed = false;
    private bool hasMumbled = false;
    private bool isGameOver = false;

    void Awake()
    {
        // Singleton pattern to allow NoiseEmitters to find this script
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        // Naturally decay noise over time
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

        // 100%: Wake Up / Game Over
        if (percent >= 100f)
        {
            TriggerWakeUp();
        }
        // 75%: Gibberish / Mumbling
        else if (percent >= 75f && !hasMumbled)
        {
            Debug.Log("Uncle: 'Kaun hai re?' (Gibberish)");
            // Add Hindi Audio Clip play here later
            hasMumbled = true;
        }
        // 50%: Coughing / Shifting
        else if (percent >= 50f && !hasCoughed)
        {
            Debug.Log("Uncle: *Coughs and shifts on charpai*");
            hasCoughed = true;
        }

        // Reset flags if noise drops back down significantly
        if (percent < 40f) hasCoughed = false;
        if (percent < 65f) hasMumbled = false;
    }

    void TriggerWakeUp()
    {
        isGameOver = true;
        Debug.Log("UNCLE IS AWAKE! YOU GOT CAUGHT.");

        // Visual feedback: Change the bar color to solid Red
        if (noiseBar != null)
        {
            Image fillImage = noiseBar.fillRect.GetComponent<Image>();
            if (fillImage != null) fillImage.color = Color.red;
        }

        // Optional: Freeze the game so you know it's over
        // Time.timeScale = 0f; 
    }

    // Call this from a UI button to restart
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}