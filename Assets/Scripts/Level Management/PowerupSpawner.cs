using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupSpawner : NetworkBehaviour
{
    public static PowerupSpawner Instance = null;

    //Prefabs
    public List<GameObject> Powerups;

    //Map Boundaries
    public int minX;

    public int maxX;
    public int minZ;
    public int maxZ;

    private bool levelLoaded;

    //Spawn Settings
    private List<Vector3> randomSpawnPositions = new List<Vector3>();

    public int spawnHeight;
    public float spawnRate = 1.0f;
    public GameObject PowerupGroupPrefab;
    public float difficultyRating = 50f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public override void OnStartServer()
    {
    }

    void Update()
    {
        //Start
        if (LevelManager.Instance != null && !levelLoaded)
        {
            levelLoaded = true;
            StartLevel();
        }
    }

    public void StartLevel()
    {
        spawnPowerups(LevelManager.Instance.level);
    }

    public void spawnPowerups(int levelNumber)
    {
        //Algorithm to find quantity to spawn
        int numPowerups = levelNumber * (NetworkServer.connections.Count / 2);

        //Procedural Seeding based on levelNumber
        Random.InitState(levelNumber);

        Debug.Log("Num of Powerups: " + numPowerups);

        for (int i = 0; i < numPowerups; i++)
        {
            //Grab random position and powerup
            Vector3 randPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ));
            int powerupIndex = Random.Range(0, Powerups.Count);

            GameObject newPowerup = Instantiate(Powerups[powerupIndex], randPosition, Quaternion.identity);
            NetworkServer.Spawn(newPowerup);

            //Group the powerups
            newPowerup.transform.SetParent(transform);
        }
    }
}