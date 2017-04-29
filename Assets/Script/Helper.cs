using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public static class Helper
{
    static public bool isNull(params object[] objects )
    {
        for(int i =0; i< objects.Length; ++i )
        {
            if(null == objects[i])
            {
                Debug.LogError( "연결된 객체를 찾지 못함. 인스펙터 확인 필요" );
                return true;
            }
        }

        return false;
    }

    static public IEnumerator Wait( float time, Action callback )
    {
        yield return new WaitForSeconds( time );
        callback();
    }

    static public AnimationClip GetClip( this Animator taget, string animationClipName = null )
    {
        AnimationClip clip = null;
        AnimatorController ac = taget.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine sm = ac.layers[0].stateMachine;

        for( int i = 0; i < sm.states.Length; i++ )
        {
            AnimatorState state = sm.states[i].state;
            if( null != animationClipName )
            {
                if( state.name == animationClipName )
                {
                    clip = state.motion as AnimationClip;
                }
            }
            else
            {
                clip = state.motion as AnimationClip;
                break;
            }
        }
        return clip;
    }

    static public void LookAt2D( this Transform target, Vector3 worldPosition )
    {
        var dir = worldPosition - target.position;
        var angle = Mathf.Atan2( dir.y, dir.x ) * Mathf.Rad2Deg;
        target.rotation = Quaternion.AngleAxis( angle, Vector3.forward );
    }
}
