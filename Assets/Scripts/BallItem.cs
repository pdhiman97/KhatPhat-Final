using UnityEngine;

public class BallItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player (XR Controller or Camera) touched it
        if (other.CompareTag("Player"))
        {
            BallManager manager = Object.FindAnyObjectByType<BallManager>();
            if (manager != null) manager.OnBallCollected();

            Destroy(gameObject); // Remove ball from world
        }
    }
}