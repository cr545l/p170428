using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScene : MonoBehaviour
{    
	private void Start ()
    {
		
	}

    private void Update ()
    {
		
	}

    public void StartButtonClick()
    {
        SceneManager.LoadScene( GameConst._GAME_SCENE, LoadSceneMode.Single );
    }
}
