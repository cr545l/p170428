using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScene : MonoBehaviour
{
    [SerializeField]
    private GameObject _button = null;

    private void Start()
    {
        StartCoroutine( Helper.Wait( 1.2f, () =>
          {
              iTween.ScaleTo( _button, new Vector3( 1, 1, 1 ), 0.4f );
          }
        ) );
    }

    private void Update()
    {

    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene( GameConst._GAME_SCENE, LoadSceneMode.Single );
    }
}