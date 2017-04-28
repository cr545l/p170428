using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene( GameConst._UI_GAME_SCENE, LoadSceneMode.Additive );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
