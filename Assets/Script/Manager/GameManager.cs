using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonAwake<GameManager>
{
    public event Action<GameMessage> _eventPlayer = null;
    public event Action<GameMessage> _eventNonPlayer = null;

    private GameTimer _timer = new GameTimer();

    private void Start ()
    {
        _timer._eventTimeOver += CreateNonPlayerActor;
        _timer.RandomSelect();
    }

	private void Update ()
    {
        _timer.CheckTime( Time.deltaTime );
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

    }

    public void InvokeTitle()
    {
        SceneManager.LoadScene( GameConst._TITLE_SCENE, LoadSceneMode.Single );
    }

    public void InvokePause( bool enable = true )
    {
        Time.timeScale = enable ? 0.0f : 1.0f;
    }

    private void CreateNonPlayerActor()
    {

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
