using UnityEngine;

public class CheckWallinBound : MonoBehaviour
{
    public static bool canMove = true; // Public static variable to control movement
    public GameObject Locomotion;

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the required tags
        if (other.CompareTag("WALLS") || other.CompareTag("BORDERWALLS"))
        {
            canMove = false; // Disable movement
            Locomotion.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Allow movement again when no longer inside the object
        if (other.CompareTag("WALLS") || other.CompareTag("BORDERWALLS"))
        {
            canMove = true;
            Locomotion.SetActive(true);
        }
    }
}
