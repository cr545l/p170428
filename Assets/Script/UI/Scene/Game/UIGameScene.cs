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
    private Canvas _canvas = null;
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _timeText = null;
    [SerializeField]
    private Text _healthPointText = null;
    [SerializeField]
    private Text _damageText = null;
    [SerializeField]
    private UINagativeCover _uiNagativeCover = null;
    [SerializeField]
    private UIHPBar _uiHpBar = null;

    [SerializeField]
    private UIPopupPause _uiPopupPause = null;
    [SerializeField]
    private UIPopupResult _uiPopupResult = null;

    private void Start ()
    {
        if( Helper.isNull( _scoreText, _timeText, _uiHpBar, _uiNagativeCover, _uiPopupPause, _uiPopupResult ) ) return;

        _uiPopupPause.gameObject.SetActive( false );
        _uiPopupResult.gameObject.SetActive( false );
    }
    
    private void Update ()
    {
        _timeText.text = GameManager.Instance.Timer.CurrentTime.ToString();
        _healthPointText.text = "체력 : " + GameManager.Instance.PlayerActor.CurrentHealthPoint + "/" + GameManager.Instance.PlayerActor.MaxHealthPoint;
        _damageText.text = "공격력 : " + GameManager.Instance.PlayerActor.Damage;
        _scoreText.text = "점수 " + GameManager.Instance.GetScore();
    }

    public void CreateHPBar( NonPlayerActor target )
    {
        //UIHPBar instance = Instantiate( _uiHpBar );
        //instance.transform.SetParent( _canvas.transform );
        //instance.transform.SetSiblingIndex( 0 );
        //
        //instance.Init( target, _canvas );
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

    public void SetScoreText(int score)
    {
        _scoreText.text = "점수 " + score;
    }
}
