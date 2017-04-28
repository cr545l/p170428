using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eGameState
{
    None,
    Playing,
    Ended,
}

public class GameManager : SingletonAwake<GameManager>
{
    public event Action<GameMessage> _eventPlayer = null;
    public event Action<GameMessage> _eventNonPlayer = null;

    [SerializeField]
    private PlayerActor _playerActor = null;

    private eGameState _gameState = eGameState.None;
    private GameTimer _timer = new GameTimer();
    private int _currentScore = 0;

    public PlayerActor PlayerActor { get { return _playerActor; } }
    public GameTimer Timer { get { return _timer; } }

    private void Start()
    {
        if( Helper.isNull( _playerActor ) ) return;

        _timer._eventTimeOver += CreateNonPlayerActor;
        _playerActor._eventDeath += CallbackPlayerDeath;

        InvokeStart();
    }

    private void Update()
    {
        if( eGameState.Playing == _gameState )
        {
            _timer.CheckTime( Time.deltaTime );
        }
    }

    public void InvokeMessage( GameMessage message )
    {
        switch( message._targetType )
        {
            case eMessageType.All:
                InvokePlayerEvent( message );
                InvokeNonPlayerEvent( message );
                break;

            case eMessageType.Player:
                InvokePlayerEvent( message );
                break;

            case eMessageType.NonPlayer:
                InvokeNonPlayerEvent( message );
                break;
        }
    }

    public void InvokeStart()
    {
        InvokePause( false );
        _gameState = eGameState.Playing;

        _playerActor.MaxHealthPoint = GameConst._DEFAULT_MAX_HEALTH_POINT;

        _currentScore = 0;
        _timer.Init();
    }

    public void InvokeTitle()
    {
        InvokePause( false );
        SceneManager.LoadScene( GameConst._TITLE_SCENE, LoadSceneMode.Single );
    }

    public void InvokePause( bool enable = true )
    {
        Time.timeScale = enable ? 0.0f : 1.0f;
    }

    private void CreateNonPlayerActor()
    {

    }

    private void CallbackPlayerDeath( GameActor target )
    {
        _gameState = eGameState.Ended;
        UIGameScene.Instance.ShowResult( new ResultData()
        {
            _score = _currentScore,
        } );
    }

    private void InvokePlayerEvent( GameMessage message )
    {
        if( null != _eventPlayer )
        {
            _eventPlayer( message );
        }
    }

    private void InvokeNonPlayerEvent( GameMessage message )
    {
        if( null != _eventNonPlayer )
        {
            _eventNonPlayer( message );
        }
    }
}
