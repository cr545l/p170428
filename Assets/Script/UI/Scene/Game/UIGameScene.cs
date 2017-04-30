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
    private Text _besetScoreText = null;
    [SerializeField]
    private Text _timeText = null;
    [SerializeField]
    private Text _healthPointText = null;
    [SerializeField]
    private Text _damageText = null;
    [SerializeField]
    private Text _playingText = null;
    [SerializeField]
    private UINagativeCover _uiNagativeCover = null;
    [SerializeField]
    private UIHPBar _uiHpBar = null;
    [SerializeField]
    private Image _image = null;

    [SerializeField]
    private UIPopupPause _uiPopupPause = null;
    [SerializeField]
    private UIPopupResult _uiPopupResult = null;
    [SerializeField]
    private UIPopupUpgrade _uiPopupUpgrade = null;

    private void Start ()
    {
        if( Helper.isNull( _scoreText, _besetScoreText, _timeText, _playingText,
            _uiHpBar, _uiNagativeCover, 
            _uiPopupPause, _uiPopupResult, _image ) ) return;

        _uiPopupPause.gameObject.SetActive( false );
        _uiPopupResult.gameObject.SetActive( false );
        _uiPopupUpgrade.gameObject.SetActive( false );
    }
    
    private void Update ()
    {
        _timeText.text = GameManager.Instance.Timer.CurrentTime.ToString("N2");

        _healthPointText.text = GameManager.Instance.PlayerActor.CurrentHealthPoint.ToString();
        _damageText.text = "POWER " + GameManager.Instance.PlayerActor.Damage;
        _scoreText.text = "SCORE " + GameManager.Instance.GetScore();
        _besetScoreText.text = "BEST " + GameAnalyst.Instance.BestScore;
        _playingText.text = "PLAYING " + GameManager.Instance.CurrentPatton;
        _image.fillAmount = GameManager.Instance.PlayerActor.CurrentHealthPoint / GameManager.Instance.PlayerActor.MaxHealthPoint;
    }

    public UIHPBar CreateHPBar( NonPlayerActor target )
    {
        UIHPBar instance = Instantiate( _uiHpBar );
        instance.transform.SetParent( _canvas.transform );
        instance.transform.SetSiblingIndex( 0 );        
        instance.Init( target, _canvas );

        return instance;
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

    public void PopupUpdate()
    {
        GameManager.Instance.InvokePause(true);
        _uiPopupUpgrade.gameObject.SetActive(true);
    }
}
