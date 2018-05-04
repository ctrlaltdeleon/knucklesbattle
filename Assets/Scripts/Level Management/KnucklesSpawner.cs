using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KnucklesSpawner : NetworkBehaviour
{
    //Instance
    public static KnucklesSpawner Instance = null;

    //Prefabs
    public List<GameObject> Knuckles;

    //Map Boundaries
    public int minX;

    public int maxX;

    //Spawn Settings
    private List<Vector3> randomSpawnPositions = new List<Vector3>();

    public int spawnHeight;
    public float spawnRate = 1.0f;
    public float difficultyRating = 50f;
    [SyncVar] public int numKnuckles = 0;
    [SyncVar] public int maxNumKnuckles = 0;

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

        DontDestroyOnLoad(gameObject);
    }

    public override void OnStartServer()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        int levelNumber = LevelManager.Instance.level;
        maxNumKnuckles = (int) Mathf.Log(levelNumber * difficultyRating * NetworkServer.connections.Count, 2f);
        LevelManager.Instance.totalNumMonsters += maxNumKnuckles;
        LevelManager.Instance.numMonsters = maxNumKnuckles;
        Random.InitState(levelNumber);
        numKnuckles = 0;
        GenerateSpawnPoints();
        InvokeRepeating("spawnKnuckles", 1f, spawnRate);
    }

    public void GenerateSpawnPoints()
    {
        //Number to spawn
        Debug.Log("Num of Knuckles: " + maxNumKnuckles);

        for (int i = 0; i < maxNumKnuckles; i++)
        {
            //Four sided Map Spawn Points
            float randomPos = Random.Range(minX, maxX);
            randomSpawnPositions.Add(new Vector3(minX, spawnHeight, randomPos));
            randomSpawnPositions.Add(new Vector3(maxX, spawnHeight, randomPos));
            randomSpawnPositions.Add(new Vector3(randomPos, spawnHeight, minX));
            randomSpawnPositions.Add(new Vector3(randomPos, spawnHeight, maxX));
        }
    }

    public void spawnKnuckles()
    {
        if (numKnuckles < maxNumKnuckles)
        {
            //Randomly instantiate from a spawn point
            int index = Random.Range(0, randomSpawnPositions.Count);
            int randomColor = Random.Range(0, Knuckles.Count);
            GameObject newKnuckles =
                Instantiate(Knuckles[randomColor], randomSpawnPositions[index], Quaternion.identity);
            NetworkServer.Spawn(newKnuckles);

            //Group the knuckles
            newKnuckles.transform.SetParent(transform);
            numKnuckles++;
        }
    }
}