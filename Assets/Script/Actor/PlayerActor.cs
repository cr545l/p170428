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

    [SerializeField]
    private AudioSource _gameOverAudioSource = null;

    private List<Missile> _currentMissileList = new List<Missile>();
    private float _maximumLaunchMissile = GameConst._DEFAULT_MAXIMUM_LAUNCH_MISSILE;

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
        if( Input.GetMouseButtonDown( 0 ) && isPossibleLaunch() )
        {
            Vector3 worldPosition = Camera.main.ScreenPointToRay( Input.mousePosition ).GetPoint( 0 );
            var missile = Instantiate( _missile, transform, false );
            _currentMissileList.Add( missile );
            missile.Launch( worldPosition, ( actor ) => { actor.InvokeDamage( Damage ); } );
            missile._eventDestroy += ( destroyed ) => { _currentMissileList.Remove( destroyed ); };
        }
    }

    private bool isPossibleLaunch()
    {
        return _currentMissileList.Count < _maximumLaunchMissile;
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
    private void MissileDestroy()
    {
        _currentMissileList.ForEach( x => x.InvokeDestroy() );
    }

    protected override void CallbackDeath( Actor target )
    {
        _gameOverAudioSource.Play();
        MissileDestroy();

        iTween.MoveTo( GameManager.Instance.Camera.gameObject, 
                        GameConst._GAME_OVER_CAMERA_POSITION,
                        GameConst._GAME_OVER_CAMERA_MOVE_TIME );

        StartCoroutine( Helper.Wait( GameConst._GAME_OVER_CAMERA_MOVE_TIME - GameConst._GAME_OVER_RESULT_POPUP, ()=>
        {
            _deathAnimator.gameObject.SetActive( true );
            _deathAnimator.Play( 0 );
            _spriteRenderer.enabled = false;
        } ) );
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
