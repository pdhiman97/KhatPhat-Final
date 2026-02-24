using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHandSwing : MonoBehaviour
{
    public GameObject trackingObject;
    public GameObject centerEyeAnchor;
    public GameObject leftHand, rightHand;
    float swingThreshold = 1.75f;        // Threshold velocity to detect swing
    public static float movementSpeed = 10.0f;         // Speed multiplier for movement

    Vector3 LHpreviousPosition;
    Vector3 RHpreviousPosition;
    bool canMove = true;
    float lastSwingTime;
    float moveCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        LHpreviousPosition = leftHand.transform.localPosition;
        RHpreviousPosition = rightHand.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandSwing();
    }

    void HandSwing()
    {
        // Get the current position of the hand
        Vector3 leftHandCurrentPosition = leftHand.transform.localPosition;

        // Calculate the velocity of the hand (how fast it is moving)
        Vector3 leftHandVelocity = (leftHandCurrentPosition - LHpreviousPosition) / Time.deltaTime;

        // Store the current position as the previous for the next frame
        LHpreviousPosition = leftHandCurrentPosition;

        // Get the current position of the hand
        Vector3 rightHandCurrentPosition = rightHand.transform.localPosition;

        // Calculate the velocity of the hand (how fast it is moving)
        Vector3 rightHandVelocity = (rightHandCurrentPosition - RHpreviousPosition) / Time.deltaTime;

        // Store the current position as the previous for the next frame
        RHpreviousPosition = rightHandCurrentPosition;

        // Check if the velocity exceeds the swing threshold
        if (leftHandVelocity.magnitude > swingThreshold && canMove && rightHandVelocity.magnitude > swingThreshold)
        {
            // Detecting a swing, and trigger the movement
            if (moveCount > 15)
            {
                MovePlayer((leftHandVelocity.magnitude + rightHandVelocity.magnitude) / 5);
            }
            //Debug.Log("moving" + moveCount);
            moveCount += 1;
            lastSwingTime = Time.time;
        }
    }

    void MovePlayer(float swingspeed)
    {
        Vector3 direction = centerEyeAnchor.transform.forward;
        direction.y = 0;
        direction = direction.normalized;
        moveCount -= 2;
        // Raycast to check if there is an obstacle in the direction of movement
        RaycastHit hit;
        if (Physics.Raycast(trackingObject.transform.position, direction, out hit, swingspeed * Time.deltaTime * movementSpeed))
        {
            // Check if the collider is tagged as "WALLS"
            if (hit.collider.CompareTag("WALLS") || hit.collider.CompareTag("BORDERWALLS"))
            {
                // Stop movement if a wall is detected
                return;
            }
        }
        trackingObject.transform.Translate(direction * swingspeed * Time.deltaTime * movementSpeed, Space.World);
    }

}
