using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    public List<Transform> levelPartsList;
    

    [SerializeField]
    public Transform deathBarrier;

    [SerializeField]
    public Transform endZone;

    public float levelPartLength = 50f;
    public float maxPlatformVerticalSpace = 10f;
    public float maxPlatformHorizontalSpace = 10f;
    public float numberOfPlatformsPerlevelPart = 10f;

    public static GameObject lastPlatform;
    public static Vector3 levelPlatformPointer = new Vector3(0, 0, 0);
    public static float lowestPlatformPos = 100;
    public GameObject playercharacter;

    public GameObject currentPlayerCharacter;
    public Transform level1;
    
    public static int flagFirst = 0;
    public static Vector3 startPos;
    float timer = 0f;
    int flag = 0;

    //private float currentLevelPartPointer = 0f;
    private List<Transform> CurrentLevelParts = new List<Transform>();

    private float currentLevelPart_x = 0f;

    private int originalNumberOfLevelParts;

    private void Awake()
    { //create 10 sections
        levelPartsList = new List<Transform>(10);
        for (int i = 0; i < levelPartsList.Capacity; i++) {
            levelPartsList.Add(level1);
        }
        originalNumberOfLevelParts = levelPartsList.Count;
        lastPlatform = gameObject;
        while(levelPartsList.Count != 0) {
            SpawnLevelPart(new Vector3(currentLevelPart_x, 0, 0));
            currentLevelPart_x += levelPartLength;
            
        }


    }

    // Start is called before the first frame update
    void Start()
    {

        /*GameObject currentCharacter = GameObject.FindWithTag("Player");
        if (currentCharacter != null)
        {
            //print("zzz " + CurrentLevelParts.Count);
            LevelPartGenerator currentScript = CurrentLevelParts[0].GetComponent<LevelPartGenerator>();
            Vector3 firstPlatformCoordinate = currentScript.platformList[0].transform.position;
            //Debug.Log(currentCharacter.transform.position + "======================");
            //print(firstPlatformCoordinate.x + 1f);
            //print(firstPlatformCoordinate.y + 0.5f);
            //print(firstPlatformCoordinate.z);
            currentCharacter.transform.position = new Vector3(firstPlatformCoordinate.x + 1f, firstPlatformCoordinate.y + 0.5f, firstPlatformCoordinate.z);
            print("moved");
            //Debug.Log(currentCharacter.transform.position + "======================");
        }*/

        //Move Character Object to start of First Platform

    }

    // Update is called once per frame
    void Update()
    {
        //create deathbarrier and instantiate important objects such as the player and death zone. It's done after a 0.5s delay to allow the level to generate. In hindsight it would have been better to create some flag to allow this code to run
        //immediately after the level is generated, but it's a small enough game that the generation is finished quickly and 0.5s is not a long wait.
        if (flag == 0) {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0;
                SetDeathBarrier();
                var obj = (GameObject)Instantiate(playercharacter, startPos+new Vector3(0, 3, 0), Quaternion.identity);
                currentPlayerCharacter = obj;
                CameraMovement.player = obj.gameObject;
                flag = 1;
            }   
        }
    }
    private void SpawnLevelPart(Vector3 spawnPosition)//create level section and set important variables
    {

        int stageMaxInt = levelPartsList.Count - 1;
        int stageInt = Random.Range(0, stageMaxInt);

        var currentLevelPart = Instantiate(levelPartsList[stageInt], spawnPosition, Quaternion.identity);
        LevelPartGenerator currentLevelPartScript = currentLevelPart.GetComponent<LevelPartGenerator>();
        currentLevelPartScript.parentTransform = transform;
        currentLevelPartScript.levelPartLength = levelPartLength;
        currentLevelPartScript.endLevelPartVector = spawnPosition + new Vector3(levelPartLength, 0f, 0f);
        currentLevelPartScript.maxPlatformVerticalSpace = maxPlatformVerticalSpace;
        currentLevelPartScript.maxPlatformHorizontalSpace = maxPlatformHorizontalSpace;
        currentLevelPartScript.numberOfPlatformsPerlevelPart = numberOfPlatformsPerlevelPart;

        levelPartsList.RemoveAt(stageInt);
        //CurrentLevelParts.Add(currentLevelPart);
        CurrentLevelPartChange(currentLevelPart);

        

    }

    private void CurrentLevelPartChange(Transform currentLevelPart)
    {
        if (CurrentLevelParts == null)
        {
            CurrentLevelParts.Add(currentLevelPart);
        }
        else
        {
            switch (CurrentLevelParts.Count)
            {
            
            case 0: //No Level Parts, add current one
                CurrentLevelParts.Add(currentLevelPart);
                break;
            case 1: //One Level Parts, add next one to current one
                CurrentLevelParts.Add(currentLevelPart);
                break;
            case 2: //Two Level Parts, add next one to current one
                CurrentLevelParts.Add(currentLevelPart);
                break;
            case 3: //Three Level Parts, add next one to current on, current one
                CurrentLevelParts.RemoveAt(0);
                CurrentLevelParts.Add(currentLevelPart);
                break;
            }
        }
    }

    //create the death barrier at the bottom of the level
    private void SetDeathBarrier()
    {
        var db = Instantiate(deathBarrier, transform.position, Quaternion.identity);
        db.localScale = new Vector3((float)(levelPartLength * originalNumberOfLevelParts), db.localScale.y, db.localScale.z);
        db.transform.position += new Vector3(db.localScale.x/2, 0f, 0f);

        Vector3 lowestVector = new Vector3(db.localScale.x / 2, 0f, 0f);

        foreach (var part in CurrentLevelParts)
        {
            LevelPartGenerator partScript = part.GetComponent<LevelPartGenerator>();
            if(partScript != null)
            {
                foreach(var platform in partScript.platformList)
                {
                    if(platform.transform.position.y < lowestVector.y)
                    {
                        lowestVector = new Vector3(lowestVector.x, platform.transform.position.y, lowestVector.z); //keep track of the lowest platform to create the deathzone under it
                    }
                }
            }  
        }

        db.transform.position = lowestVector + new Vector3(0f, -10f, 0f);

        if (levelPartsList.Count == 0) //add the ending to the level after the 10 sections
        {
            var endPart = Instantiate(endZone, new Vector3(currentLevelPart_x, db.transform.position.y, 0), Quaternion.identity);
            endPart.localScale = new Vector3((float)(levelPartLength / 2), endPart.localScale.y, endPart.localScale.z);
            endPart.transform.position += new Vector3(endPart.localScale.x / 2, 0f, 0f);
            //var eCol = endPart.GetComponent<BoxCollider>();
            //eCol.isTrigger = true;
        }

    }


}
