using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance = null;

    //Game State
    [SyncVar] public int level = 1;

    [SyncVar] public int numMonsters = 1;
    [SyncVar] public int totalNumMonsters = 0;
    [SyncVar] public bool win;
    public AudioSource audioSource;
    public AudioClip nextLevelSound;


    // Use this for initialization
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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        if (numMonsters <= 0)
        {
            Debug.Log("From the server, emit WON LEVEL");
            CmdWonLevel();
        }
    }

//    public void DestroyEnemy(GameObject knuckles)
//    {
//        CmdDestroyKnuckles(knuckles);
//    }
//
//    [Command]
//    void CmdDestroyKnuckles(GameObject knuckles)
//    {
//        NetworkServer.Destroy(knuckles);
//    }

    [Command]
    public void CmdWonLevel()
    {
        level++;
        audioSource.PlayOneShot(nextLevelSound);
        if (level > 1)
        {
            RpcWonGame();
            return;
        }
        Debug.Log("Start new Level " + level);
        if (KnucklesSpawner.Instance != null)
        {
            KnucklesSpawner.Instance.StartLevel();
            PowerupSpawner.Instance.StartLevel();
        }
    }

    [ClientRpc]
    public void RpcWonGame()
    {
        win = true;
        SceneManager.LoadScene("MainMenu");
    }

    [Command]
    public void CmdLoseGame()
    {
        RpcLoseGame();
    }

    [ClientRpc]
    public void RpcLoseGame()
    {
        win = false;
        SceneManager.LoadScene("MainMenu");
    }
}