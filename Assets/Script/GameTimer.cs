using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    public event Action _eventTimeOver = null;
    private float[] _times = GameConst._DEFAULT_TIMES;

    private float _currentTime = 0;

    public void RandomSelect()
    {
        _currentTime = _times[ UnityEngine.Random.Range( 0, _times.Length )];

        Debug.LogFormat( "Random Selected time : {0}", _currentTime );
    }

    public void CheckTime( float deltaTime )
    {
        _currentTime -= deltaTime;
        if( _currentTime <= 0.0f )
        {
            RandomSelect();

            if( null != _eventTimeOver )
            {
                _eventTimeOver();
            }
        }
    }
}
