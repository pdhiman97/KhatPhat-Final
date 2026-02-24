using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JoystickDataInput : MonoBehaviour
{
    public float joystickDataX;
    public float joystickDataY;
    void Update()
    {
        OVRInput.Update();
        joystickDataX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        joystickDataY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
    }
    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }
}
