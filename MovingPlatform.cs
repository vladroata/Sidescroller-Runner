using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PlatformBaseScript
{
    /*
    public int pos1X = 20;
    public int pos2X = 0;
    public int pos1Y = 16;
    public int pos2Y = 22;

    public float speedX = 1f;
    public float speedY = 1f;
    */
    public float pos1X;
    public float pos2X;
    public float pos1Y;
    public float pos2Y;

    public float moveSpeedX;
    public float moveSpeedY;
    //private float moveSpeedX;
    //private float moveSpeedY;
    private float modX = -1;
    private float modY = 1;
    private float speedX;
    private float speedY;
    private float currentXPos;
    private float currentYPos;
    public Vector3 movement;
    private Vector3 movement_pos;
    private Vector3 movement_neg;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(2f, 4f);
        movement[0] = rand;
        movement_pos = movement;
        movement_neg = movement * -1;
        /*
        //moveSpeedX = moveSpeed;
        //moveSpeedY = moveSpeed;
        
        if (pos1X == pos2X)
            moveSpeedX = 0f;

        if (pos1Y == pos2Y)
            moveSpeedY = 0f;

        speedX = moveSpeedX * modX;
        speedY = moveSpeedY * modY;

        setStartEndPointBasedOnLength();
        //goalPosition = this.transform.position;
        */
    }

    // Update is called once per frame
    void FixedUpdate() 
    {

        //platform moves between two positions, repeatedly

        if (gameObject.transform.position.x + gameObject.transform.localScale.x / 2 < GameObject.FindWithTag("MainCamera").transform.position.x - 20)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += movement * Time.deltaTime;
        if (gameObject.transform.position.x - gameObject.transform.localScale.x/2 < pos1X) {

            movement = movement_pos;
        }
        if (gameObject.transform.position.x > pos2X)
        {
            movement = movement_neg;
        }
        /*
        //goalPosition = this.transform.position;
        //currentXPos = this.transform.position.x;
        //currentYPos = this.transform.position.y;

        bool atEdgeX = (speedX == 0f) || ((currentXPos <= pos1X) || (currentXPos >= pos2X));
        bool atEdgeY = (speedY == 0f) || ((currentYPos <= pos1Y) || (currentYPos >= pos2Y));
        //bool atEdgeX = ((currentXPos <= pos1X) || (currentXPos >= pos2X));
        //bool atEdgeY = ((currentYPos <= pos1Y) || (currentYPos >= pos2Y));

        if ((atEdgeX) && (atEdgeY))
        {
            modX = -modX;
            modY = -modY;
            speedX = modX * moveSpeedX;
            speedY = modY * moveSpeedY;
        }


        if ((speedX == 0f) && (speedY != 0f))
        {
            MoveY(speedY);
        }
        else if ((speedX != 0f) && (speedY == 0f))
        {
            MoveX(speedX);
        }
        else if ((!atEdgeX) && (atEdgeY))
        {
            MoveX(speedX);
        }
        else if ((atEdgeX) && (!atEdgeY))
        {
            MoveY(speedY);
        }
        else
        {//if ((atEdgeX) && (atEdgeY)) {
            MoveX(speedX);
            MoveY(speedY);
        }

        //this.transform.position = goalPosition;
        */
    }
    public void setPos1X(float x) {
        pos1X = x;
    }
    public void setPos2X(float x)
    {
        pos2X = x;
    }
    public float getPos1X()
    {
        return pos1X;
    }
    public float getPos2X()
    {
        return pos2X;
    }

    /*
    public void setStartEndPointBasedOnLength()
    {
        //startpoint = transform.position;
        //endpoint = transform.position;

        if ((pos2X != null && pos1X != null) &&(pos1X != pos2X))
        {
            startpoint.x -= pos1X;
            endpoint.x += pos2X;
        }
        else
        {
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

        if ((pos2Y != null && pos1Y != null) && (pos1Y != pos2Y))
        {
            startpoint.y -= pos1Y;
            endpoint.y += pos2Y;
        }
    }

    void MoveX(float direction)
    {
        goalPosition.x = goalPosition.x + direction * Time.deltaTime;

        if (goalPosition.x > pos2X)
        {
            goalPosition.x = pos2X;
            speedX = 0f;
        }

        if (goalPosition.x < pos1X)
        {
            goalPosition.x = pos1X;
            speedX = 0f;
        }
    }

    void MoveY(float direction)
    {
        goalPosition.y = goalPosition.y + direction * Time.deltaTime;

        if (goalPosition.y > pos2Y)
        {
            goalPosition.y = pos2Y;
            speedY = 0f;
        }

        if (goalPosition.y < pos1Y)
        {
            goalPosition.y = pos1Y;
            speedY = 0f;
        }
    }*/
}
 