using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InduceMissile : Missile
{
    private Actor _targetActor = null;
    private Vector3 _startPosition = Vector3.zero;
    private float _targetTime = GameConst._DEFAULT_MISSILE_TIME;
    private float _currentTime = 0.0f;
    protected Action _finished = null;

    public void Launch( Actor target, Action finished )
    {
        _bLaunch = true;
        _finished = finished;
        DestroyAnimation.gameObject.SetActive( false );

        _targetActor = target;
        _startPosition = transform.position;

        _bLaunch = true;
    }

    private void Update()
    {
        if( _bLaunch )
        {
            _currentTime += Time.deltaTime;

            float percent = _currentTime / _targetTime;

            if( 1.0f <= percent || null == _targetActor )
            {
                InvokeFinished();
                return;
            }

            transform.position = Vector2.LerpUnclamped( _startPosition, _targetActor.transform.position, percent );
            transform.LookAt2D( _targetActor.transform.position );
        }
    }

    protected void InvokeFinished()
    {
        if( null != _finished )
        {
            _finished();
        }
        InvokeDestroy();
    }
}
