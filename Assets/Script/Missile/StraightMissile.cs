using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMissile : Missile
{
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _moveVector = Vector3.zero;
    protected Action<NonPlayerActor> _finished = null;

    public void Launch( Vector3 worldPosition, Action<NonPlayerActor> finished )
    {
        _bLaunch = true;
        _finished = finished;
        DestroyAnimation.gameObject.SetActive( false );

        _startPosition = transform.position;
        _moveVector = Vector3.Normalize( worldPosition - _startPosition );
        _moveVector.z = 0.0f;

        transform.LookAt2D( worldPosition );
    }

    private void Update ()
    {
        if( _bLaunch )
        {
            transform.position += _moveVector;
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        NonPlayerActor actor = collision.GetComponent<NonPlayerActor>();

        if( null != actor )
        {
            InvokeFinished(actor);
        }
    }

    protected void InvokeFinished(NonPlayerActor actor)
    {
        if( null != _finished )
        {
            _finished(actor);
        }
        InvokeDestroy();
    }
}
