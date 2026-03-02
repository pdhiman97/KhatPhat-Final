using UnityEngine;

public class ArmSwingLocomotion : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public CharacterController characterController;

    public float speedMultiplier = 2.5f;
    public float movementThreshold = 0.02f;

    private Vector3 lastLeftPos;
    private Vector3 lastRightPos;

    void Start()
    {
        lastLeftPos = leftHand.position;
        lastRightPos = rightHand.position;
    }

    void Update()
    {
        Vector3 leftDelta = leftHand.position - lastLeftPos;
        Vector3 rightDelta = rightHand.position - lastRightPos;

        float swingAmount = leftDelta.magnitude + rightDelta.magnitude;

        if (swingAmount > movementThreshold)
        {
            Vector3 forward = head.forward;
            forward.y = 0;
            forward.Normalize();

            characterController.Move(forward * swingAmount * speedMultiplier * Time.deltaTime);
        }

        lastLeftPos = leftHand.position;
        lastRightPos = rightHand.position;
    }
}