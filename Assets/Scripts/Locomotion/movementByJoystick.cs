using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementByJoystick : MonoBehaviour
{
    public JoystickDataInput joystickDataInput;
    public GameObject cameraAnchor;
    public GameObject moveMe;
    private float jx;
    private float jy;
    public float speed = 10.0f;
    private float cameraOrientation;

    // Update is called once per frame
    void Update()
    {
        jx = joystickDataInput.joystickDataX;
        jy = joystickDataInput.joystickDataY;
        //float normalizedCameraOrientation = NormalizeValue(cameraOrientation);
        Vector3 forwardDirection = cameraAnchor.transform.forward;
        Vector3 backwardDirection = -cameraAnchor.transform.forward;
        forwardDirection.y = 0;
        forwardDirection = forwardDirection.normalized;
        RaycastHit hit;
        if (Physics.Raycast(moveMe.transform.position, backwardDirection, out hit, speed * Time.deltaTime * 3) && jy < 0)
        {
            // Check if the collider is tagged as "WALLS"
            if (hit.collider.CompareTag("WALLS") || hit.collider.CompareTag("BORDERWALLS"))
            {
                // Stop movement if a wall is detected
                return;
            }
        }
        moveMe.transform.Translate(speed * Time.deltaTime * forwardDirection * jy );
    }
}
