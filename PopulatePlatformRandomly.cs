using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulatePlatformRandomly : MonoBehaviour
{
    public int level = 1;
    public bool platformMovesHorizontally = false;

    public int oddsEnemyInTen = 2;
    public int oddsCollectableInTen = 4;
    public int oddsPowerupInTen = 2;
    public GameObject [] enemies = new GameObject[5];
    public GameObject [] collectables = new GameObject[15];
    public GameObject [] powerups = new GameObject[5];
    public float howFarAbovePlatformEnemy = 1.5f;
    public float howFarAbovePlatformCollectable = 1f;
    public float howFarAbovePlatformPowerup = .5f;

    /*   
  public bool[] validEnemyLevel1;// = new bool[5] { true, true, true, true, true };
  public bool[] validEnemyLevel2;// = new bool[5] { true, true, true, true, true };
  public bool[] validEnemyLevel3;// = new bool[5] { true, true, true, true, true };
  public bool[] validCollectableLevel1;// = new bool[15] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
  public bool[] validCollectableLevel2;// = new bool[15] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
  public bool[] validCollectableLevel3;// = new bool[15] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
  public bool[] validPowerupLevel1;// = new bool[5] { true, true, true, true, true };
  public bool[] validPowerupLevel2;// = new bool[5] { true, true, true, true, true };
  public bool[] validPowerupLevel3;// = new bool[5] { true, true, true, true, true };

   public GameObject enemy1;
   public GameObject enemy2;
   public GameObject enemy3;
   public GameObject enemy4;
   public GameObject enemy5;
   public int numEnemies = 5;

   public GameObject collectable1;
   public GameObject collectable2;
   public GameObject collectable3;
   public GameObject collectable4;
   public GameObject collectable5;
   public GameObject collectable6;
   public GameObject collectable7;
   public GameObject collectable8;
   public GameObject collectable9;
   public GameObject collectable10;
   public GameObject collectable11;
   public GameObject collectable12;
   public GameObject collectable13;
   public GameObject collectable14;
   public GameObject collectable15;
   public int numCollectables = 15;

   public GameObject powerup1;
   public GameObject powerup2;
   public GameObject powerup3;
   public GameObject powerup4;
   public GameObject powerup5;
   public int numPowerups = 5;*/


    /* public bool[] validEnemyLevel1 = new bool[numEnemies] { true, true, true, true, true };
     public bool[] validEnemyLevel2 = new bool[numEnemies] { true, true, true, true, true };
     public bool[] validEnemyLevel3 = new bool[numEnemies] { true, true, true, true, true };
     public bool[] validCollectableLevel1 = new bool[numCollectables] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
     public bool[] validCollectableLevel2 = new bool[numCollectables] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
     public bool[] validCollectableLevel3 = new bool[numCollectables] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
     public bool[] validPowerupLevel1 = new bool[numPowerups] { true, true, true, true, true };
     public bool[] validPowerupLevel2 = new bool[numPowerups] { true, true, true, true, true };
     public bool[] validPowerupLevel3 = new bool[numPowerups] { true, true, true, true, true };
    */

    public bool[] validEnemyLevel1 = new bool [5] { true, true, true, true, true };
    public bool[] validEnemyLevel2 = new bool [5] { true, true, true, true, true };
    public bool[] validEnemyLevel3 = new bool [5] { true, true, true, true, true };
    public bool[] validCollectableLevel1 = new bool [15] { true, true, true, true, true,   true, true, true, true, true,   true, true, true, true, true };
    public bool[] validCollectableLevel2 = new bool [15] { true, true, true, true, true,   true, true, true, true, true,   true, true, true, true, true };
    public bool[] validCollectableLevel3 = new bool [15] { true, true, true, true, true,   true, true, true, true, true,   true, true, true, true, true };
    public bool[] validPowerupLevel1 = new bool [5] { true, true, true, true, true };
    public bool[] validPowerupLevel2 = new bool [5] { true, true, true, true, true };
    public bool[] validPowerupLevel3 = new bool [5] { true, true, true, true, true };



    // remaining odds are platform is blank

    // Start is called before the first frame update
    void Start()
    {
        int numEnemies = enemies.Length;
        int numCollectables = collectables.Length;
        int numPowerups = powerups.Length;
        ///TODO: NEED TO GET LEVEL FROM LEVEL OBJECT
        ///PLACEHOLDER set to 1 always

        bool[] validEnemy;
        bool[] validCollectable;
        bool[] validPowerup;
        switch (level) {
            case 1:
                validEnemy = validEnemyLevel1;
                validCollectable = validCollectableLevel1;
                validPowerup = validPowerupLevel1;
                break;
            case 2:
                validEnemy = validEnemyLevel2;
                validCollectable = validCollectableLevel2;
                validPowerup = validPowerupLevel2;
                break;
            case 3:
                validEnemy = validEnemyLevel3;
                validCollectable = validCollectableLevel3;
                validPowerup = validPowerupLevel3;
                break;
            default:
                validEnemy = validEnemyLevel1;
                validCollectable = validCollectableLevel1;
                validPowerup = validPowerupLevel1;
                break;
        }


        //System.Random rnd = new System.Random();
        //int typeSpawn = rnd.Next(1, 11);
        //int typeSpawn = rnd.Next(1, 11);
        int typeSpawn = Random.Range(1, 11);
        bool itemCreated = false;
        Vector3 platform = this.transform.position;
        if (typeSpawn <= oddsEnemyInTen) {
            while (!itemCreated) {
               // typeSpawn = rnd.Next(0, numEnemies);
                //typeSpawn = rnd.Next(0, numEnemies);
                typeSpawn = Random.Range(0, numEnemies);
                if (validEnemy[typeSpawn]) {
                    Instantiate(enemies[typeSpawn], new Vector3(platform.x, (platform.y + howFarAbovePlatformEnemy), platform.z), Quaternion.identity);
            //        if (createTheEnemy(typeSpawn))
                        itemCreated = true;
                }
            }
        }
        else if (typeSpawn <= (oddsEnemyInTen + oddsCollectableInTen)) {
            while (!itemCreated)
            {
              //  typeSpawn = rnd.Next(0, numCollectables);
                //typeSpawn = rnd.Next(0, numCollectables);
                typeSpawn = Random.Range(0, numCollectables);
                if (validCollectable[typeSpawn])
                {
                    Instantiate(collectables[typeSpawn], new Vector3(platform.x, (platform.y + howFarAbovePlatformCollectable), platform.z), Quaternion.identity);
            //        if(createTheCollectable(typeSpawn))
                        itemCreated = true;
                }
            }
        }
        else if (typeSpawn <= (oddsEnemyInTen + oddsCollectableInTen + oddsPowerupInTen)) {
            while (!itemCreated)
            {
               // typeSpawn = rnd.Next(0, numPowerups);
                //typeSpawn = rnd.Next(0, numPowerups);
                typeSpawn = Random.Range(0, numPowerups);
                if (validPowerup[typeSpawn])
                {
                    Instantiate(powerups[typeSpawn], new Vector3(platform.x, (platform.y + howFarAbovePlatformPowerup), platform.z), Quaternion.identity);
            //        if(createThePowerup(typeSpawn))
                        itemCreated = true;
                }
            }
        }
    }



/*    public bool createTheEnemy(int typeSpawn) {
        Vector3 platform = this.transform.position;

        switch (typeSpawn)
        {
            case 1:
                Instantiate(enemy1, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 2:
                Instantiate(enemy2, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 3:
                Instantiate(enemy3, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 4:
                Instantiate(enemy4, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 5:
                Instantiate(enemy5, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            default:
                return false;
                break;
        }
    } 



    public bool createTheCollectable(int typeSpawn) { 
        Vector3 platform = this.transform.position;
        //bool created = false;
        switch (typeSpawn)
        {
            case 1:
                Instantiate(collectable1, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 2:
                Instantiate(collectable2, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 3:
                Instantiate(collectable3, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 4:
                Instantiate(collectable4, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 5:
                Instantiate(collectable5, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 6:
                Instantiate(collectable6, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 7:
                Instantiate(collectable7, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 8:
                Instantiate(collectable8, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 9:
                Instantiate(collectable9, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 10:
                Instantiate(collectable10, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 11:
                Instantiate(collectable11, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 12:
                Instantiate(collectable12, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 13:
                Instantiate(collectable13, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 14:
                Instantiate(collectable14, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 15:
                Instantiate(collectable15, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            default:
                return false;
                break;
        }
    }


    public bool createThePowerup(int typeSpawn)
    {
        Vector3 platform = this.transform.position;

        switch (typeSpawn)
        {
            case 1:
                Instantiate(powerup1, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 2:
                Instantiate(powerup2, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 3:
                Instantiate(powerup3, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 4:
                Instantiate(powerup4, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            case 5:
                Instantiate(powerup5, new Vector3(platform.x, (platform.y + howFarAbovePlatform), platform.z), Quaternion.identity);
                return true;
                break;
            default:
                return false;
                break;
        }
    } */
}
