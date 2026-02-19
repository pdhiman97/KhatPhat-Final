using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoiseEmitter : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public float silentThreshold = 0.001f;
    public float maxImpactVelocity = 3.5f;
    public float globalVolumeBoost = 5f;

    [Header("Aesthetic Settings")]
    // This is the "very light sound" that plays for even the tiniest nudge
    public float baselineVolume = 0.001f;
    // How much the impact "adds" to the base volume
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

        if (impact < silentThreshold)
            return;

        // 1. Start with the "Very Light" default sound
        float finalVolume = baselineVolume;

        // 2. Add impact-based volume on top
        float normalizedImpact = Mathf.Clamp01(impact / maxImpactVelocity);

        // We use a Power of 3 to make the "loud" sounds only happen on big drops
        float impactVolume = Mathf.Pow(normalizedImpact, 3f) * impactWeight;

        // 3. Combine them and apply the boost
        finalVolume = (finalVolume + impactVolume) * globalVolumeBoost;

        // Final clamp so it doesn't get distorted
        finalVolume = Mathf.Clamp(finalVolume, 0.05f, 1.2f);

        // DISTURBANCE LOGIC:
        // Small baseline nudges (0.05) will barely move the bar (adds ~1.5 points)
        float noiseAmount = finalVolume * 30f;

        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.PlayOneShot(impactClip, finalVolume);

        if (DisturbanceManager.Instance != null)
        {
            DisturbanceManager.Instance.AddNoise(noiseAmount);
        }

        lastNoiseTime = Time.time;
    }
}