using UnityEngine;

public class DogChasePlayer : MonoBehaviour
{
    public DogAI dog;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
