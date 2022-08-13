using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float tiltSpeedX;
    public float tiltSpeedY;
    public float tiltSpeedZ;

    // Update is called once per frame
    void Update()
    {
        float changeX = tiltSpeedX * Time.deltaTime;
        float changeY = tiltSpeedY * Time.deltaTime;
        float changeZ = tiltSpeedZ * Time.deltaTime;
        this.transform.Rotate(changeX, changeY, changeZ);
    }
}
