using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    public event Action<GameMessage> _eventPlayer = null;
    public event Action<GameMessage> _eventNonPlayer = null;

    private GameTimer _timer = new GameTimer();

    private void Awake ()
    {
        _instance = this;
        _timer._eventTimeOver += CreateNonPlayerActor;
        _timer.RandomSelect();
    }

	private void Update ()
    {
        _timer.CheckTime( Time.deltaTime );
    }
    
    private void CreateNonPlayerActor()
    {

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
