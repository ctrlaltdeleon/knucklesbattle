using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class MenuManager : MonoBehaviour
{
    //Pause Menu
    private bool paused;

    public GameObject PauseMenu;
    public GameObject WonMenuPrefab;
    public GameObject LoseMenuPrefab;

    void Start()
    {
        Debug.Log("START MENU");
        if (LevelManager.Instance)
        {
            Debug.Log("level instance!");
            if (LevelManager.Instance.win)
            {
                WonGame();
            }
            else
            {
                Debug.Log("LOSE");
                LoseGame();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
    }

    //Public Interfaces
    //-----------------
    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        PauseMenu.SetActive(true);
    }


    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        PauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void WonGame()
    {
        LobbyManager.s_Singleton.StopHost();
        NetworkManager.Shutdown();
        GameObject WonMenu = Instantiate(WonMenuPrefab);
        WonMenu.SetActive(true);
        WonMenu.transform.GetChild(3).GetComponent<Text>().text =
            "Max level: " + LevelManager.Instance.level + "\nTotal knuckles defeated: " +
            LevelManager.Instance.totalNumMonsters;
        Destroy(GameManager.Instance.gameObject);
        Destroy(LevelManager.Instance.gameObject);
        Destroy(KnucklesSpawner.Instance.gameObject);
        Destroy(PowerupSpawner.Instance.gameObject);
        DestroyObject(LobbyManager.s_Singleton.gameObject);
    }

    public void LoseGame()
    {
        LobbyManager.s_Singleton.StopHost();
        NetworkManager.Shutdown();
        GameObject LoseMenu = Instantiate(LoseMenuPrefab);
        LoseMenu.SetActive(true);
        Debug.Log("Level: " + LevelManager.Instance.level);
        Debug.Log("total knuckles defeated: " +
                  (LevelManager.Instance.totalNumMonsters - LevelManager.Instance.numMonsters));
        LoseMenu.transform.GetChild(3).GetComponent<Text>().text =
            "Max level: " + LevelManager.Instance.level + "\nTotal knuckles defeated: " +
            (LevelManager.Instance.totalNumMonsters - LevelManager.Instance.numMonsters);
        Destroy(GameManager.Instance.gameObject);
        Destroy(LevelManager.Instance.gameObject);
        Destroy(KnucklesSpawner.Instance.gameObject);
        Destroy(PowerupSpawner.Instance.gameObject);
        DestroyObject(LobbyManager.s_Singleton.gameObject);
    }
}