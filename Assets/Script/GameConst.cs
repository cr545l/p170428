﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    static public readonly float[] _DEFAULT_RANDOM_TIMES = new float[] { 1.0f, 1.5f, 2.0f };

    static public readonly float[] _NONPLAYER_HEALTH_POINT = new float[] {
        5.0f, 3.0f, 1.0f, // 빨강, 회색, 파랑
        1.0f, 3.0f, 1.0f, // 네거티브, 강화, 아이템
        1.0f, 1.0f, 2.0f, // UFO
    };

    static public readonly float[] _NONPLAYER_DAMAGE = new float[] {
        1.0f, 1.0f, 1.0f,
        0.0f, 1.0f, 1.0f,
        1.0f, 2.0f, 1.0f,
    };

    static public readonly float[] _NONPLAYER_UPGRADE_TIME = new float[] { 10,0f, 20.0f };

    static public readonly int[] _NONPLAYER_SCORE = new int[] { 30, 20, 10, -100, 0, 30, 10, 30, 30 };

    static public readonly Vector3 _ATTACK_SHAKE_POSITION_VECTOR = new Vector3( 0.2f, 0.2f, 0.2f );
    static public readonly Vector3 _NAGATIVE_SHAKE_POSITION_VECTOR = new Vector3( 0.5f, 0.5f, 0.5f );
    static public readonly Vector3 _GAME_OVER_CAMERA_POSITION = new Vector3( 0.0f, 0.0f, -6.0f );

    public const float _GAME_OVER_CAMERA_MOVE_TIME = 2.0f;
    public const float _GAME_OVER_RESULT_POPUP = 0.2f;

    public const float _DEFAULT_PLAYER_DAMAGE = 1.0f;
    public const float _DEFAULT_NONPLAYER_DAMAGE = 1.0f;
    public const float _DEFAULT_PLAYER_MAX_HEALTH_POINT = 10.0f; // 기본체력
    public const float _DEFAULT_NON_PLAYER_MAX_HEALTH_POINT = 5.0f;

    public const float _DEFAULT_ACTOR_SPEED = 0.1f; // 액터 이동속도
    public const float _DEFAULT_ACTOR_SPEED_DEPTH = 0.0005f;
    public const float _DEFAULT_ACTOR_SPEED_SLOW_UPGRADE = 0.007f;

    public const float _DEFAULT_MISSILE_TIME = 3f;
    public const float _DEFAULT_MISSILE_SPEED = 160.0f; // 미사일 속도

    public const float _ATTACK_SHAKE_TIME = 0.2f;
    public const float _NAGATIVE_SHAKE_TIME = 5.0f;

    public const float _NAGATIVE_COVER_SPEED = 0.05f;
    public const float _NAGATIVE_COVER_TIME = 1.0f;
    public const float _NAGATIVE_DEFAULT_DAMAGE = 1.0f; // 네거티브 액터를 공격 시 받는 데미지

    public const float _POWER_MODE_TIME = 2.0f;

    public const float _DEFAULT_TIME = 0.5f;
    public const float _NEXT_TIME_GAP = 0.005f;

    public const int _DEFAULT_MAXIMUM_LAUNCH_MISSILE = 10; // 기본 미사일 개수

    public const string _GAME_SCENE = "GameScene";
    public const string _TITLE_SCENE = "TitleScene";
    public const string _UI_GAME_SCENE = "UIGameScene";
    public const string _UI_TITLE_SCENE = "UITitleScene";

    public const int _NONPLAYER_TYPE_METEORITE1 = 0;
    public const int _NONPLAYER_TYPE_METEORITE2 = 1;
    public const int _NONPLAYER_TYPE_METEORITE3 = 2;
    public const int _NONPLAYER_TYPE_METEORITE4 = 3;
    public const int _NONPLAYER_TYPE_METEORITE5 = 4;
    public const int _NONPLAYER_TYPE_METEORITE6 = 5;
    public const int _NONPLAYER_TYPE_UFO1 = 6;
    public const int _NONPLAYER_TYPE_UFO2 = 7;
    public const int _NONPLAYER_TYPE_UFO3 = 8;

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


public static class PlayerPrefsKey
{
    public const string _BEST_SCORE = "_HIGH_SCORE";
}