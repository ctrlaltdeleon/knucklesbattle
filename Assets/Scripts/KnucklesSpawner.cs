using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnucklesSpawner : MonoBehaviour {
	
	//Prefabs
	public GameObject Knuckles;
	
	//Map Boundaries
	public int minX;
	public int maxX;
	public int minZ;
	public int maxZ;
	
	//Spawn Settings
	private List<Vector3> randomSpawnPositions = new List<Vector3>();
	public int spawnHeight;
	public float spawnRate = 1.0f;
	public GameObject KnucklesGroup;
	public float difficultyRating = 50f;
	
	//Can Spawn
	public bool canSpawn = false;
	
	public void GenerateSpawnPoints (int levelNumber) {
		//Number to spawn
		int numKnuckles = (int)Mathf.Log(levelNumber * difficultyRating, 2f);
		
		Debug.Log("Num of Knuckles: " + numKnuckles);
		
		for (int i = 0; i < numKnuckles; i++)
		{
			//Procedural Seeding based on levelNumber
			Random.InitState(levelNumber);
			
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
		if (canSpawn)
		{	
			Debug.Log("Spawning ~");
			//Randomly instantiate from a spawn point
			int index = Random.Range(0, randomSpawnPositions.Count);
			GameObject newKnuckles = Instantiate(Knuckles, randomSpawnPositions[index], Quaternion.identity);
			
			//Group the knuckles
			newKnuckles.transform.SetParent(KnucklesGroup.transform);
		}
	}
	
	
}
