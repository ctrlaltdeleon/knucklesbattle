using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecraftGround : MonoBehaviour
{
    //Map Boundaries
    public int minX;

    public int maxX;

    // Prefabs
    public GameObject[] blocks;

    public GameObject[] monsters;

    private bool levelLoaded;

    // Terrain Settings
    public int groundHeight;

    public float terrainDetail;
    public float terrainHeight;
    public int terrainMinHeight;
    public int seed;


    void Update()
    {
        //Start
        if (LevelManager.Instance != null && !levelLoaded)
        {
            levelLoaded = true;
            seed = LevelManager.Instance.level;
            GenerateTerrain();
        }
    }

    // Update is called once per frame
    void GenerateTerrain()
    {
        for (var x = minX; x < maxX; x++)
        {
            for (var z = minX; z < maxX; z++)
            {
                int maxY = (int) (Mathf.PerlinNoise(
                                      (x / 2 + seed) / terrainDetail,
                                      (z / 2 + seed) / terrainDetail) * terrainHeight);
                maxY += groundHeight;

                // Grass & Dirt Creation
                GameObject grassBlock = Instantiate(blocks[Random.Range(0, 2)], new Vector3(x, maxY, z),
                    Quaternion.identity);
                grassBlock.transform.SetParent(transform);

                // Underlayers Creation
                for (int y = terrainMinHeight; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if (y >= maxY - dirtLayers)
                    {
                        GameObject dirtBlock = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity);
                        dirtBlock.transform.SetParent(transform);
                    }
                    else
                    {
                        GameObject stoneBlock = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity);
                        stoneBlock.transform.SetParent(transform);
                    }
                }
            }
        }
    }
}