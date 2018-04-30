using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    //Prefabs
    public List<GameObject> Powerups;

    //Map Boundaries
    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;

    //Spawn Settings
    private List<Vector3> randomSpawnPositions = new List<Vector3>();
    public int spawnHeight;
    public float spawnRate = 1.0f;
    public GameObject PowerupGroupPrefab;
    public float difficultyRating = 50f;
    

    private void Start()
    {
        spawnPowerups(GameManager.Instance.level);
    }

    public void StartLevel()
    {
        spawnPowerups(GameManager.Instance.level);
    }

    public void spawnPowerups(int levelNumber)
    {

        //Algorithm to find quantity to spawn
        int numPowerups = (int) Mathf.Log(levelNumber * difficultyRating, 2f);

        //Procedural Seeding based on levelNumber
        Random.InitState(levelNumber);

        Debug.Log("Num of Powerups: " + numPowerups);

        for (int i = 0; i < numPowerups; i++)
        {
            //Grab random position and powerup
            Vector3 randPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ));
            int powerupIndex = Random.Range(0, Powerups.Count);

            GameObject newPowerup = Instantiate(Powerups[powerupIndex], randPosition, Quaternion.identity);

            //Group the powerups
            newPowerup.transform.SetParent(transform);
        }
    }
}