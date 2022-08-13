using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBaseScript : MonoBehaviour
{
    public float platformLengthX = -9999;
    public Vector3 startpoint;
    public Vector3 endpoint;

    // Set some default values
    void Start()
    {
        if (platformLengthX != null && platformLengthX > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = platformLengthX;
            transform.localScale = scale;
        }

        setStartEndPointBasedOnLength();
    }


    public void setStartEndPointBasedOnLength() {
        startpoint = transform.position;
        endpoint = transform.position;
        if (platformLengthX != -9999)
        {
            startpoint.x -= platformLengthX / 2;
            endpoint.x += platformLengthX / 2;
        }
        else
        {
            startpoint.x -= (float) 10.25 / 2;
            endpoint.x += (float) 10.25 / 2;
        }
    }


    //functions to allow other scripts to modify this gameobject
    public void setStartEndPointBasedOnLength(float otherLength)
    {
        startpoint = transform.position;
        endpoint = transform.position;
        startpoint.x -= otherLength / 2;
        endpoint.x += otherLength / 2;
    }

    private void updatePlatformSpecsFromPoints() {
        platformLengthX = endpoint.x - startpoint.x;
        Vector3 midpoint = (startpoint + endpoint) / 2;
        transform.position = midpoint;

        Vector3 scale = transform.localScale;
        scale.x = platformLengthX;
        transform.localScale = scale;
    }


    private void updatePlatformSpecsFromLengthChange()
    {
        if (platformLengthX != null && platformLengthX > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = platformLengthX;
            transform.localScale = scale;
        }

        setStartEndPointBasedOnLength();
    }

    public float getPlatformLengthX() { return platformLengthX; }
    public void setPlatformLengthX(float length) { platformLengthX = length; updatePlatformSpecsFromLengthChange(); }

    public Vector3 getStartPoint() { return startpoint; }
    public Vector3 getEndPoint() { return endpoint; }

    public void setStartPoint(Vector3 start) { startpoint = start; updatePlatformSpecsFromPoints(); }
    public void setEndPoint(Vector3 end) { endpoint = end; updatePlatformSpecsFromPoints(); }

}
