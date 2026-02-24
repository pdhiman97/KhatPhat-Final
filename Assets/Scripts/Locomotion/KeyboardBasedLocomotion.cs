using UnityEngine;

public class KeyboardBasedLocomotion : MonoBehaviour
{
    public GameObject targetObject; // The object you want to move
    public static float speed = 4.0f;      // Movement speed
    public float rotationSpeed = 100f; // Rotation speed
    public GameObject centerEyeAnchor;

    void Update()
    {
        if (targetObject != null)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        // Rotate left and right using Left/Right arrow keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetObject.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        Vector3 direction = centerEyeAnchor.transform.forward;
        direction.y = 0;
        direction = direction.normalized;
        // Raycast to check if there is an obstacle in the direction of movement
        RaycastHit hit;
        if (Physics.Raycast(targetObject.transform.position, direction, out hit, Time.deltaTime * speed))
        {
            // Check if the collider is tagged as "WALLS"
            if (hit.collider.CompareTag("WALLS") || hit.collider.CompareTag("BORDERWALLS"))
            {
                // Stop movement if a wall is detected
                return;
            }
        }

        // Move forward and backward using the Up/Down arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            targetObject.transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        direction = -centerEyeAnchor.transform.forward;
        direction.y = 0;
        direction = direction.normalized;

        if (Physics.Raycast(direction, direction, out hit, Time.deltaTime * speed))
        {
            // Check if the collider is tagged as "WALLS"
            if (hit.collider.CompareTag("WALLS") || hit.collider.CompareTag("BORDERWALLS"))
            {
                // Stop movement if a wall is detected
                return;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            targetObject.transform.Translate(-targetObject.transform.forward * speed * Time.deltaTime, Space.World);
        }
        
        
    }
}
