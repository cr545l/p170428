using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerActor : Actor
{
    [SerializeField]
    private StraightMissile _missile = null;

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private Animator _deathAnimator = null;

    [SerializeField]
    private Sprite[] _sprites = new Sprite[] { };

    override protected void InitActor()
    {
        if( Helper.isNull( _missile, _spriteRenderer, _deathAnimator ) ) return;

        InitPlayer();
    }

    public void InitPlayer()
    {
        Damage = GameConst._DEFAULT_PLAYER_DAMAGE;

        _deathAnimator.gameObject.SetActive( false );
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = _sprites[0];

        MaxHealthPoint = GameConst._DEFAULT_PLAYER_MAX_HEALTH_POINT;
        CurrentHealthPoint = MaxHealthPoint;
    }

    override protected void UpdateActor()
    {
        if( Input.GetMouseButtonDown( 0 ) )
        {
            var v3 = Camera.main.ScreenPointToRay( Input.mousePosition ).GetPoint( 0 );
            Debug.Log( v3 );
            var missile = Instantiate( _missile, transform, false );
            missile.Launch( v3, ( actor ) => { actor.InvokeDamage( Damage ); } );

        }
    }

    protected override void CallbackDamage()
    {
        float percent = CurrentHealthPoint / MaxHealthPoint;

        if( percent < 0.1f )
        {
            _spriteRenderer.sprite = _sprites[5];
        }
        else if( percent < 0.3f )
        {
            _spriteRenderer.sprite = _sprites[4];
        }
        else if( percent < 0.5f )
        {
            _spriteRenderer.sprite = _sprites[3];
        }
        else if( percent < 0.7f )
        {
            _spriteRenderer.sprite = _sprites[2];
        }
        else if( percent < 0.9f )
        {
            _spriteRenderer.sprite = _sprites[1];
        }
        else
        {
            _spriteRenderer.sprite = _sprites[0];
        }
    }

    protected override void CallbackDeath( Actor target )
    {
        _deathAnimator.gameObject.SetActive( true );
        _deathAnimator.Play( 0 );
        _spriteRenderer.enabled = false;
    }

    protected override void CallbackMessage( GameMessage message )
    {
        switch( message._gameActorMessage._targetType )
        {
            case eMessageType.NonPlayerAttack_FromUser:
                {
                    //var missile = Instantiate( _missile, transform, false );
                    //missile.Launch( message._invokeActor.transform.position, ()=> { message._invokeActor.InvokeDamage( Damage ); } );
                }
                break;
        }
    }
}
