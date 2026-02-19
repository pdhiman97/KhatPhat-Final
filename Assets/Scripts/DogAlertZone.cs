using UnityEngine;

public class DogAlertZone : MonoBehaviour
{
    public DogAI dog;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
