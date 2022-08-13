using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryFire : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    private DeathZones script;

    private void Awake()
    {
        script = transform.GetComponent<DeathZones>();
    }
    private void Start()
    {
        var camera = GameObject.FindWithTag("MainCamera");
        mainCamera = camera.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    private void Update()
    {
        if(script != null)
        {
            for (int i = 0; i < script.fires.Count; i++)
            {
                if(script.fires[i].transform.position.x < mainCamera.transform.position.x + screenBounds.x)
                {
                    var temp = script.fires[i];
                    script.fires.RemoveAt(i);
                    Destroy(temp);
                }
            }
        }    
    }

}
