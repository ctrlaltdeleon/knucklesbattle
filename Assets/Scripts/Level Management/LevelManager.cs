using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance = null;

    //Game State
    [SyncVar] public int level = 1;

    [SyncVar] public int numMonsters = 1;


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

    // Update is called once per frame
    void Update()
    {
        if (numMonsters <= 0)
        {
            WonLevel();
        }
    }

    public void DestroyEnemy(GameObject knuckles)
    {
        CmdDestroyKnuckles(knuckles);
    }

    [Command]
    void CmdDestroyKnuckles(GameObject knuckles)
    {
        NetworkServer.Destroy(knuckles);
    }

    public void WonLevel()
    {
        level++;
        if (level > 10)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        Debug.Log("Start new Level " + level);
        GameObject ks = GameObject.FindGameObjectWithTag("KnucklesGroup");
        ks.GetComponent<KnucklesSpawner>().StartLevel();
        GameObject ps = GameObject.FindGameObjectWithTag("PowerupGroup");
        ps.GetComponent<PowerupSpawner>().StartLevel();
        if (KnucklesSpawner.Instance != null)
        {
            KnucklesSpawner.Instance.StartLevel();
        }
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}