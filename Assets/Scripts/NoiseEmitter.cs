using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoiseEmitter : MonoBehaviour
{
    [Header("Audio (The Sound You Hear)")]
    public float maxImpactVelocity = 3.5f;
    public float globalVolumeBoost = 5f;
    public float baselineVolume = 0.001f;
    public float impactWeight = 1.2f;
    public AudioClip impactClip;

    [Header("Noise Meter (The Bar Scaling)")]
    [Range(0.1f, 1.0f)]
    public float noiseMeterSensitivity = 0.3f; // Lower = Less sensitive bar
    public float maxNoisePerImpact = 15f;      // Caps how much one drop can add

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

        if (Time.time < lastNoiseTime + cooldown || impact < 0.001f)
            return;

        // --- PART 1: AUDIO CALCULATION (Keep this for the sound) ---
        float normalizedImpact = Mathf.Clamp01(impact / maxImpactVelocity);
        float impactVolume = Mathf.Pow(normalizedImpact, 3f) * impactWeight;
        float finalVolume = (baselineVolume + impactVolume) * globalVolumeBoost;
        finalVolume = Mathf.Clamp(finalVolume, 0.05f, 1.2f);

        // Play the sound at the original volume
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.PlayOneShot(impactClip, finalVolume);

        // --- PART 2: METER CALCULATION (Separate Logic) ---
        // We use the raw impact but multiply it by your new sensitivity
        float noiseAmount = impact * 10f * noiseMeterSensitivity;

        // Safety cap: No single impact can fill too much of the bar at once
        noiseAmount = Mathf.Clamp(noiseAmount, 0f, maxNoisePerImpact);

        if (DisturbanceManager.Instance != null)
        {
            DisturbanceManager.Instance.AddNoise(noiseAmount);
        }

        lastNoiseTime = Time.time;
    }
}