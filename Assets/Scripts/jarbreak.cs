using UnityEngine;

public class JarBreak : MonoBehaviour
{
    public GameObject crackedJar;
    public AudioClip breakSound;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(breakSound, transform.position);

        // IMPACT NOISE METER:
        // Adds 75 points to instantly hit the 75% threshold
        if (DisturbanceManager.Instance != null)
        {
            DisturbanceManager.Instance.AddNoise(75f);
        }

        crackedJar.SetActive(true);

        Rigidbody[] bodies = crackedJar.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
        }

        gameObject.SetActive(false);
    }
}