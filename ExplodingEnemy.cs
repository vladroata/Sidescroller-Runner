using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public static int inSphere = 0;
    public Material[] materials;
    Renderer rend;
    Color oldColor;
    public int damageImmune = 0;
    private float timer = 0;
    private float explodeTimer = 0;
    float distance;
    private int readyToTeleport = 0;
    private int explodeTrigger = 0;
    private int sphereCreated = 0;
    public GameObject player;
    public GameObject sphere;
    private int applyForce = 0;
    private int applyForceFlag = 0;
    float forceTimer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = materials[0];

    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        //Destroy(gameObject, exp.main.duration);
    }
    void dealDamage() {
        if (PlayerControllerAnimation.damageImmune == 0) {
            GameObject.Find("PlagueDoctor(Clone)").gameObject.GetComponent<PlayerControllerAnimation>().playerCurrentHealth -= 10;
        }

    }


    void FixedUpdate() {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); //keep trying to find the player object because the reference is needed and the player is spawned after the player
        }
        distance = Vector3.Distance(player.transform.position, transform.position);
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            readyToTeleport = 1; //allow the enemy to teleport every 5s
            timer = 0;
        }
        if (distance <= 15 && readyToTeleport == 1) {
            transform.position = new Vector3(1, 1, 0) + (player.transform.position); //teleport to player if they are in range
            readyToTeleport = 0;
            explodeTrigger = 1;
        }
        if (explodeTrigger == 1) { // create the sphere representing the explosion and set attributes
            rend.sharedMaterial = materials[1];
            damageImmune = 1;
            explodeTimer += Time.deltaTime;
            if (sphereCreated == 0) {
                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.gameObject.transform.localScale = new Vector3(2, 2, 2);
                var sCol = sphere.GetComponent<SphereCollider>();
                Rigidbody rb = sphere.AddComponent<Rigidbody>();
                sCol.isTrigger = true;
                rb.isKinematic = true;
                sphere.AddComponent<SphereScript>();
                sphere.GetComponent<Renderer>().sharedMaterial = materials[1];
                sphere.transform.position = gameObject.transform.position - new Vector3(0.05f, 0, 0);
                sphereCreated = 1;
            }
            if (explodeTimer >= 1) {
                Explode(); //explode when ready
                if (inSphere == 1) { //deal damage and apply pushback if the player is in the sphere (colliding with it)
                    dealDamage();
                    applyForce = 1;
                }
                //reset values
                rend.sharedMaterial = materials[0];
                explodeTrigger = 0;
                damageImmune = 0;
                explodeTimer = 0;
                Destroy(sphere);
                sphereCreated = 0;
            }
        }
        if (applyForce == 1) { //force application. Having a character controller made it difficult to add a rigidbody. We added force by simulating it with short teleports.
            var control = player.GetComponent<CharacterController>();
            forceTimer += Time.deltaTime;
            if (forceTimer < 0.1f)
            {
                control.Move(new Vector3(-distance / 10, 0, 0));
            }
            if (forceTimer > 0.3f) {
                applyForce = 0;
                forceTimer = 0;
            }
        }
    }
}