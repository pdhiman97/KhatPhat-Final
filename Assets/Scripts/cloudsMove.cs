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
        newPosition.z = (float)(newPosition.z + 0.03);
        transform.position = newPosition;
    }
}
