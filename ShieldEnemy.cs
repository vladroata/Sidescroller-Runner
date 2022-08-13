using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    public int shieldActive = 1;
    int sphereCreated = 0;
    public Material[] materials;
    GameObject sphere;
    float timer = 0f;
    public GameObject FlyingBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 3) { //enemy fires off two bullets from either side
            timer = 0;
            GameObject bullet1 = Instantiate(FlyingBullet, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
            
            print(bullet1.transform.position - new Vector3(1, 0, 0));
            GameObject bullet2 = Instantiate(FlyingBullet, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            bullet1.transform.LookAt(bullet1.transform.position - new Vector3(1, 0, 0));
            bullet2.transform.LookAt(bullet2.transform.position + new Vector3(1, 0, 0));

        }
        if (shieldActive == 1) {
            if (sphereCreated == 0) //Enemy has a shield that protects it from one attack and then is gone. Enemy is immune to damage for a short time after the shield is destroyed.
            {
                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.gameObject.transform.localScale = new Vector3(2, 2, 2);
                var sCol = sphere.GetComponent<SphereCollider>();
                Rigidbody rb = sphere.AddComponent<Rigidbody>();
                sCol.isTrigger = true;
                rb.isKinematic = true;
                sphere.GetComponent<Renderer>().sharedMaterial = materials[0];
                sphere.transform.position = gameObject.transform.position - new Vector3(0.05f, 0, 0);
                sphereCreated = 1;
            }
            sphere.transform.position = gameObject.transform.position - new Vector3(0.05f, 0, 0);
        }
        if (shieldActive == 0) {
            Destroy(sphere);
        }
    }
    
    

}
