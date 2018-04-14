using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    //Global Instance
    public static GameManager Instance = null;
	
    //Game Setup
    public int waveLength;
	
    //Game State
    private static int levelNumber;
    private static int waveNumber;
    
    //Pause Menu
    private bool paused;
    public GameObject PauseMenu;
    
    //Spawner
    public KnucklesSpawner kSpawn;

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
        levelNumber = 1;
        waveNumber = 1;
        LoadLevel();
    }

    public void StartGame(string firstLevel)
    {
        SceneManager.LoadScene(firstLevel);
    }

    void LoadLevel()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    //Public Interfaces
    //-----------------
    public void WinGame()
    {
        //Increment Wave
        waveNumber += 1;
		
        //Move to next stage if all waves are finished
        if (waveNumber > waveLength)
        {
            waveNumber = 1;
            levelNumber += 1;
            LoadLevel();
        }
    }

    public void LoseGame()
    {
        //Reset Wave and Level
        levelNumber = 1;
        waveNumber = 1;
        //TODO: Lose behavior
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
    }
	
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #endif
    }

}