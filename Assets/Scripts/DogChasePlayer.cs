using UnityEngine;

public class DogChasePlayer : MonoBehaviour
{
    public DogAI dog;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // IMPACT NOISE METER:
            // Adds 100 points to instantly wake the Uncle and end the game
            if (DisturbanceManager.Instance != null)
            {
                DisturbanceManager.Instance.AddNoise(100f);
            }

            dog.EnterSmallZone();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dog.ExitSmallZone();
        }
    }
}