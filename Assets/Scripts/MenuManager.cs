using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance = null;

    //Pause Menu
    private bool paused;

    public GameObject PauseMenu;

    // Use this for initialization
    void Start()
    {
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
}