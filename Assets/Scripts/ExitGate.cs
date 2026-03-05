using UnityEngine;

public class ExitGate : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource winSource;
    public AudioClip winClip;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the player has entered the gate
        if (other.CompareTag("Player"))
        {
            // 2. Check if the player is holding the ball
            // This assumes your ball has the tag "Ball" 
            // and is a child of the player's hand or within the trigger
            if (IsHoldingBall())
            {
                PlayWinCondition();
            }
            else
            {
                Debug.Log("You reached the gate, but you forgot the ball!");
            }
        }
    }

    bool IsHoldingBall()
    {
        // Simplest way: Check if any object with the tag "Ball" 
        // is currently a child of the Player (being held)
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            // If you are using XRI, checking if it has a parent works well
            return ball.transform.parent != null;
        }
        return false;
    }

    void PlayWinCondition()
    {
        if (winSource != null && winClip != null)
        {
            winSource.clip = winClip;
            winSource.Play();
            Debug.Log("WINNER! You escaped with the ball.");

            // Optional: Stop the ambient noise like you did for the Game Over
            if (DisturbanceManager.Instance != null)
            {
                DisturbanceManager.Instance.ambientSource.Stop();
            }
        }
    }
}