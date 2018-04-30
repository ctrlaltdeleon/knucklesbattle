using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KnucklesSpawner : NetworkBehaviour
{
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


    public override void OnStartServer()
    {
        GenerateSpawnPoints();
        InvokeRepeating("spawnKnuckles", spawnRate, 30f);
    }

    public void GenerateSpawnPoints()
    {
        int levelNumber = GameManager.Instance.level;

        //Number to spawn
        int numKnuckles = (int) Mathf.Log(levelNumber * difficultyRating, 2f);
        Debug.Log("Num of Knuckles: " + numKnuckles);

        //Procedural Seeding based on levelNumber
        Random.InitState(levelNumber);

        for (int i = 0; i < numKnuckles; i++)
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
        //Randomly instantiate from a spawn point
        int index = Random.Range(0, randomSpawnPositions.Count);
        int randomColor = Random.Range(0, Knuckles.Count);
        Debug.Log("Knuckles color Number" + randomColor);
        GameObject newKnuckles = Instantiate(Knuckles[randomColor], randomSpawnPositions[index], Quaternion.identity);
        NetworkServer.Spawn(newKnuckles);

        //Group the knuckles
        newKnuckles.transform.SetParent(transform);
    }
}