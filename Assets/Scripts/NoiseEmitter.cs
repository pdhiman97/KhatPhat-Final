using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoiseEmitter : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public float silentThreshold = 0.001f; // Set this to 0.001 in Inspector
    public float maxImpactVelocity = 3.5f;
    public float globalVolumeBoost = 5f;

    [Header("Aesthetic Settings")]
    public float baselineVolume = 0.01f;
    public float impactWeight = 1.2f;

    [Header("Audio Assets")]
    public AudioClip impactClip;

    private AudioSource audioSource;
    private float cooldown = 0.05f;
    private float lastNoiseTime;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        if (Time.time < lastNoiseTime + cooldown)
            return;

        // Catch almost everything
        if (impact < silentThreshold)
            return;

        // 1. Calculate Volume
        float normalizedImpact = Mathf.Clamp01(impact / maxImpactVelocity);
        float impactVolume = Mathf.Pow(normalizedImpact, 3f) * impactWeight;
        float finalVolume = (baselineVolume + impactVolume) * globalVolumeBoost;

        // 2. GUARANTEE MINIMUM SOUND (Audio Floor)
        finalVolume = Mathf.Clamp(finalVolume, 0.05f, 1.2f);

        // 3. GUARANTEE MINIMUM METER IMPACT (Meter Floor)
        // Even at 0 impact, it adds +2 points to the meter
        float noiseAmount = (finalVolume * 30f) + 2f;

        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.PlayOneShot(impactClip, finalVolume);

        if (DisturbanceManager.Instance != null)
        {
            DisturbanceManager.Instance.AddNoise(noiseAmount);
        }

        lastNoiseTime = Time.time;
    }
}