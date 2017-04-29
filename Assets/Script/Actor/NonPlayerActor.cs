using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class NonPlayerActor : Actor
{
    private AudioSource _audioSource = null;
    private Animator animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private Animator _deathAnimator = null;

    private PlayerActor _enemyTarget = null;

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

        InitSelectResource( UnityEngine.Random.Range( 0, 6 ) );
        UIGameScene.Instance.CreateHPBar( this );
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

    private void InvokeHit( PlayerActor target )
    {
        Debug.Log( "InvokeHit" );
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

            StartCoroutine( Helper.Wait( _deathAnimator.GetClip().length, () =>
            {
                Destroy( gameObject );
            } ) );
        }
    }

    virtual protected void CallbackHit( PlayerActor playerActor ) { }

    virtual protected void CallbackDestroy( bool bSelf ) { }
}
