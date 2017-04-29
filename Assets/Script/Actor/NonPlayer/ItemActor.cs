﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActor : NonPlayerActor
{
    protected override void CallbackDestroy( bool bSelf )
    {
        if (!bSelf)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            print(spriteRenderer.sprite.name);
            switch (spriteRenderer.sprite.name)
            {
                case "meteorite 1":
                    CallbackDamage();
                    break;
                case "meteorite 2":
                    
                    break;
                case "meteorite 3":
                    break;
                case "meteorite 4":
                    Negative();
                    break;
                case "meteorite 5":
                    UpgradePlayerActor();
                    break;
                case "meteorite 6":
                    Positive();
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
        int value = UnityEngine.Random.Range( 0, 1000 );

        if( 0 <= value && value < 15 )
        {
            Debug.Log( "무적" );
            GameManager.Instance.PlayerActor.InvokePowerMode();
        }
        else if( 15 <= value && value < 40 )
        {
            Debug.Log( "쉴드추가" );
            GameManager.Instance.PlayerActor.InvokeShield();
        }
        else if( 40 <= value && value < 65 )
        {
            Debug.Log( "체력회복" );
            GameManager.Instance.PlayerActor.InvokeHealthRecovery();
        }
        else
        {
            Debug.Log( "폭탄" );
            GameManager.Instance.InvokeDestroyAll();
        }
    }

    private void Negative()
    {
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
