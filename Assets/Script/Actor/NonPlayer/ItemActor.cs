using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActor : NonPlayerActor
{
    protected override void CallbackDestroy( bool bSelf )
    {
        if( !bSelf )
        {
            if( 0 < UnityEngine.Random.Range(0, 2) )
            {
                Positive();
            }
            else
            {
                Negative();
            }
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
                break;
        }
    }
}
