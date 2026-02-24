using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHeadOrientation : MonoBehaviour
{
    public float RotationY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotationY = transform.localRotation.eulerAngles.y;
    }
}
