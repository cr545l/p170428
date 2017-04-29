using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    public const float _DEFAULT_DAMAGE = 1.0f;
    public const float _DEFAULT_PLAYER_MAX_HEALTH_POINT = 30.0f;
    public const float _DEFAULT_NON_PLAYER_MAX_HEALTH_POINT = 3.0f;

    static public readonly float[] _DEFAULT_RANDOM_TIMES = new float[] { 1.0f, 2.0f, 3.0f, 4.0f };
    public const float _DEFAULT_TIME = 0.5f;
    public const float _NEXT_TIME_GAP = 0.001f;

    public const string _GAME_SCENE = "GameScene";
    public const string _TITLE_SCENE = "TitleScene";
    public const string _UI_GAME_SCENE = "UIGameScene";
    public const string _UI_TITLE_SCENE = "UITitleScene";
    
    static public readonly string[] _RANDOM_MESSAGES = new string[]
    {
        "테스트 문구0",
        "테스트 문구1",
        "테스트 문구2",
        "테스트 문구3",
        "테스트 문구4",
        "테스트 문구5",
        "테스트 문구6",
        "테스트 문구7",
        "테스트 문구8",
        "테스트 문구9",
    };
}
