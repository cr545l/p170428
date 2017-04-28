using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMessageType
{
    All,
    Player,
    NonPlayer,
}

public struct GameMessage
{
    public eMessageType _targetType;
    public GameActor _invokeActor;
    public GameActor _targetActor;
}
