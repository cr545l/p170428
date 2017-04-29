using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ResultData
{
    public int _score;
}

public class UIGameScene : SingletonAwake<UIGameScene>
{
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _timeText = null;
    [SerializeField]
    private UINagativeCover _uiNagativeCover = null;

    [SerializeField]
    private UIPopupPause _uiPopupPause = null;
    [SerializeField]
    private UIPopupResult _uiPopupResult = null;

    private void Start ()
    {
        if( Helper.isNull( _scoreText, _timeText, _uiNagativeCover, _uiPopupPause, _uiPopupResult ) ) return;

        _uiPopupPause.gameObject.SetActive( false );
        _uiPopupResult.gameObject.SetActive( false );
    }
    
    private void Update ()
    {
        _timeText.text = GameManager.Instance.Timer.CurrentTime.ToString();
	}

    public void InvokeCover( float time )
    {
        _uiNagativeCover.InvokeAlpha( time );
    }

    public void PauseButtonClick()
    {
        Debug.Log( "PauseButtonClick" );
        GameManager.Instance.InvokePause( true );
        _uiPopupPause.gameObject.SetActive( true );
    }

    public void ShowResult( ResultData resultData )
    {
        _uiPopupResult.Show( resultData );
    }
}
