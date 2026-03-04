using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("YOU ESCAPED WITH THE BALL!");
            // Trigger your Win UI or restart here
        }
    }
}