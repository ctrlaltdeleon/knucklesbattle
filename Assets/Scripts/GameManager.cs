using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : NetworkBehaviour
{
    public enum CoordinateDirection {
        POS_Z_FORWARD,
        POS_X_FORWARD,
        NEG_Z_FORWARD,
        NEG_X_FORWARD
    };

    //Global Instance
    public static GameManager Instance = null;
	
    //Game State
    public int level = 1;
    public int numMonsters = 0;
    
    //Pause Menu
    private bool paused;
    public GameObject PauseMenu;

    //Direction Management
    private CoordinateDirection m_coordinateDirection;
    public CoordinateDirection CoordDirection { get { return m_coordinateDirection; } }
    
    //Spawners
    public KnucklesSpawner KnucklesSpawner;
    public PowerupSpawner PowerupSpawner;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
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

    [Server]
    public void WonLevel()
    {
        level++;
        if (level > 10)
        {
            SceneManager.LoadScene("Credits");
        }
        GameObject ks = GameObject.FindGameObjectWithTag("KnucklesGroup");
        ks.GetComponent<KnucklesSpawner>().StartLevel();
        GameObject ps = GameObject.FindGameObjectWithTag("PowerupGroup");
        ps.GetComponent<PowerupSpawner>().StartLevel();
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("MainMenu");
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

    //Coordinate Direction    
    /// <summary>
    /// Changes the coordinate direction.
    /// </summary>
    /// <param name="direction">The direction.</param>
    public void ChangeCoordinateDirection(int direction) {
        switch (Mathf.Abs(direction % 4)) {
            case 0:
                m_coordinateDirection = CoordinateDirection.POS_Z_FORWARD;
                break;
            case 1:
                m_coordinateDirection = CoordinateDirection.POS_X_FORWARD;
                break;
            case 2:
                m_coordinateDirection = CoordinateDirection.NEG_Z_FORWARD;
                break;
            case 3:
                m_coordinateDirection = CoordinateDirection.NEG_X_FORWARD;
                break;
        }
    }
}