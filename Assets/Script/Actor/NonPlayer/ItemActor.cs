using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActor : NonPlayerActor
{
    protected override void CallbackDestroy( bool bSelf )
    {
        if (!bSelf)
        {
            switch (_nonPlayerActorType)
            {
                case GameConst._NONPLAYER_TYPE_METEORITE1:
                case GameConst._NONPLAYER_TYPE_METEORITE2:
                case GameConst._NONPLAYER_TYPE_METEORITE3:
                    //점수(다른 효과 없음)
                    GameManager.Instance.CountScore(GameConst._NONPLAYER_SCORE[_nonPlayerActorType] * GameManager.Instance.GetCombo());
                    GameManager.Instance.ComboUp();
                    break;
                case GameConst._NONPLAYER_TYPE_METEORITE4:
                    GameManager.Instance.CountScore(GameConst._NONPLAYER_SCORE[_nonPlayerActorType]);
                    GameManager.Instance.ResetCombo();
                    Negative();
                    break;
                case GameConst._NONPLAYER_TYPE_METEORITE5:
                    UpgradePlayerActor();
                    break;
                case GameConst._NONPLAYER_TYPE_METEORITE6:
                    Positive();
                    break;
                case GameConst._NONPLAYER_TYPE_UFO1:
                case GameConst._NONPLAYER_TYPE_UFO2:
                case GameConst._NONPLAYER_TYPE_UFO3:
                    //점수(다른 효과 없음)
                    GameManager.Instance.CountScore(GameConst._NONPLAYER_SCORE[_nonPlayerActorType] * GameManager.Instance.GetCombo());
                    GameManager.Instance.ComboUp();
                    break;
                default:
                    return;
            }
        }
    }

    private void UpgradePlayerActor()
    {
        int value = UnityEngine.Random.Range(0, 3);

        switch (value)
        {
            case 0:
                GameManager.Instance.PlayerActor.MaxHealthPoint++;
                GameManager.Instance.PlayerActor.InvokeHealthRecovery();
                break;
            case 1:
                GameManager.Instance.PlayerActor.InvokeDamageUpdate();
                break;
            case 2:
                GameManager.Instance.PlayerActor.InvokePowerModeDurationUpdate();
                break;
        }
    }

    private void Positive()
    {
        int value = UnityEngine.Random.Range( 0, 100 );

        if( 0 <= value && value < 15 )
        {
            Debug.Log( "무적" );
            GameManager.Instance.PlayerActor.InvokePowerMode();
            GameManager.Instance.CountScore(GameConst._NONPLAYER_SCORE[_nonPlayerActorType] * GameManager.Instance.GetCombo());
            GameManager.Instance.ComboUp();
        }
        else if( 15 <= value && value < 40 )
        {
            Debug.Log( "쉴드추가" );
            GameManager.Instance.PlayerActor.InvokeShield();
            GameManager.Instance.CountScore(GameConst._NONPLAYER_SCORE[_nonPlayerActorType] * GameManager.Instance.GetCombo());
            GameManager.Instance.ComboUp();
        }
        else if( 40 <= value && value < 65 )
        {
            Debug.Log( "체력회복" );
            GameManager.Instance.PlayerActor.InvokeHealthRecovery();
            GameManager.Instance.CountScore(GameConst._NONPLAYER_SCORE[_nonPlayerActorType] * GameManager.Instance.GetCombo());
            GameManager.Instance.ComboUp();
        }
        else
        {
            Debug.Log( "폭탄" );
            GameManager.Instance.InvokeDestroyAll();
        }
    }

    private void Negative()
    {
        GameManager.Instance.PlayerActor.InvokeDamage( GameConst._NAGATIVE_DEFAULT_DAMAGE );
        int value = UnityEngine.Random.Range( 0, 3 );

        switch( value )
        {
            case 0:
                Debug.Log( "최대체력 감소" );
                GameManager.Instance.PlayerActor.MaxHealthPoint--;
                break;

            case 1:
                Debug.Log( "화면 흔들기" );
                GameManager.Instance.InvokeNagativeShakeCamera();
                break;

            case 2:
                Debug.Log( "화면 가리기" );
                UIGameScene.Instance.InvokeCover( GameConst._NAGATIVE_COVER_TIME );
                break;
        }
    }
}
