﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public abstract class NonPlayerActor : Actor
{
    private int _nonPlayerActorType = 0;

    private AudioSource _audioSource = null;
    private Animator animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private Animator _deathAnimator = null;

    private PlayerActor _enemyTarget = null;
    private UIHPBar _uiHpBar = null;

    public int NonPlayerActorType { get { return _nonPlayerActorType; } }

    public void InitNonPlayer( float speed, PlayerActor enemyTarget )
    {
        Damage = GameConst._DEFAULT_NONPLAYER_DAMAGE;

        _enemyTarget = enemyTarget;

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = GameManager.Instance.NonPlayerDeathAudioClip;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if( null != animator )
        {
            animator.speed = speed;
        }

        _nonPlayerActorType = RandomSelect();
        InitSelectResource( _nonPlayerActorType );

        MaxHealthPoint = GameConst._NONPLAYER_HEALTH_POINT[_nonPlayerActorType];
        switch (_nonPlayerActorType)
        {
            case GameConst._NONPLAYER_TYPE_METEORITE1:
            case GameConst._NONPLAYER_TYPE_METEORITE2:
            case GameConst._NONPLAYER_TYPE_METEORITE3:
            case GameConst._NONPLAYER_TYPE_UFO1:
            case GameConst._NONPLAYER_TYPE_UFO2:
            case GameConst._NONPLAYER_TYPE_UFO3:
                MaxHealthPoint += GameManager.Instance.NpNormalUpgradeHp;
                break;
            case GameConst._NONPLAYER_TYPE_METEORITE5:
            case GameConst._NONPLAYER_TYPE_METEORITE6:
                MaxHealthPoint *= GameManager.Instance.NpPositiveUpgradeHp;
                break;

        }
        CurrentHealthPoint = MaxHealthPoint;
        Damage = GameConst._NONPLAYER_DAMAGE[_nonPlayerActorType];
        switch(_nonPlayerActorType)
        {
            case GameConst._NONPLAYER_TYPE_METEORITE1:
            case GameConst._NONPLAYER_TYPE_METEORITE2:
            case GameConst._NONPLAYER_TYPE_METEORITE3:
            case GameConst._NONPLAYER_TYPE_UFO1:
            case GameConst._NONPLAYER_TYPE_UFO2:
            case GameConst._NONPLAYER_TYPE_UFO3:
                Damage += GameManager.Instance.NpNormalUpgradeAtk;
                break;
        }

        _uiHpBar = UIGameScene.Instance.CreateHPBar( this );
        _uiHpBar.gameObject.SetActive( false );
    }
    public bool isNagative { get { return _nonPlayerActorType == 3; } }

    private float GetNextHealPoint(float value)
    {
        float result = value;

        return result;
    }

    private int RandomSelect()
    {
        int select = 0;
        int i = UnityEngine.Random.Range( 0, 100 );
        
        if(i < 30)
        {
            int j = UnityEngine.Random.Range( 0, 30 );
            if( j < 10 )
            {
                // 네거티브
                select = 3;
            }
            else
            {
                if( 0 == UnityEngine.Random.Range( 0, 2 ) )
                {
                    // 강화
                    select = 4;
                }
                else
                {
                    // 아이템
                    select = 5;
                }
            }
        }
        else
        {
            int[] selects = { 0, 1, 2, 6, 7, 8 };
            int j = UnityEngine.Random.Range( 0, selects.Length );
            select = selects[j];
        }

        return select;
    }

    private void InitSelectResource( int index )
    {
        _spriteRenderer.sprite = GameManager.Instance.NonPlayerSprites[index];
        _spriteRenderer.enabled = true;

        _deathAnimator = Instantiate( GameManager.Instance.NonPlayerDeathAnimators[index] );
        _deathAnimator.transform.parent = transform;
        _deathAnimator.transform.localPosition = Vector3.zero;
        _deathAnimator.gameObject.SetActive( false );
    }

    protected override void UpdateActor()
    {
        if( isHit( _enemyTarget.Collider ) )
        {
            InvokeHit( _enemyTarget );
        }

        if( animator.GetCurrentAnimatorStateInfo( 0 ).normalizedTime >= 1 )
        {
            Destroy( gameObject );
        }
    }

    private void OnMouseDown()
    {
        InvokeMessage( new GameActorMessage {
            _targetType = eMessageType.NonPlayerAttack_FromUser,
            _targetActor = null } );
    }

    public void SetPlaySpeed( float speed )
    {
        animator.speed = speed;
    }

    public void SetAnimatorController( RuntimeAnimatorController controller )
    {
        animator.runtimeAnimatorController = controller;
    }

    protected override void CallbackMessage( GameMessage message )
    {
    }

    protected override void CallbackDeath( Actor target )
    {
        InvokeDestroy();
        CallbackDestroy( false );
    }

    protected override void CallbackDamage()
    {
        _uiHpBar.gameObject.SetActive( true );
    }

    private void InvokeHit( PlayerActor target )
    {
        //Debug.Log( "InvokeHit" );
        target.InvokeDamage( Damage );
        InvokeDestroy();
        CallbackDestroy( true );

        CallbackHit( target );
    }

    public void InvokeDestroy()
    {
        if( isAlive )
        {
            isAlive = false;

            GameManager.Instance.InvokeAttackShakeCamera();

            animator.speed = 0.0f;

            _spriteRenderer.enabled = false;

            _deathAnimator.gameObject.SetActive( true );
            _deathAnimator.Play( 0 );

            _audioSource.Play();

            StartCoroutine( Helper.Wait( _deathAnimator.GetAnimatorLength(), () =>
            {
                Destroy( gameObject );
            } ) );
        }
    }

    virtual protected void CallbackHit( PlayerActor playerActor ) { }

    virtual protected void CallbackDestroy( bool bSelf ) { }
}
