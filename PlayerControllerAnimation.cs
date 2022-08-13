using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerAnimation : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public float playerSpeed;
    public int playerMaxHealth = 100;
    public int playerCurrentHealth;

    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float walkSpeed = 6.0f;
    public float runSpeed = 10f;
    public bool canDoubleJump;
    public float jumpHeight = 2.0f;
    public float gravityValue = -9.81f;
    public float castTimer = 3.0f;

    public float innerCastTimer;
    public static int PowerUp = 0;
    private float laserTimer = 0f;
    private float powerUpTimer = 0f;
    private int laserTimerFlag = 0;
    float dist = 0f;
    GameObject capsule;
    public GameObject playerBase;
    public static int damageImmune = 0;
    private float immunityTimer = 0f;
    private int teleportTimerFlag = 0;
    int powerUp0Flag = 0;
    public int powerUp1Flag = 0;
    public int powerUp2Flag = 0;
    public int powerUp2Charges = 0;
    public int powerUp3Flag = 0;
    public int powerUp4Flag = 0;
    
    public GameObject model;
    public Material defaultMaterial;
    Renderer mesh;
    Color defaultColor;

    public GManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GManager>();
        model = GameObject.FindWithTag("PlagueDoctor");
        mesh = model.GetComponent<SkinnedMeshRenderer>();
        defaultColor = mesh.material.GetColor("_Color");
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        innerCastTimer = castTimer;
        playerSpeed = walkSpeed;
        playerCurrentHealth = playerMaxHealth;
        

    }

    void Update()
    {
        if(playerCurrentHealth <= 0)
        {
            PowerUp = 0;
            gameManager.InCompleteLevel();
        }

        animator.SetBool("isJumping", false);
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            canDoubleJump = true;
        }

        //Casting Timer logic
        if (innerCastTimer <= 0f && animator.GetBool("isCasting") == true)
        {
            innerCastTimer = castTimer;
            animator.SetBool("isCasting", false);
        }
        else if (innerCastTimer > 0f && animator.GetBool("isCasting") == true)
        {
            innerCastTimer -= 1.0f * Time.deltaTime;
        }

        //If falling
        if (!groundedPlayer && playerVelocity.y != 0)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isCasting", false);
        }

        //If landed, then ensure that the y velocity is 0
        if (groundedPlayer && playerVelocity.y < 0)
        {
            animator.SetBool("isFalling", false);
            playerVelocity.y = 0f;
        }

        //Input.GetAxis("Vertical")

        //Retrieve horizontal input, then creates a new movement vector
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0f)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isCasting", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //Move in the horizontal axis
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Rotate object upon moving left-right
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl))
        {
            playerSpeed = runSpeed;
        }
        else
        {
            playerSpeed = walkSpeed;
        }


        // Changes the height position of the player
        if (Input.GetKey(KeyCode.Space))
        {
            //doubleJumpLogic
            if (groundedPlayer)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isCasting", false);
                //Debug.Log("Player Velocity 1st before: " + playerVelocity.y);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                //Debug.Log("Player Velocity 1st: " + playerVelocity.y);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (canDoubleJump)
                    {
                        //Debug.Log("Player Velocity 2nd before: " + playerVelocity.y);
                        //The fix for the double jump: not += but just = , since velocity is negative and the additional jump may only add enough to break a small positive value
                        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                        //Debug.Log("Player Velocity 2nd after: " + playerVelocity.y);
                        canDoubleJump = false;
                    }
                }
            }

        }

        //Applying gravitational forces
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //FireBall casting spell
        /*if (Input.GetKeyDown(KeyCode.LeftControl) && groundedPlayer && animator.GetBool("isCasting") == false)
        {
            animator.SetBool("isCasting", true);
        }*/

        //Powerups
        if(PowerUp == 0)
        {
            if (powerUp0Flag == 0) {
                powerUp1Flag = 0;
                powerUp2Flag = 0;
                powerUp3Flag = 0;
                powerUp4Flag = 0;
                gravityValue = -9.81f;
                powerUpTimer = 0;
                damageImmune = 0;
                runSpeed = 10;
                walkSpeed = 6;
                jumpHeight = 2;
                mesh.material.SetColor("_Color", defaultColor);
                powerUp0Flag = 1;
            }
        }

        if (PowerUp == 1)
        {
            powerUp0Flag = 0;
            powerUpTimer += Time.deltaTime;
            if (powerUp1Flag == 0)
            {
                powerUp0Flag = 0;
                powerUp2Flag = 0;
                powerUp3Flag = 0;
                powerUp4Flag = 0;
                mesh.material.SetColor("_Color", Color.yellow);
                powerUp1Flag = 1;
            }
            if (laserTimerFlag == 1)
            {
                laserTimer += Time.deltaTime;
            }
            if (Input.GetButtonDown("Fire1") && laserTimerFlag != 1)
            {
                laserTimerFlag = 1;
                animator.SetBool("isCasting", true);
                //Vector3 posVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
                Vector3 posVec = Camera.main.ScreenToWorldPoint(screenPoint);
                posVec[2] = 0;
                capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                dist = Vector3.Distance(posVec, transform.position);
                var rb = capsule.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                var cCol = capsule.GetComponent<CapsuleCollider>();
                cCol.isTrigger = true;

                float x = Mathf.Abs(posVec[0] - transform.position.x);
                float y = Mathf.Abs(posVec[1] - transform.position.y);
                float hypotenuse = Mathf.Sqrt(x * x + y * y);
                float angle = Mathf.Asin(y / hypotenuse) * 180 / Mathf.PI;
                if (posVec[0] >= transform.position.x && posVec[1] >= transform.position.y)
                {
                    capsule.transform.position = transform.position;
                    capsule.transform.rotation = Quaternion.Euler(capsule.transform.eulerAngles.x + (90 - angle), 90, 0);
                    capsule.transform.Translate(0, (dist / 2), 0);
                    capsule.transform.localScale = new Vector3(0.5f, dist / 2, 0.5f);
                }
                else if (posVec[0] > transform.position.x && posVec[1] < transform.position.y)
                {
                    capsule.transform.position = transform.position;
                    capsule.transform.rotation = Quaternion.Euler(capsule.transform.eulerAngles.x + (180 - (90 - angle)), 90, 0);
                    capsule.transform.Translate(0, (dist / 2), 0);
                    capsule.transform.localScale = new Vector3(0.5f, dist / 2, 0.5f);
                }
                else if (posVec[0] < transform.position.x && posVec[1] >= transform.position.y)
                {
                    capsule.transform.position = transform.position;
                    capsule.transform.rotation = Quaternion.Euler(capsule.transform.eulerAngles.x + (180 - (90 - angle)), 90, 0);
                    capsule.transform.Translate(0, -(dist / 2), 0);
                    capsule.transform.localScale = new Vector3(0.5f, dist / 2, 0.5f);
                }
                else if (posVec[0] < transform.position.x && posVec[1] < transform.position.y)
                {
                    capsule.transform.position = transform.position;
                    capsule.transform.rotation = Quaternion.Euler(capsule.transform.eulerAngles.x + (90 - angle) + 180, 90, 0);
                    capsule.transform.Translate(0, (dist / 2), 0);
                    capsule.transform.localScale = new Vector3(0.5f, dist / 2, 0.5f);
                }
                capsule.AddComponent<LaserScript>();



                //The code below is all stuff that I wrote earlier while trying to create the powerup laser, I'm leaving the prototyping here to show for later, as requested in the assignment instructions
                /*
                var line = gameObject.AddComponent<LineRenderer>();
                line.SetVertexCount(2);
                line.SetPosition(0, transform.position + new Vector3(1, 1, 0));
                line.SetPosition(1, posVec);
                line.SetWidth(0.5f, 0.5f);
                line.useWorldSpace = true;
                var pCol = gameObject.AddComponent<PolygonCollider2D>();
                pCol.isTrigger = true;
                */


                /*
                print("1" + posVec);
                posVec[2] = 0;
                float temp1 = posVec[0];
                float temp2 = posVec[1];
                print("transform.pos: " + transform.position.x);
                print("subtraction:" + 2 * (posVec[0] - transform.position.x));
                posVec[0] -= 2 * (posVec[0] - transform.position.x);
                print("temp1:" + temp1);
                print("posVec0" + posVec[0]);
                posVec[1] -= 2 * (posVec[1] - transform.position.y);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = transform.position + new Vector3(0, 1, 0);
                sphere.transform.LookAt(posVec);
                print("2" + posVec);
                float x = sphere.transform.rotation.x;
                float y = sphere.transform.rotation.y;
                float z = sphere.transform.rotation.z;
                float w = sphere.transform.rotation.w;
                Destroy(sphere);
                posVec[0] = temp1;
                posVec[1] = temp2;
                laser = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                dist = Vector3.Distance(posVec, transform.position);
                laser.transform.localScale = new Vector3(0.1f, Vector3.Distance(posVec, transform.position), 0.1f);
                var cCol = laser.GetComponent<CapsuleCollider>();
                cCol.isTrigger = true;
                laser.transform.position = transform.position + new Vector3(1, 1, 0);
                laser.transform.rotation = new Quaternion(x, y, z, w);*/

            }
            if (laserTimer >= 2.5f)
            {
                laserTimer = 0;
                laserTimerFlag = 0;
            }
            if (powerUpTimer >= 30)
            {
                powerUpTimer = 0;
                PowerUp = 0;
            }
        }

        if (PowerUp == 2)
        {
            if (powerUp2Flag == 0) {
                powerUp0Flag = 0;
                powerUp1Flag = 0;
                powerUp3Flag = 0;
                powerUp4Flag = 0;
                mesh.material.SetColor("_Color", Color.green);
                gravityValue = -9.81f;
                powerUp2Charges = 5;
                powerUp2Flag = 1;
            }
            if (teleportTimerFlag == 1)
            {
                immunityTimer += Time.deltaTime;
            }
            if (Input.GetButtonDown("Fire1") && powerUp2Charges >= 1)
            {
                //mousePos.x = Input.mousePosition.x;
                //mousePos.y = Input.mousePosition.y;
                var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
                Vector3 posVec = Camera.main.ScreenToWorldPoint(screenPoint);
                //Vector3 posVec = Camera.ScreenToWorldPoint(Input.mousePosition);
                posVec[2] = 0;
                posVec[0] = posVec[0] - transform.position.x;
                posVec[1] = posVec[1] - transform.position.y;
                controller.Move(posVec);
                canDoubleJump = true;
                teleportTimerFlag = 1;
                damageImmune = 1;
                powerUp2Charges -= 1;
            }
            if (immunityTimer >= 2)
            {
                teleportTimerFlag = 0;
                immunityTimer = 0;
                damageImmune = 0;
            }
            if (powerUp2Charges == 0) {
                if (immunityTimer >= 2) {
                    PowerUp = 0;
                }
            }
        }
        if (PowerUp == 3) {
            if (powerUp3Flag == 0) {
                powerUp0Flag = 0;
                powerUp1Flag = 0;
                powerUp2Flag = 0;
                powerUp4Flag = 0;
                mesh.material.SetColor("_Color", Color.blue);
                powerUp3Flag = 1;
            }
            powerUpTimer += Time.deltaTime;
            gravityValue = -9.81f / 2;
            if (powerUpTimer >= 15) {
                PowerUp = 0;
                gravityValue = -9.81f;
                powerUpTimer = 0;
            }
        }
        if (PowerUp == 4)
        {
            powerUpTimer += Time.deltaTime;
            if (powerUp4Flag == 0)
            {
                powerUp0Flag = 0;
                powerUp1Flag = 0;
                powerUp2Flag = 0;
                powerUp3Flag = 0;
                mesh.material.SetColor("_Color", Color.magenta);
                gravityValue = -9.81f;
                runSpeed = runSpeed * 1.5f;
                walkSpeed = walkSpeed * 1.5f;
                jumpHeight = jumpHeight * 1.5f;
                powerUp4Flag = 1;
            }

            if (powerUpTimer >= 15)
            {
                PowerUp = 0;
                runSpeed = runSpeed / 1.5f;
                walkSpeed = walkSpeed / 1.5f;
                jumpHeight = jumpHeight / 1.5f;
                powerUpTimer = 0;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("EndZone"))
        {
            if(gameManager != null)
            {
                PowerUp = 0;
                gameManager.CompleteLevel();
            }
            
        }
        else if (hit.gameObject.CompareTag("DeathZone"))
        {
            if (gameManager != null)
            {
                PowerUp = 0;
                playerCurrentHealth = 0;
                gameManager.InCompleteLevel();
            }    
        }

    }

}
