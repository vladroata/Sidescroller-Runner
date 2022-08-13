using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Material[] materials;
    public int position1;
    public int position2;
    private float timer = 0f;
    private float attackTimer = 0f;
    GameObject leftAttack;
    GameObject rightAttack;
    private int attacking = 0;
    Vector3 movement;
    int direction = 0; //0 = left, 1 = right.
    int flag = 0;
    int temp;
    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(0.5f, 0f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move between two positions
        if (gameObject.transform.position.x <= position1) {
            direction = 1;
        }
        if (gameObject.transform.position.x >= position2)
        {
            direction = 0;
        }
        if (direction == 0) {
            gameObject.transform.position -= movement*Time.deltaTime;
        }
        if (direction == 1)
        {
            gameObject.transform.position += movement * Time.deltaTime;
        }
        
        timer += Time.deltaTime;
        //every 3 seconds, generate two beams on each side of the enemy to damage the player. The enemy stops when firing.
        if (timer >= 3) {
            timer = 0;
            //create gameobjects, add scripts
            leftAttack = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leftAttack.transform.parent = gameObject.transform; //Seting as child to delete when jumped on
            rightAttack = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rightAttack.transform.parent = gameObject.transform; //Seting as child to delete when jumped on
            leftAttack.transform.position = new Vector3(gameObject.transform.position.x - 1.5f, gameObject.transform.position.y, 0f);
            leftAttack.transform.localScale = new Vector3(2f, 0.5f, 1f);
            rightAttack.transform.position = new Vector3(gameObject.transform.position.x + 1.5f, gameObject.transform.position.y, 0f);
            rightAttack.transform.localScale = new Vector3(2f, 0.5f, 1f);
            leftAttack.GetComponent<Renderer>().sharedMaterial = materials[0];
            rightAttack.GetComponent<Renderer>().sharedMaterial = materials[0];
            var lCol = leftAttack.GetComponent<BoxCollider>();
            lCol.isTrigger = true;
            var rCol = rightAttack.GetComponent<BoxCollider>();
            rCol.isTrigger = true;
            leftAttack.AddComponent<AttackScript>();
            rightAttack.AddComponent<AttackScript>();
            attacking = 1;
        }
        if (attacking == 1) {
            if (flag == 0) {
                temp = direction;
                flag = 1;
            }
            direction = 2;
            attackTimer += Time.deltaTime;
            if (attackTimer >= 1)
            { //reset values, destroy beams
                direction = temp;
                attackTimer = 0;
                attacking = 0;
                Destroy(leftAttack);
                Destroy(rightAttack);
                flag = 0;
            }
        }
    }
}
