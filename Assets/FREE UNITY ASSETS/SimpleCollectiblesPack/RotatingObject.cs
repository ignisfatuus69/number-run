using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public bool rotate; // do you want it to rotate?
    public float rotationSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
