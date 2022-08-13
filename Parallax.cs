using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    public GameObject currentCamera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float distance = (currentCamera.transform.position.x * parallaxEffect);

        this.transform.position = new Vector3(startPosition + distance, this.transform.position.y, this.transform.position.z);
    }
}
