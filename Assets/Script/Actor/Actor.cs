using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMessageType
{
    None,
    Attack_FromNonPlayer,
    NonPlayerAttack_FromUser,
}

public struct GameActorMessage
{
    public eMessageType _targetType;
    public Actor _targetActor;
}

public class Actor : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D _collider = null;

    public event Action<Actor> _eventDeath = null;
    private float _currentHealthPoint = 1.0f;
    private float _maxHealthPoint = 1.0f;

    private int _shieldCount = 0;
    private bool _bAlive = false;

    private float _powerModeTime = 0.0f;

    private float _damage = 0.0f;

    public float Damage { get { return _damage; } protected set { _damage = value; } }

    public float CurrentHealthPoint
    {
        get { return _currentHealthPoint; }

        private set
        {
            if( 0 < _shieldCount && value < _currentHealthPoint )
            {
                _shieldCount--;
                return;
            }

            _currentHealthPoint = Mathf.Clamp( value, 0, _maxHealthPoint );
            
            if( _currentHealthPoint <= 0.0f )
            {
                Debug.LogFormat( "GameActor Death" );
                if( null != _eventDeath )
                {
                    _eventDeath( this );
                }
                _bAlive = false;
            }
        }
    }

    public float MaxHealthPoint
    {
        get { return _maxHealthPoint; }

        set
        {
            _maxHealthPoint = value;
            _currentHealthPoint = _maxHealthPoint;

            Debug.LogFormat( "GameActor Max Health Point {0}", _maxHealthPoint );
        }
    }

    public CircleCollider2D Collider { get { return _collider; } }

    public bool isAlive
    {
        get { return _bAlive; }
        protected set { _bAlive = value; }
    }

    public int ShieldCount { get { return _shieldCount; } set { _shieldCount = value; } }

    virtual protected void InitActor() { }
    virtual protected void UpdateActor() { }

    private void Start()
    {
        GameManager.Instance._eventGameActor += CallbackMessage;
        _eventDeath += CallbackDeath;

        if( null == _collider )
        {
            _collider = gameObject.AddComponent<CircleCollider2D>();
        }
        Init();
    }

    public void Init()
    {
        _bAlive = true;
        InitActor();
    }

    private void Update()
    {
        if( _bAlive )
        {
            if( 0.0f < _powerModeTime )
            {
                _powerModeTime -= Time.deltaTime;
            }

            UpdateActor();
        }
    }

    virtual protected void CallbackMessage( GameMessage message ) { }
    virtual protected void CallbackDeath( Actor target ) { }
    virtual protected void CallbackDamage() { }

    public void InvokePowerMode()
    {
        _powerModeTime = GameConst._POWER_MODE_TIME;
    }

    public void InvokeShield()
    {
        _shieldCount++;
    }

    public void InvokeHealthRecovery()
    {
        CurrentHealthPoint++;
    }

    public void InvokeDamage( float damage )
    {
        if( _bAlive && _powerModeTime <= 0.0f )
        {
            //Debug.LogFormat( "InvokeDamage {0}", damage );
            CurrentHealthPoint -= damage;
            CallbackDamage();
            Debug.LogFormat( "InvokeDamage {0}, CurrentHealthPoint {1}", damage, _currentHealthPoint );
        }
    }

    protected void InvokeMessage( GameActorMessage message )
    {
        GameManager.Instance.InvokeMessage( new GameMessage()
        {
            _invokeActor = this,
            _gameActorMessage = message,
        } );
    }

    public bool isHit( CircleCollider2D target )
    {
        if( null != target )
        {
            float distance = Collider.radius + target.radius;
            if( Vector3.Distance( transform.position, target.transform.position ) <= distance )
            {
                return true;
            }
        }

        return false;
    }
}
