using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
	public enum GameState : uint {
		START,
		GAME,
		END
	}
    public enum CoordinateDirection {
        POS_Z_FORWARD,
        POS_X_FORWARD,
        NEG_Z_FORWARD,
        NEG_X_FORWARD
    };

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
	private int m_currentLevel = 0;

	private bool powerupSpawned = false;
	private GameState m_currentState = GameState.START;

    private CoordinateDirection m_coordinateDirection;

    public CoordinateDirection CoordDirection { get { return m_coordinateDirection; } }
    
    //Spawners
    public KnucklesSpawner KnucklesSpawner;
    public PowerupSpawner PowerupSpawner;

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
    }

	public void StartGame(string firstLevel)
    {
        Debug.Log("suh");
		SceneManager.LoadScene (firstLevel);
		//StartCoroutine (StartGame2 (firstLevel));
		//LoadLevel(levelNumber);
		m_currentState = GameState.GAME;
		powerupSpawned = true;
    }

	IEnumerator StartGame2(string firstLevel){
		SceneManager.LoadScene(firstLevel);
		yield return new WaitForSeconds (2);

	}

	void deferredWait(int level){
		LoadLevel (level);
	}

    public void LoadLevel(int seed)
    {
		
        //Prepare Knuckles and Powerups
        KnucklesSpawner.GenerateSpawnPoints(seed);
        KnucklesSpawner.canSpawn = true;
        PowerupSpawner.spawnPowerups(seed);
        
        //Procedurally Spawn Knuckles
        InvokeRepeating("GM_spawnKnuckles", 1.0f, KnucklesSpawner.spawnRate);
    }

    void GM_spawnKnuckles()
    {
        KnucklesSpawner.spawnKnuckles();
    }


    void Update()
    {
		if (m_currentState == GameState.GAME && powerupSpawned) {
			//TODO Powerup spawn here
			LoadLevel(1);
			powerupSpawned = false;
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    //Public Interfaces
    //-----------------
    public void WinGame()
    {
        KnucklesSpawner.canSpawn = false;
        
        //Increment Wave
        waveNumber += 1;
		
        //Move to next stage if all waves are finished
        if (waveNumber > waveLength)
        {
            waveNumber = 1;
            levelNumber += 1;
            LoadLevel(levelNumber);
        }
    }

    public void LoseGame()
    {
        //Reset Wave and Level
        levelNumber = 1;
        waveNumber = 1;
        StartGame("Level1");
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