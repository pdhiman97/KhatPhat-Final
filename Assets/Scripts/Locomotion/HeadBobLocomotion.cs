using System.Collections;
using UnityEngine;

public class HeadBobLocomotion : MonoBehaviour
{
    public GameObject cameraAnchor;
    public GameObject trackingSpace;

    double heightOfCamera;
    private double maxHeight = 0f;
    float timer = 0f;
    float trackingDuration = 2f;
    float heightChangeThreshold = 0.03f;

    int downCount = 0;
    int HeadWaveCount = 0;

    public float speed = 3f;
    public int initialMoveSteps = 20;
    int moveSteps;

    private void Start()
    {
        speed = PlayerPrefs.GetInt("HeadBobSpeed");
        initialMoveSteps = PlayerPrefs.GetInt("MoveStepsHeadbob");
        moveSteps = initialMoveSteps;
    }

    void Update()
    {
        heightOfCamera = cameraAnchor.transform.position.y;

        timer += Time.deltaTime;

        if (timer < trackingDuration)
        {
            if (heightOfCamera > maxHeight)
                maxHeight = heightOfCamera;
        }

        if (Mathf.Abs((float)(heightOfCamera - maxHeight)) > heightChangeThreshold)
            downCount++;

        if (downCount >= 2)
        {
            if (Mathf.Abs((float)(heightOfCamera - maxHeight)) < heightChangeThreshold)
            {
                downCount = 0;
                HeadWaveCount++;
            }
        }

        if (HeadWaveCount > 1)
        {
            HeadWaveCount--;
            moveCamera();
        }
    }

    private void moveCamera()
    {
        Vector3 forwardDirection = cameraAnchor.transform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();

        StartCoroutine(RunRoutine(forwardDirection));
    }

    IEnumerator RunRoutine(Vector3 forwardDirection)
    {
        while (moveSteps > 0)
        {
            trackingSpace.transform.Translate(speed * Time.deltaTime * forwardDirection);
            moveSteps--;
            yield return null;
        }

        moveSteps = initialMoveSteps;
    }
}