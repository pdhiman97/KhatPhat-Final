using UnityEngine;

public class BallItem : MonoBehaviour
{
    // We remove OnTriggerEnter and Destroy(gameObject) 
    // because the XR Grab Interactable will handle the "picking up."

    public void OnBallPickedUp()
    {
        // Find the manager and tell it the ball is now in hand
        BallManager manager = Object.FindAnyObjectByType<BallManager>();
        if (manager != null)
        {
            Debug.Log("Ball picked up! Head to the gate.");
            // Optional: You can trigger a specific UI or audio here
        }
    }

    public void OnBallDropped()
    {
        Debug.Log("Ball dropped! Tau ji might hear that...");
    }
}