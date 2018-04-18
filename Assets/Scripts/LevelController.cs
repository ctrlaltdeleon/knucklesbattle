using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	void Awake(){
		SceneManager.activeSceneChanged += OnLevelFinishedLoading;
	}

	void OnDestroy(){
		SceneManager.activeSceneChanged -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading (Scene previousScene, Scene newScene) {
		GameManager.Instance.LoadLevel (1);
	}
}
