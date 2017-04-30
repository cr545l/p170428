using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public event Action<Missile> _eventDestroy = null;

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;
    [SerializeField]
    private Animator _destroyAnimation = null;
    [SerializeField]
    private ParticleSystem _particleSystem = null;

    public Animator DestroyAnimation { get { return _destroyAnimation; } }

    protected bool _bLaunch = false;

    private void Start()
    {
        if( Helper.isNull( _spriteRenderer, _destroyAnimation, _particleSystem ) ) return;
    }

    public void InvokeDestroy()
    {
        _bLaunch = false;

        _spriteRenderer.enabled = false;
        _particleSystem.gameObject.SetActive( false );

        DestroyAnimation.gameObject.SetActive( true );
        DestroyAnimation.Play( 0 );

        StartCoroutine( Helper.Wait( DestroyAnimation.GetAnimatorLength(), () =>
        {
            if( null != _eventDestroy )
            {
                _eventDestroy(this);
            }

            Destroy( gameObject );
        } ) );
    }
}
