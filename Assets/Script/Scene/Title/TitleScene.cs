using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {
    
	private void Start ()
    {
        Application.targetFrameRate = 60;

        SceneManager.LoadScene( GameConst._UI_TITLE_SCENE, LoadSceneMode.Additive );
	}

    private void Update ()
    {
		
	}
}
