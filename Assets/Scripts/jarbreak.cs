using UnityEngine;

public class JarBreak : MonoBehaviour
{
    public GameObject crackedJar;
    public AudioClip breakSound;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(breakSound, transform.position);

        crackedJar.SetActive(true);

        Rigidbody[] bodies = crackedJar.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
        }

        gameObject.SetActive(false);
    }
}
