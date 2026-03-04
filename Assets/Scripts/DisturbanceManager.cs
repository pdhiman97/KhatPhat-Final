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
    public float maxNoise = 100f;
    public float decayRate = 2f;

    [Header("Uncle State Flags")]
    private bool hasCoughed = false;
    private bool hasMumbled = false;
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
        if (noiseBar != null) noiseBar.value = currentNoise;
    }

    void CheckThresholds()
    {
        float percent = (currentNoise / maxNoise) * 100f;

        if (percent >= 100f)
        {
            TriggerWakeUp();
        }
        else if (percent >= 75f && !hasMumbled)
        {
            Debug.Log("Uncle: 'Kaun hai re?'");
            hasMumbled = true;
        }
        else if (percent >= 50f && !hasCoughed)
        {
            Debug.Log("Uncle: *Coughs and shifts*");
            hasCoughed = true;
        }

        if (percent < 40f) hasCoughed = false;
        if (percent < 65f) hasMumbled = false;
    }

    void TriggerWakeUp()
    {
        isGameOver = true;
        Debug.Log("UNCLE IS AWAKE!");

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