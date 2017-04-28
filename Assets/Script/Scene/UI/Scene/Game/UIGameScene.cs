using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScene : SingletonAwake<UIGameScene>
{
    [SerializeField]
    private Text _scoreText = null;

    [SerializeField]
    private UIPopupPause _uiPopupPause = null;
    [SerializeField]
    private UIPopupResult _uiPopupResult = null;
    
    private void Start ()
    {
        if( Helper.isNull( _scoreText, _uiPopupPause, _uiPopupResult ) ) return;

        _uiPopupPause.gameObject.SetActive( false );
        _uiPopupResult.gameObject.SetActive( false );
    }
    
    private void Update ()
    {
		
	}

    public void PauseButtonClick()
    {
        GameManager.Instance.InvokePause( true );
        _uiPopupPause.gameObject.SetActive( true );
    }
}
