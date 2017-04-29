using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    static public readonly float[] _DEFAULT_RANDOM_TIMES = new float[] { 1.0f, 2.0f, 3.0f, 4.0f };
    static public readonly float[] _NONPLAYER_HEALTH_POINT = new float[] { 5.0f, 3.0f, 1.0f, 1.0f, 3.0f, 1.0f, 1.0f, 1.0f, 2.0f };
    static public readonly float[] _NONPLAYER_DAMAGE = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f };

    static public readonly Vector3 _ATTACK_SHAKE_POSITION_VECTOR = new Vector3( 0.2f, 0.2f, 0.2f );
    static public readonly Vector3 _NAGATIVE_SHAKE_POSITION_VECTOR = new Vector3( 0.5f, 0.5f, 0.5f );
    static public readonly Vector3 _GAME_OVER_CAMERA_POSITION = new Vector3( 0.0f, 0.0f, -6.0f );

    public const float _GAME_OVER_CAMERA_MOVE_TIME = 2.0f;
    public const float _GAME_OVER_RESULT_POPUP = 0.2f;

    public const float _DEFAULT_PLAYER_DAMAGE = 1.0f;
    public const float _DEFAULT_NONPLAYER_DAMAGE = 1.0f;
    public const float _DEFAULT_PLAYER_MAX_HEALTH_POINT = 1.0f;
    public const float _DEFAULT_NON_PLAYER_MAX_HEALTH_POINT = 5.0f;

    public const float _DEFAULT_ACTOR_SPEED = 0.1f;
    public const float _DEFAULT_MISSILE_TIME = 3f;
    public const float _DEFAULT_MISSILE_SPEED = 3.0f;

    public const float _ATTACK_SHAKE_TIME = 0.2f;
    public const float _NAGATIVE_SHAKE_TIME = 5.0f;

    public const float _NAGATIVE_COVER_SPEED = 0.05f;
    public const float _NAGATIVE_COVER_TIME = 2.0f;

    public const float _POWER_MODE_TIME = 2.0f;

    public const float _DEFAULT_TIME = 0.5f;
    public const float _NEXT_TIME_GAP = 0.001f;

    public const int _DEFAULT_MAXIMUM_LAUNCH_MISSILE = 5;

    public const string _GAME_SCENE = "GameScene";
    public const string _TITLE_SCENE = "TitleScene";
    public const string _UI_GAME_SCENE = "UIGameScene";
    public const string _UI_TITLE_SCENE = "UITitleScene";

    public const string _METEORITE1 = "meteorite 1";
    public const string _METEORITE2 = "meteorite 2";
    public const string _METEORITE3 = "meteorite 3";
    public const string _METEORITE4 = "meteorite 4";
    public const string _METEORITE5 = "meteorite 5";
    public const string _METEORITE6 = "meteorite 6";

    static public readonly string[] _RANDOM_MESSAGES = new string[]
    {
        "지구 헬적화 완료.",
        "역시 헬조선 출신답게 실력이…….",
        "이 실력이니까 헬조선에서나 살지",
        "제 평가는요! 헬지구 입니다!",
        "지! 구! 폭! 파! 짝짝짝",
        "넌 커서 지구를 폭파시킬 사람이야.",
        "붐!Boom!붐! 바스틱~ 에yo~",
        "넌 예술가야. 예술은 폭발 이랬거든.",
        "이젠 현실도 터트릴 차롄가?",
        "니 미래야 임마.",
    };
}
