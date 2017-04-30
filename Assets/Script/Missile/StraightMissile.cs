using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMissile : Missile
{
    private const int _OUT_SIZE = 20;

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

        _moveVector *= GameConst._DEFAULT_MISSILE_SPEED;

        transform.LookAt2D( worldPosition );
    }

    private void Update()
    {
        if( _bLaunch )
        {
            transform.position += _moveVector * Time.deltaTime;

            if( isOutDisplay() )
            {
                InvokeDestroy();
            }
        }
    }

    private bool isOutDisplay()
    {
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint( transform.position );

        return Screen.width + _OUT_SIZE < targetScreenPos.x
                || targetScreenPos.x < -_OUT_SIZE
                || Screen.height + _OUT_SIZE < targetScreenPos.y
                || targetScreenPos.y < -_OUT_SIZE;
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
