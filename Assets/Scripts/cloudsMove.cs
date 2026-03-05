using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudsMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        // Reduced from 0.03 to 0.005 for slower movement
        newPosition.z = (float)(newPosition.z + 0.005);
        transform.position = newPosition;
    }
}