using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : SingletonAwake<GameScene>
{    
	private void Start ()
    {
        SceneManager.LoadScene( GameConst._UI_GAME_SCENE, LoadSceneMode.Additive );
	}

    private void Update ()
    {
		
	}
}
