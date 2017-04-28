using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    public event Action<GameActor> _eventDeath = null;
    private float _healthPoint = 0.0f;

    virtual protected void InitActor() { }
    virtual protected void UpdateActor() { }

	private void Start ()
    {
        InitActor();
	}
	
	private void Update ()
    {
        UpdateActor();	
	}

    protected void SetListerner( eMessageType type )
    {
        switch(type)
        {
            case eMessageType.Player:
                GameManager.Instance._eventPlayer += CallbackMessage;
                break;

            case eMessageType.NonPlayer:
                GameManager.Instance._eventNonPlayer += CallbackMessage;
                break;
        }
    }

    virtual protected void CallbackMessage( GameMessage message ) { }

    protected void InvokeAttack( GameActor target, float damage )
    {
        target._healthPoint -= damage;

        if( null != _eventDeath )
        {
            _eventDeath( this );
        }
    }

    protected void InvokeMessage( GameMessage message )
    {
        GameManager.Instance.InvokeMessage( message );
    }
}
