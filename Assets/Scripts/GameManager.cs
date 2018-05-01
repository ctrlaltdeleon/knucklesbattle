using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    //Global Instance
    public static GameManager Instance = null;

    public GameObject LevelManagerPrefab;
    private GameObject LevelManagerGO;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public override void OnStartServer()
    {
        LevelManagerGO = Instantiate(LevelManagerPrefab);
        NetworkServer.Spawn(LevelManagerGO);
    }

    void Update()
    {
    }
}