using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitGate : MonoBehaviour
{
    [Header("Win Audio (The Speaker on the Gate)")]
    public AudioSource winSource;
    public AudioClip winClip;

    [Header("Ambient Audio (The Empty Object)")]
    public AudioSource ambientObjectSource;

    [Header("UI")]
    public GameObject winButton;   // Button that appears after winning

    private bool hasWon = false;
    private Collider gateCollider;

    void Awake()
    {
        gateCollider = GetComponent<Collider>();

        if (gateCollider != null)
        {
            gateCollider.enabled = true;
            gateCollider.isTrigger = true;
        }

        // Make sure button starts hidden
        if (winButton != null)
        {
            winButton.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !hasWon)
        {
            HandleWin();
        }
    }

    void HandleWin()
    {
        hasWon = true;

        Debug.Log("GOAL! You escaped with the ball!");

        // Stop ambient audio
        if (ambientObjectSource != null && ambientObjectSource.isPlaying)
        {
            ambientObjectSource.Stop();
        }

        // Play win audio
        if (winSource != null && winClip != null)
        {
            winSource.clip = winClip;
            winSource.Play();
        }

        // Show win button
        if (winButton != null)
        {
            winButton.SetActive(true);
        }
    }
}