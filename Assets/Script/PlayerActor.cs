﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerActor : GameActor
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private Animator _deathAnimator = null;

    [SerializeField]
    private Sprite[] _sprites = new Sprite[] { };

    private float _damage = GameConst._DEFAULT_DAMAGE;

    public float Damage
    {
        get { return _damage; }
        //set { _damage = value; }
    }

    override protected void InitActor()
    {
        if( Helper.isNull( _spriteRenderer, _deathAnimator ) ) return;

        Debug.Log(_deathAnimator.GetClip().length);
        _deathAnimator.gameObject.SetActive( false );
        _spriteRenderer.enabled = true;
    }

    override protected void UpdateActor()
    {

    }

    protected override void CallbackDamage()
    {
        float percent = CurrentHealthPoint / MaxHealthPoint;

        if( percent < 0.1f )
        {
            _spriteRenderer.sprite = _sprites[4];
        }
        else if( percent < 0.3f )
        {
            _spriteRenderer.sprite = _sprites[3];
        }
        else if( percent < 0.5f )
        {
            _spriteRenderer.sprite = _sprites[2];
        }
        else if( percent < 0.7f )
        {
            _spriteRenderer.sprite = _sprites[1];
        }
        else if( percent < 0.9f )
        {
            _spriteRenderer.sprite = _sprites[0];
        }
    }

    protected override void CallbackDeath( GameActor target )
    {
        _deathAnimator.gameObject.SetActive( true );
        _spriteRenderer.enabled = false;
    }    protected override void CallbackMessage( GameMessage message )
    {
        message._invokeActor.InvokeDamage(_damage);
    }
}
