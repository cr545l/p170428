using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    public event Action _eventTimeOver = null;
    private float[] _times = GameConst._DEFAULT_RANDOM_TIMES;

    private float _currentTime = 0;
    private float _targetTime = GameConst._DEFAULT_TIME;

    public float CurrentTime { get { return _currentTime; } }
    public float TargetTime { get { return _targetTime; } }

    public void InitRandom()
    {
        _currentTime = _targetTime;
        _targetTime = _times[UnityEngine.Random.Range( 0, _times.Length )];
        Debug.LogFormat( "InitRandom {0}", _targetTime );
    }

    public void Init( float targetTime = 0.0f )
    {
        _targetTime = targetTime;
        _currentTime = _targetTime;
        Debug.LogFormat( "Init {0}", _targetTime );
    }

    public void CheckTime( float deltaTime )
    {
        _currentTime -= deltaTime;
        if( _currentTime <= 0.0f )
        {
            Debug.LogFormat( "CheckTime Over {0}", _targetTime );
            
            if( null != _eventTimeOver )
            {
                _eventTimeOver();
            }
        }
    }
}
