using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    public const float _DEFAULT_DAMAGE = 1.0f;
    public const float _DEFAULT_MAX_HEALTH_POINT = 3.0f;

    static public readonly float[] _DEFAULT_RANDOM_TIMES = new float[] { 1.0f, 2.0f, 3.0f, 4.0f };
    public const float _DEFAULT_TIME = 0.5f;
    public const float _NEXT_TIME_GAP = 0.001f;

    public const string _GAME_SCENE = "GameScene";
    public const string _TITLE_SCENE = "TitleScene";
    public const string _UI_GAME_SCENE = "UIGameScene";
    public const string _UI_TITLE_SCENE = "UITitleScene";
}
