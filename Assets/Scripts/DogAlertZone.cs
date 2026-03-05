using UnityEngine;

public class DogAlertZone : MonoBehaviour
{
    public DogAI dog;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // IMPACT NOISE METER:
            // Adds 50 points to the DisturbanceManager
            if (DisturbanceManager.Instance != null)
            {
                DisturbanceManager.Instance.AddNoise(50f);
            }

            dog.EnterLargeZone();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dog.ExitLargeZone();
        }
    }
}