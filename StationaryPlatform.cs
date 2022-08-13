using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryPlatform : MonoBehaviour
{
    [SerializeField]
    public Transform endPoint;

    //public GameObject ParentLevelObject;
    public float platformLengthX = -1;

    public int platform_EndPositionX = -1;
    public int platform_EndPositionY = -1;

    public Transform parentTransform;
    public Vector3 startpoint;
    public Vector3 endpoint;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        setStartEndPointBasedOnLength();
    }
    void FixedUpdate() {
        if (gameObject.transform.position.x + gameObject.transform.localScale.x / 2 < GameObject.FindWithTag("MainCamera").transform.position.x - 20)
        {
            Destroy(gameObject);
        }
    }

    public void setStartEndPointBasedOnLength()
    {
        startpoint = transform.position;
        endpoint = transform.position;
        if (platformLengthX != -9999)
        {
            startpoint.x -= platformLengthX / 2;
            endpoint.x += platformLengthX / 2;
        }
        else
        {
            startpoint.x -= (float)10.25 / 2;
            endpoint.x += (float)10.25 / 2;
        }
    }


    public void setStartEndPointBasedOnLength(float otherLength)
    {
        startpoint = transform.position;
        endpoint = transform.position;
        startpoint.x -= otherLength / 2;
        endpoint.x += otherLength / 2;
    }

    public void SetPlatformScale(float length)
    {
        if (length > 1f)
        {
            platformLengthX = length;
            Vector3 customScale = transform.localScale;
            customScale.x = platformLengthX;
            transform.localScale = customScale;

            transform.position += new Vector3((float)Random.Range(0f, 3f), 0, 0);
            CalculateEndPosition();
        }
    }

    public Vector3 GetEndPoint() {
        return endpoint;
    }
    
    public void CalculateEndPosition()
    {
        if(transform.childCount == 0)
        {
            //Create EndPosition Object
            var point = Instantiate(endPoint, transform.position + new Vector3((float)Random.Range(0f, 3f)/2, 0f, 0f), Quaternion.identity);
            point.transform.SetParent(transform);
        }
        
    }

}
