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

    private bool _bAlive = false;

    private float _damage = GameConst._DEFAULT_DAMAGE;

    public float Damage { get { return _damage; } }

    public float CurrentHealthPoint
    {
        get { return _currentHealthPoint; }

        private set
        {
            float temp = _currentHealthPoint;
            _currentHealthPoint = value;
            
            if( _currentHealthPoint <= 0.0f )
            {
                _bAlive = false;
                Debug.LogFormat( "GameActor Death" );
                if( null != _eventDeath )
                {
                    _eventDeath( this );
                }
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

        protected set
        {
            _bAlive = value;
        }
    }

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
            UpdateActor();
        }
    }

    virtual protected void CallbackMessage( GameMessage message ) { }
    virtual protected void CallbackDeath( Actor target ) { }
    virtual protected void CallbackDamage() { }

    public void InvokeDamage( float damage )
    {
        if( _bAlive )
        {
            Debug.LogFormat( "InvokeDamage {0}", damage );
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
