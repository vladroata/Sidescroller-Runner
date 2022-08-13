using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartGenerator : MonoBehaviour
{
    //[SerializeField]
    private GameObject[] goArray = new GameObject[3];

    //[SerializeField]
    //private List<Transform> platformSelectionList = new List<Transform>();

    public Transform parentTransform;
    public float levelPartLength = 0f;
    public Vector3 endLevelPartVector;
    public float maxPlatformVerticalSpace = 7f;
    public float maxPlatformHorizontalSpace = 5f;
    public float numberOfPlatformsPerlevelPart = 0f;

    //public Vector3 levelPlatformPointer = new Vector3(0, 0, 0);
    public List<Transform> platformList = new List<Transform>(1);
    public GameObject stationaryPlatform;
    public GameObject horizontallyMovingPlatform;
    public GameObject verticallyMovingPlatform;


    // Start is called before the first frame update
    void Start()
    {
        //find references to platforms to be able to create new ones. It's done like this because there is no object that this file is attached to, the object that hosts this script is always generated.
        //Attempted to attach prefabs to the script itself but it didn't work out.
        stationaryPlatform = GameObject.Find("PlatformBaseRef");
        horizontallyMovingPlatform = GameObject.Find("MovingPlatformBaseRef");
        verticallyMovingPlatform = GameObject.Find("VerticalMovingPlatformBaseRef");
        goArray[0] = stationaryPlatform;
        goArray[1] = horizontallyMovingPlatform;
        goArray[2] = verticallyMovingPlatform;
        //levelPlatformPointer = LevelGenerator.lastPlatform.transform.position;
        while (LevelGenerator.levelPlatformPointer.x <= endLevelPartVector.x)
        {   //generate platforms to fill the space between pointers. Number of children platforms can vary because platform sizes vary.
            GeneratePlatform();
            if (LevelGenerator.flagFirst == 0)
            {
                LevelGenerator.flagFirst = 1;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GeneratePlatform()
    {
        //Instantiate Random Platform from PlatformSelectionList
        int stageMaxInt = goArray.Length;
        int stageInt = Random.Range(0, stageMaxInt); //stageMaxInt needs to be greater than the last array element's index because max is exclusive in Random.Range when using ints

        var currentPlatform = Instantiate(goArray[stageInt], LevelGenerator.levelPlatformPointer, Quaternion.identity); //create the platform
        LevelGenerator.lastPlatform = currentPlatform.gameObject;
        //LevelGenerator.lastPlatform.transform.SetParent(transform);

        //Move Object's Height and pointer based on new position
        Vector3 tempHeight = new Vector3(0f, CalculatePlatformHeight(), 0f);
        LevelGenerator.lastPlatform.transform.position += tempHeight;
        if (LevelGenerator.lastPlatform.transform.position.y < LevelGenerator.lowestPlatformPos) {
            LevelGenerator.lowestPlatformPos = LevelGenerator.lastPlatform.transform.position.y;
        }


        //generate basic unmoving platform
        if (LevelGenerator.lastPlatform.tag == "PlatformBase")
        {
            //Scale Object from its script based on tag
            StationaryPlatform currentScript = LevelGenerator.lastPlatform.GetComponent<StationaryPlatform>();
            //currentScript.parentTransform = transform;
            float randomScale = CalculatePlatformScale();
            currentScript.SetPlatformScale(randomScale);

            //Move pointer Object Based on it's scale and height location
            //levelPlatformPointer += currentScript.transform.localPosition + currentScript.transform.GetChild(0).transform.localPosition;
            LevelGenerator.levelPlatformPointer = LevelGenerator.lastPlatform.transform.position + new Vector3(randomScale / 2, 0, 0); //now the endpoint of the platform should be the location of the pointer
            //levelPlatformPointer = currentScript.GetEndPoint();
        }

        //generate horizontally moving platform
        else if (LevelGenerator.lastPlatform.tag == "MovingPlatformBase")
        {
            MovingPlatform currentScript = LevelGenerator.lastPlatform.GetComponent<MovingPlatform>();
            float randomScale = CalculatePlatformScale();
            LevelGenerator.lastPlatform.transform.localScale = new Vector3(randomScale, 0.5f, 1f);
            float rand = Random.Range(1f, 4f);
            currentScript.setPos1X(LevelGenerator.levelPlatformPointer.x);
            currentScript.setPos2X(LevelGenerator.levelPlatformPointer.x + randomScale*2);
            LevelGenerator.levelPlatformPointer += new Vector3(randomScale*2, 0, 0); //now the endpoint of the platform should be the location of the pointer
            
        }
        
        //generate vertically moving platform
        else if (LevelGenerator.lastPlatform.tag == "VerticalMovingPlatformbase")
        {
            VerticalMovingPlatform currentScript = LevelGenerator.lastPlatform.GetComponent<VerticalMovingPlatform>();
            float randomScale = CalculatePlatformScale();
            LevelGenerator.lastPlatform.transform.localScale = new Vector3(randomScale, 0.5f, 1f);
            float rand = Random.Range(1f, 4f);
            currentScript.setPos1Y(LevelGenerator.levelPlatformPointer.y -Random.Range(1f, 4f));
            currentScript.setPos2Y(LevelGenerator.levelPlatformPointer.y+Random.Range(1f, 4f));
            LevelGenerator.levelPlatformPointer += new Vector3(randomScale * 2, 0, 0); //now the endpoint of the platform should be the location of the pointer
        }

        //Move pointer Object Based on platform gap
        LevelGenerator.levelPlatformPointer += new Vector3(CalculatePlatformGap(), 0f, 0f);
        LevelGenerator.lastPlatform.transform.position = LevelGenerator.levelPlatformPointer;

        if (LevelGenerator.flagFirst == 0)
        {
            //LevelGenerator.startPos = levelPlatformPointer+ new Vector3(randomScale / 2, 0, 0);
            LevelGenerator.startPos = LevelGenerator.lastPlatform.transform.position;
            LevelGenerator.flagFirst = 1;
        }


        //Add Instantiated Platform to Current private List of platforms in the Level Part
        platformList.Add(LevelGenerator.lastPlatform.transform);

        currentPlatform.transform.SetParent(transform);

    }

    private float CalculatePlatformGap() //create a gap between this platform to distance it from the next platform
    {
        return Random.Range(0f, 7f);
    }

    private float CalculatePlatformScale() //generate a random scale for the platform
    {
        float lengthLeft = System.Math.Abs(levelPartLength - LevelGenerator.levelPlatformPointer.x);
        //return Random.Range(3f, (float)(lengthLeft / 2));
        return Random.Range(3f, 10f);

        //return Random.Range(3f, (float)(levelPartLength));
    }

    private float CalculatePlatformHeight() //modify platform's height (y position)
    {
        switch(Random.Range(0,2))
        {
            case 1:
                //Platform Above
                //Debug.Log("High");
                return Random.Range(0f, maxPlatformVerticalSpace);
            case 0:
                //Platform Below
                //Debug.Log("Low");
                //return -(Random.Range(0f, 10f));
                return -(Random.Range(0f, maxPlatformVerticalSpace));
            default:
                return 0f;
        }
    }


}
