﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMessageType
{
    None,
    Attack_FromNonPlayer,
    NonPlayerAttack_FromUser,
}

public struct GameActorMessage
{
    public eMessageType _targetType;
    public GameActor _targetActor;
}

public class GameActor : MonoBehaviour
{
    public event Action<GameActor> _eventDeath = null;
    private float _currentHealthPoint = 1.0f;
    private float _maxHealthPoint = 1.0f;

    private bool _bAlive = false;

    public float CurrentHealthPoint
    {
        get { return _currentHealthPoint; }

        private set
        {
            float temp = _currentHealthPoint;
            _currentHealthPoint = value;
            
            if( _currentHealthPoint <= 0.0f )
            {
                _bAlive = false;
                Debug.LogFormat( "GameActor Death" );
                if( null != _eventDeath )
                {
                    _eventDeath( this );
                }
            }
        }
    }

    public float MaxHealthPoint
    {
        get { return _maxHealthPoint; }

        set
        {
            _maxHealthPoint = value;
            _currentHealthPoint = _maxHealthPoint;

            Debug.LogFormat( "GameActor Max Health Point {0}", _maxHealthPoint );
        }
    }

    virtual protected void InitActor() { }
    virtual protected void UpdateActor() { }

	private void Start ()
    {
        InitActor();

        _bAlive = true;
        GameManager.Instance._eventGameActor += CallbackMessage;
        _eventDeath += CallbackDeath;
    }

    private void Update()
    {
        UpdateActor();	
	}

    virtual protected void CallbackMessage( GameMessage message ) { }
    virtual protected void CallbackDeath( GameActor target ) { }
    virtual protected void CallbackDamage() { }

    public void InvokeDamage( float damage )
    {
        if( _bAlive )
        {
            Debug.LogFormat( "InvokeDamage {0}", damage );
            CurrentHealthPoint -= damage;
            CallbackDamage();
            Debug.LogFormat( "InvokeDamage {0}, CurrentHealthPoint {1}", damage, _currentHealthPoint );
        }
    }

    protected void InvokeMessage( GameActorMessage message )
    {
        GameManager.Instance.InvokeMessage( new GameMessage()
        {
            _invokeActor = this,
            _gameActorMessage = message,
        } );
    }
}
