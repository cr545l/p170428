using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private Animator _destroyAnimation = null;

    private Actor _targetActor = null;
    private Vector3 _startPosition = Vector3.zero;
    private float _targetTime = GameConst._DEFAULT_MISSILE_TIME;
    private float _currentTime = 0.0f;

    private bool _bLaunch = false;
    private Action _finished = null;

    private void Start()
    {
        if( Helper.isNull( _destroyAnimation ) ) return;
    }

    public void Init( Actor target, Action finished )
    {
        _finished = finished;
        _destroyAnimation.gameObject.SetActive( false );

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
                if( null != _finished )
                {
                    _finished();
                }
                InvokeDestroy();
                return;
            }

            transform.position = Vector3.LerpUnclamped( _startPosition, _targetActor.transform.position, percent );
        }
    }

    private void InvokeDestroy()
    {
        _bLaunch = false;

        _destroyAnimation.gameObject.SetActive( true );
        _destroyAnimation.Play( 0 );

        StartCoroutine( Helper.Wait( _destroyAnimation.GetClip().length, () =>
        {
            Destroy( gameObject );
        } ) );
    }
}
