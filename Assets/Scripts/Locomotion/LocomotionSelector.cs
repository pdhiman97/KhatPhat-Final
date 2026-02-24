using UnityEngine;

public class LocomotionSelector : MonoBehaviour
{
    HeadBobLocomotion headBobLocomotion;
    DetectHandSwing detectHandSwing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headBobLocomotion = gameObject.GetComponent<HeadBobLocomotion>();
        detectHandSwing = gameObject.GetComponent<DetectHandSwing>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetString("LocomotionTechnique") == "head-bob")
        {
            headBobLocomotion.enabled = true;
            detectHandSwing.enabled = false;
        }
        else
        {
            headBobLocomotion.enabled = false;
            detectHandSwing.enabled = true;
        }
    }
}
