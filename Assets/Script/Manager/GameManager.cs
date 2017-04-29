﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eGameState
{
    None,
    Playing,
    Ended,
}

public class GameManager : SingletonAwake<GameManager>
{
    public event Action<GameMessage> _eventGameActor = null;

    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private PlayerActor _playerActor = null;

    [SerializeField]
    private Sprite[] _nonPlayerSprites = null;

    [SerializeField]
    private Animator[] _nonPlayerDeathAnimators = null;

    [SerializeField]
    private AudioClip _nonPlayerDeathAudioClip = null;

    private eGameState _gameState = eGameState.None;
    private GameTimer _defaultTimer = new GameTimer();
    private GameTimer _randomTimer = new GameTimer();

    private List<NonPlayerGroup> _nonPlayerGroupList = new List<NonPlayerGroup>();

    private int _currentScore = 0;

    public PlayerActor PlayerActor { get { return _playerActor; } }
    public GameTimer Timer { get { return _randomTimer; } }

    public Sprite[] NonPlayerSprites { get { return _nonPlayerSprites; } }
    public Animator[] NonPlayerDeathAnimators { get { return _nonPlayerDeathAnimators; } }
    public AudioClip NonPlayerDeathAudioClip { get { return _nonPlayerDeathAudioClip; } }

    private void Start()
    {
        if( Helper.isNull( _camera, _playerActor ) ) return;

        _defaultTimer._eventTimeOver += CallbackDefaultTimeOver;
        _randomTimer._eventTimeOver += CallbackRandomTimeOver;

        _playerActor._eventDeath += CallbackPlayerDeath;

        InvokeStart();
    }

    private void Update()
    {
        if( eGameState.Playing == _gameState )
        {
            _defaultTimer.CheckTime( Time.deltaTime );
            _randomTimer.CheckTime( Time.deltaTime );
        }
    }

    public void InvokeShakeCamera()
    {
        iTween.ShakePosition( _camera.gameObject, new Vector3( 0.3f, 0.3f, 0.3f), 0.1f );
    }

    public void InvokeMessage( GameMessage message )
    {
        if( null != _eventGameActor )
        {
            _eventGameActor( message );
        }
    }

    public void InvokeStart()
    {
        InvokePause( false );
        _gameState = eGameState.Playing;

        _playerActor.MaxHealthPoint = GameConst._DEFAULT_MAX_HEALTH_POINT;

        _currentScore = 0;

        _playerActor.Init();
        _defaultTimer.Init( GameConst._DEFAULT_TIME );
        _randomTimer.InitRandom();
        ClearNonPlayerGroup();
    }

    public void InvokeTitle()
    {
        InvokePause( false );
        SceneManager.LoadScene( GameConst._TITLE_SCENE, LoadSceneMode.Single );
    }

    public void InvokePause( bool enable = true )
    {
        Time.timeScale = enable ? 0.0f : 1.0f;
    }

    private void CallbackDefaultTimeOver()
    {
        Debug.Log( "CallbackDefaultTimeOver" );
        _defaultTimer.Init( _defaultTimer.TargetTime - GameConst._NEXT_TIME_GAP );
        CreateDefaultpattern();
    }

    private void CallbackRandomTimeOver()
    {
        //_playerActor.InvokeDamage( 1 );

        _randomTimer.InitRandom();
        CreateNonPlayerActor();
    }

    private void CreateNonPlayerActor()
    { 
        int rnd = UnityEngine.Random.Range(0, 3);
        GameObject npg;
        switch (rnd)
        {
            case 0:
                npg = Instantiate<GameObject>(
                    Resources.Load<GameObject>("Prefab/Paths/Type1"),
                    Vector3.zero,
                    Quaternion.identity);
                break;
            case 1:
                npg = Instantiate<GameObject>(
                    Resources.Load<GameObject>("Prefab/Paths/Type2"),
                    Vector3.zero,
                    Quaternion.identity);
                break;
            case 2:
                npg = Instantiate<GameObject>(
                    Resources.Load<GameObject>("Prefab/Paths/Type3"),
                    Vector3.zero,
                    Quaternion.identity);
                break;
            default:
                return;
        }
        
        _nonPlayerGroupList.Add( npg.AddComponent<NonPlayerGroup>() );
    }

    private void ClearNonPlayerGroup()
    {
        for(int i =0; i < _nonPlayerGroupList.Count; ++i )
        {
            if(null != _nonPlayerGroupList[i] )
            {
                Destroy( _nonPlayerGroupList[i].gameObject );
            }
        }
        _nonPlayerGroupList.Clear();
    }

    private void CallbackPlayerDeath( GameActor target )
    {
        _gameState = eGameState.Ended;

        StartCoroutine( Helper.Wait( 1, () =>
          {
              UIGameScene.Instance.ShowResult( new ResultData()
              {
                  _score = _currentScore,
              } );
          } ) );
    }

    private void CreateDefaultpattern()
    {
        string nonPlayerPrefabURI = "";
        int rnd = UnityEngine.Random.Range(0, 0);


        GameObject nonPlayerPrefab = Resources.Load<GameObject>(nonPlayerPrefabURI);
    }
}
