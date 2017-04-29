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
    private PlayerActor _playerActor = null;

    private eGameState _gameState = eGameState.None;
    private GameTimer _defaultTimer = new GameTimer();
    private GameTimer _randomTimer = new GameTimer();

    private int _currentScore = 0;

    public PlayerActor PlayerActor { get { return _playerActor; } }
    public GameTimer Timer { get { return _randomTimer; } }

    private void Start()
    {
        if( Helper.isNull( _playerActor ) ) return;

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
        _defaultTimer.Init( GameConst._DEFAULT_TIME );
        _randomTimer.InitRandom();
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
    }

    private void CallbackRandomTimeOver()
    {
        _randomTimer.InitRandom();
        CreateNonPlayerActor();
    }

    private void CreateNonPlayerActor()
    {
        // _eventGameActor += 
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

        npg.AddComponent<NonPlayerGroup>();
    }

    private void CallbackPlayerDeath( GameActor target )
    {
        _gameState = eGameState.Ended;
        UIGameScene.Instance.ShowResult( new ResultData()
        {
            _score = _currentScore,
        } );
    }
}
