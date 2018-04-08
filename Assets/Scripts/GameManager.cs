using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject Knuckles;
    private List<Vector3> randomSpawnPositions = new List<Vector3>();
    public float spawnRate = 1.0f;
    private float nextSpawn;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        nextSpawn = 0.0f;
        for (int i = 0; i < 30; i++)
        {
            float randomPos = Random.Range(-250, 250);
            randomSpawnPositions.Add(new Vector3(-250, 0, randomPos));
            randomSpawnPositions.Add(new Vector3(250, 0, randomPos));
            randomSpawnPositions.Add(new Vector3(randomPos, 0, -250));
            randomSpawnPositions.Add(new Vector3(randomPos, 0, 250));
        }
    }


    void Update()
    {
        if (Time.time > nextSpawn)
        {
            Debug.Log("creating Knuckles");
            nextSpawn = Time.time + spawnRate;
            int index = Random.Range(0, randomSpawnPositions.Count);
            Knuckles.transform.root.position = randomSpawnPositions[index];
            Instantiate(Knuckles);
        }
    }
}