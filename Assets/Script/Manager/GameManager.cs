using System;
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
    private ParticleSystem[] _particleSystems = null;

    [SerializeField]
    private ParticleSystem[] _boostParticleSystems = null;

    [SerializeField]
    private Sprite[] _nonPlayerSprites = null;

    [SerializeField]
    private Animator[] _nonPlayerDeathAnimators = null;

    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private GameObject[] _pattons = null;

    [SerializeField]
    private PlayerActor _playerActor = null;

    [SerializeField]
    private AudioClip _nonPlayerDeathAudioClip = null;

    [SerializeField]
    private Light _spotLight = null;

    //[SerializeField]
    //private SpriteRenderer _background = null;

    private eGameState _gameState = eGameState.None;
    private GameTimer _defaultTimer = new GameTimer();
    private GameTimer _randomTimer = new GameTimer();

    private GameTimer _npUpgradeAt10 = new GameTimer();
    private GameTimer _npUpgradeAt20 = new GameTimer();
    private float _npNormalUpgradeHp = 0;
    private float _npNormalUpgradeAtk = 0;
    private float _npPositiveUpgradeHp = 0;

    private List<NonPlayerActorGroup> _nonPlayerGroupList = new List<NonPlayerActorGroup>();

    private Vector3 _cameraOriginalPosition = Vector3.zero;
    private Coroutine _cameraCoroutine = null;
    
    private int _currentScore = 0;
    private int _combo = 0;
    private string _currentPatton = string.Empty;
    private float _currentActorSpeed = GameConst._DEFAULT_ACTOR_SPEED;

    public PlayerActor PlayerActor { get { return _playerActor; } }
    public GameTimer Timer { get { return _randomTimer; } }

    public float NpNormalUpgradeHp { get { return _npNormalUpgradeHp; } }
    public float NpNormalUpgradeAtk { get { return _npNormalUpgradeAtk; } }
    public float NpPositiveUpgradeHp { get { return _npPositiveUpgradeHp; } }

    public Sprite[] NonPlayerSprites { get { return _nonPlayerSprites; } }
    public Animator[] NonPlayerDeathAnimators { get { return _nonPlayerDeathAnimators; } }
    public AudioClip NonPlayerDeathAudioClip { get { return _nonPlayerDeathAudioClip; } }
    public Camera Camera { get { return _camera; } }
    public ParticleSystem[] BoostParticleSystems { get { return _boostParticleSystems; } }
    public string CurrentPatton { get { return _currentPatton; } }
    public float CurrentActorSpeed { get { return _currentActorSpeed; } set { _currentActorSpeed = value; } }

    private void Start()
    {
        if( Helper.isNull( _camera, _playerActor, _nonPlayerDeathAudioClip, _spotLight ) ) return;

        _cameraOriginalPosition = _camera.transform.position;

        _defaultTimer._eventTimeOver += CallbackDefaultTimeOver;
        _randomTimer._eventTimeOver += CallbackRandomTimeOver;

        _npUpgradeAt10._eventTimeOver += CallbackNpUpdate10;
        _npUpgradeAt20._eventTimeOver += CallbackNpUpdate20;

        _playerActor._eventDeath += CallbackPlayerDeath;

        InvokeStart();
    }

    private void Update()
    {
        if( eGameState.Playing == _gameState )
        {
            _defaultTimer.CheckTime( Time.deltaTime );
            _randomTimer.CheckTime( Time.deltaTime );

            //float value = _playerActor.CurrentHealthPointPercent;
            _spotLight.intensity = 50 - ( 50 * _playerActor.CurrentHealthPointPercent );
            //_background.color = new Color( 1, value, value, 1 );
        }
    }

    public void InvokeAttackShakeCamera()
    {
        iTween.ShakePosition( _camera.gameObject,
            GameConst._ATTACK_SHAKE_POSITION_VECTOR,
            GameConst._ATTACK_SHAKE_TIME );

        if( null != _cameraCoroutine )
        {
            StopCoroutine( _cameraCoroutine );
        }

        _cameraCoroutine = StartCoroutine( Helper.Wait( GameConst._ATTACK_SHAKE_TIME, () =>
        {
            _camera.transform.position = _cameraOriginalPosition;
        } ) );
    }

    public void InvokeNagativeShakeCamera()
    {
        iTween.ShakePosition( _camera.gameObject,
            GameConst._NAGATIVE_SHAKE_POSITION_VECTOR,
            GameConst._NAGATIVE_SHAKE_TIME );

        if( null != _cameraCoroutine )
        {
            StopCoroutine( _cameraCoroutine );
        }

        _cameraCoroutine = StartCoroutine( Helper.Wait( GameConst._NAGATIVE_SHAKE_TIME, () =>
        {
            _camera.transform.position = _cameraOriginalPosition;
        } ) );
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
        if(null != _particleSystems )
        {
            for(int i =0; i < _particleSystems.Length; ++i )
            {
                _particleSystems[i].Simulate( 100000.0f );
                _particleSystems[i].Play();
            }
        }

        _camera.transform.position = _cameraOriginalPosition;
        _gameState = eGameState.Playing;

        _currentActorSpeed = GameConst._DEFAULT_ACTOR_SPEED;
        _currentScore = 0;

        _playerActor.Init();
        _defaultTimer.Init( GameConst._DEFAULT_TIME );
        _randomTimer.InitRandom();
        _npUpgradeAt10.Init(GameConst._NONPLAYER_UPGRADE_TIME[0]);
        _npUpgradeAt20.Init(GameConst._NONPLAYER_UPGRADE_TIME[1]);
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
        //Debug.Log( "CallbackDefaultTimeOver" );

        _currentActorSpeed += GameConst._DEFAULT_ACTOR_SPEED_DEPTH;
        _defaultTimer.Init( _defaultTimer.TargetTime - GameConst._NEXT_TIME_GAP );
        CreateDefaultPattern();
    }
     
    private void CallbackRandomTimeOver()
    {
        _randomTimer.InitRandom();
        CreateNonPlayerActor();
    }

    private void CallbackNpUpdate10()
    {
        _npNormalUpgradeHp++;
    }

    private void CallbackNpUpdate20()
    {
        _npNormalUpgradeAtk++;
        _npPositiveUpgradeHp *= 2;
    }

    private void CreateNonPlayerActor()
    {
        if( null != _pattons )
        {
            GameObject actorGroup = _pattons[UnityEngine.Random.Range( 0, _pattons.Length )];
            GameObject instance = Instantiate<GameObject>( actorGroup, Vector3.zero, Quaternion.identity );
            _nonPlayerGroupList.Add( instance.AddComponent<NonPlayerActorGroup>() );
            _currentPatton = actorGroup.name;
        }
    }

    public void InvokeDestroyAll( bool bNagativeDestroy )
    {
        for( int i = 0; i < _nonPlayerGroupList.Count; ++i )
        {
            for(int j = 0; j< _nonPlayerGroupList[i].NonPlayerActorList.Count; ++j )                
            {
                var target = _nonPlayerGroupList[i].NonPlayerActorList[j];
                if( !bNagativeDestroy && target.isNagative )
                {
                    continue;
                }
                
                target.InvokeDestroy();
            }
        }
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

    private void CallbackPlayerDeath( Actor target )
    {
        _gameState = eGameState.Ended;

        StartCoroutine( Helper.Wait( GameConst._GAME_OVER_CAMERA_MOVE_TIME + 
                                    GameConst._GAME_OVER_RESULT_POPUP, () =>
          {
              // PlayerPrefs.SetInt(PlayerPrefsKey._HIGH_SCORE, _score);
              UIGameScene.Instance.ShowResult( new ResultData()
              {
                  _score = _currentScore,
              } );
          } ) );
    }

    private void CreateDefaultPattern()
    {
        string nonPlayerPrefabURI = "";
        int rnd = UnityEngine.Random.Range(0, 0);

        GameObject nonPlayerPrefab = Resources.Load<GameObject>(nonPlayerPrefabURI);
    }

    public void CountScore(int gameScore)
    {
        _currentScore += gameScore;
    }

    public int GetScore()
    {
        return _currentScore;
    }

    public int GetCombo()
    {
        return _combo;
    }

    public void ComboUp()
    {
        _combo++;
    }

    public void ResetCombo()
    {
        _combo = 0;
    }
}
