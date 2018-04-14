using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnucklesSpawner : MonoBehaviour {
	
	//Spawn Settings
	public GameObject Knuckles;
	private List<Vector3> randomSpawnPositions = new List<Vector3>();
	public float spawnRate = 1.0f;
	private float nextSpawn;
	
	// Use this for initialization
	void Awake () {
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
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
			int index = Random.Range(0, randomSpawnPositions.Count);
			Knuckles.transform.root.position = randomSpawnPositions[index];
			Instantiate(Knuckles);
		}
	}
	
}
