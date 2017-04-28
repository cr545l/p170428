using System.Collections;
using System.Collections.Generic;
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
}
