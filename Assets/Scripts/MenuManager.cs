using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine;
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
        if (LevelManager.Instance)
        {
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
        GameObject WonMenu = Instantiate(WonMenuPrefab);
        WonMenu.SetActive(true);
        WonMenu.transform.GetChild(3).GetComponent<Text>().text = "Max level: " + LevelManager.Instance.level;
        WonMenu.transform.GetChild(4).GetComponent<Text>().text =
            "Total knuckles defeated: " + KnucklesSpawner.Instance.totalNumKnuckles;
        Destroy(LevelManager.Instance.gameObject);
        Destroy(KnucklesSpawner.Instance.gameObject);
        Destroy(LobbyManager.s_Singleton);
    }

    public void LoseGame()
    {
        GameObject LoseMenu = Instantiate(LoseMenuPrefab);
        LoseMenu.SetActive(true);
        LoseMenu.transform.GetChild(3).GetComponent<Text>().text = "Max level: " + LevelManager.Instance.level;
        LoseMenu.transform.GetChild(4).GetComponent<Text>().text =
            "Total knuckles defeated: " +
            (KnucklesSpawner.Instance.totalNumKnuckles - KnucklesSpawner.Instance.numKnuckles);
        Destroy(LevelManager.Instance.gameObject);
        Destroy(KnucklesSpawner.Instance.gameObject);
        Destroy(LobbyManager.s_Singleton);
    }
}